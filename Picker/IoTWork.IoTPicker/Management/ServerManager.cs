using IoTWork.Protocol.Types;
using IoTWork.Infrastructure;
using IoTWork.Infrastructure.Interfaces;
using IoTWork.Infrastructure.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IoTWork.Protocol;
using System.Collections.Concurrent;
using IoTWork.IoTPicker.Servers;
using IoTWork.IoTPicker.Interfaces;
using IoTWork.Connections;
using IoTDal.Provider.Npgsql;
using IoTWork.Infrastructure.Helpers;
using IoTDal.Model.DB.IoTWork.Data;
using IoTDal.Access;
using System.Diagnostics;
using IoTWork.Infrastructure.Management;
using IoTWork.Infrastructure.Statistics;
using IotWork.Utils.Helpers;
using IoTWork.Protocol.Devices;

namespace IoTWork.IoTPicker.Management
{
    internal enum ServerRole
    {
        Data,
        Management
    }

    internal class ServerManager
    {
        internal class DataPacket
        {
            public DateTime ReceivedOn { get; set; }
            public IIotPacket Packet { get; set; }
        }

        public string UniqueName { get; internal set; }

        private ServerRole _role;

        public IChannelServer Server { get; internal set; }
        public IChannelWriter Writer { get; internal set; }

        private bool _mustrun;
        private Thread _thread;

        private Thread _datathread;
        private ConcurrentQueue<DataPacket> _dataqueue;

        private ConcurrentQueue<IIotPacket> _packetsQueue;

        private int _serverSequenceNumber;

        private SortedList<string, int> _readersSequenceNumber;
        private SortedList<string, DateTime> _readersFirstTriggeredOn;
        private SortedList<string, DateTime> _readersLastTriggeredOn;
        private SortedList<string, Double> _writersMaxTime;
        private SortedList<string, Double> _writersMinTime;
        private SortedList<string, Double> _writersMeanTime;
        private SortedList<string, int> _readersPacketReceived;
        private SortedList<string, int> _readersBytesReceived;

        private Journal<ExceptionContainer> _exceptions;

        private object _exlock;
        private object _stlock;

        List<String> _devicesUniqueNames;

        private bool _enableCustomAgents;
        private bool _enableDatabaseWriting;
        private bool _enableHookToCentral;

        public ServerManager()
        {
            _mustrun = false;
            _thread = new Thread(_Run);
            _packetsQueue = null;

            _serverSequenceNumber = 0;

            _readersSequenceNumber = new SortedList<string, int>();
            _readersFirstTriggeredOn = new SortedList<string, DateTime>();
            _readersLastTriggeredOn = new SortedList<string, DateTime>();
            _writersMaxTime = new SortedList<string, double>();
            _writersMinTime = new SortedList<string, double>();
            _writersMeanTime = new SortedList<string, double>();
            _readersPacketReceived = new SortedList<string, int>();
            _readersBytesReceived = new SortedList<string, int>();

            _exceptions = new Journal<ExceptionContainer>(200);

            _exlock = new object();
            _stlock = new object();

            _devicesUniqueNames = new List<string>();
        }

        internal void SetPacketsQueue(ConcurrentQueue<IIotPacket> PacketsQueue)
        {
            _packetsQueue = PacketsQueue;
        }

        internal void Stop()
        {
            Server.server.Stop();

            _mustrun = false;
        }

        internal void Start()
        {
            _datathread = null;
            _dataqueue = null;

            _mustrun = true;

            Server.server.Start();

            if (_enableDatabaseWriting && _role == ServerRole.Data)
            {
                _dataqueue = new ConcurrentQueue<DataPacket>();
                _datathread = new Thread(_RunDataStore);
                _datathread.Start();
            }

            _thread.Start();
        }

        internal void Wait()
        {
            try
            {
                if (_thread != null)
                {
                    bool terminated = false;
                    int count = 16;
                    while (count > 0)
                    {
                        if (_thread.Join(500))
                        {
                            terminated = true;
                            break;
                        }
                        count--;
                    }

                    if (!terminated)
                        _thread.Abort();

                    _thread = null;
                }

                if (_datathread != null)
                {
                    bool terminated = false;
                    int count = 16;
                    while (count > 0)
                    {
                        if (_datathread.Join(500))
                        {
                            terminated = true;
                            break;
                        }
                        count--;
                    }

                    if (!terminated)
                        _datathread.Abort();

                    _datathread = null;
                }
            }
            catch(Exception ex)
            {
                LogManager.Error(ex.Message, ex);
            }
        }

        internal void _Run()
        {
            bool dequeued = true;

            while (_mustrun || dequeued)
            {
                NetworkPacket packet = null;

                try
                {
                    dequeued = Server.server.TryGetMessage(out packet);
                }
                catch (Exception)
                {
                    dequeued = false;
                }

                if (dequeued && packet != null)
                {
                    _ManagePacket(packet);

                    if (_mustrun)
                        Thread.Sleep(100);
                }
                else
                {
                    if (_mustrun)
                        Thread.Sleep(250);
                }
            }
        }

        private void _ManagePacket(NetworkPacket packet)
        {
            try
            {
                _serverSequenceNumber++;

                byte[] message = null;
                message = packet.Message;

                IPacketManagerInput pmi = new PacketManagerInput();

                IIotPacket originalmessage = null;

                if (_role == ServerRole.Data)
                    originalmessage = pmi.Decode(message);
                else if (_role == ServerRole.Management)
                    originalmessage = pmi.Decode(message);

                if (originalmessage != null)
                {
                    originalmessage.SetRemoteEndpoint(packet.RemoteEndPoint);

                    if (_enableHookToCentral && _role == ServerRole.Management && _packetsQueue != null)
                        _packetsQueue.Enqueue(originalmessage);

                    Header h = originalmessage.GetHeader();
                    Payload p = originalmessage.GetPayload();

                    UpdateDevices(h.DeviceUniqueAddress);

                    ServiceCodes scode = (ServiceCodes)h.ServiceCode;
                    PacketCodes pcode = (PacketCodes)h.PacketCode;

                    LogManager.Info(LogFormat.Format("PACKET {0} [{1}].{2} {3}.{4}",
                        scode.ToString()+"."+ pcode.ToString(),
                        h.DeviceUniqueAddress,
                        h.SequenceNumber,
                        h.ServiceCode,
                        h.PacketCode));

                    //System.Console.WriteLine("PACKET [{0}].{1} {2}.{3}",
                    //    h.DeviceUniqueAddress,
                    //    h.SequenceNumber,
                    //    h.ServiceCode,
                    //    h.PacketCode);

                    Stopwatch sw = new Stopwatch();

                    if (_role == ServerRole.Data)
                    {
                        sw.Start();

                        if (_enableCustomAgents && Writer != null)
                        {
                            Writer.writer.Write(
                                h.ServiceCode,
                                h.PacketCode,
                                h.SentAt,
                                h.SequenceNumber,
                                h.SourceAdrress,
                                h.Region,
                                h.SensorUniqueAddress,
                                p
                                );
                        }

                        if (_enableDatabaseWriting && _dataqueue != null)
                        {
                            DataPacket dp = new DataPacket();
                            dp.ReceivedOn = DateTime.Now;
                            dp.Packet = originalmessage;
                            _dataqueue.Enqueue(dp);
                        }

                        sw.Stop();
                    }

                    CalculateAndUpdateStatistics(h.DeviceUniqueAddress, message.Length, sw.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message, ex);

                RegisterException(_role.ToString(), UniqueName, DateTime.Now, _serverSequenceNumber, ex);
            }
        }

        private void UpdateDevices(string DeviceUniqueAddress)
        {
            if (_devicesUniqueNames.Where(x => x == DeviceUniqueAddress).FirstOrDefault() == null)
                _devicesUniqueNames.Add(DeviceUniqueAddress);
        }

        internal void _RunDataStore()
        {
            DbConnectionManager connmanager = DbConnectionManager.Instance;
            bool dequeued = true;

            while (_mustrun || dequeued)
            {
                DataPacket dataPacket = null;

                try
                {
                    dequeued = _dataqueue.TryDequeue(out dataPacket);
                }
                catch (Exception)
                {
                    dequeued = false;
                }

                if (dequeued && dataPacket != null)
                {
                    _ManagePacketForStore(dataPacket);

                    if (_mustrun)
                        Thread.Sleep(100);
                }
                else
                {
                    if (_mustrun)
                        Thread.Sleep(250);
                }
            }
        }

        private void _ManagePacketForStore(DataPacket dataPacket)
        {
            IoTWork.Protocol.Datas.Sample[] samples = null;
            Boolean issample = false;

            Header h = dataPacket.Packet.GetHeader();
            Payload p = dataPacket.Packet.GetPayload();

            if (p.GetType() == typeof(IoTWork.Protocol.Datas.Sample))
            {
                issample = true;
                samples = new Protocol.Datas.Sample[1];
                samples[0] = (IoTWork.Protocol.Datas.Sample)p;
            }
            else if (p.GetType() == typeof(IoTWork.Protocol.Datas.Measures))
            {
                var measures = ((IoTWork.Protocol.Datas.Measures)p).datas;
                if (measures != null && measures.Count > 0)
                {
                    issample = true;
                    samples = measures.ToArray();
                }
            }

            if (issample)
            {
                try
                {
                    var ordinals = h.SensorUniqueAddress.Replace("s", "").Split('.').Select(x => Int32.Parse(x)).ToArray();

                    using (var dbmanager = new IoTWorkDataDalManager())
                    {
                        foreach (var s in samples)
                        {
                            var realtype = s.GetType();

                            var xml = SerializerHelper.ToXml(s.data);
                            var csv = SerializerHelper.ToCsv(s.data);
                            var bin = SerializerHelper.BinarySerialize(s.data);

                            Sample dbsample = new Sample();

                            dbsample.TriggeredOn = s.AcquiredAt;
                            dbsample.SentOn = h.SentAt;
                            dbsample.ReceivedOn = dataPacket.ReceivedOn;
                            dbsample.CreatedOn = DateTime.Now;

                            dbsample.OrdinalForNetwork = ordinals[0];
                            dbsample.OrdinalForRegion = ordinals[1];
                            dbsample.OrdinalForRing = ordinals[2];
                            dbsample.OrdinalForDevice = ordinals[3];
                            dbsample.OrdinalForChain = ordinals[4];
                            dbsample.OrdinalForSensor = ordinals[5];

                            dbsample.BinValue = bin;
                            dbsample.XmlValue = xml;
                            dbsample.CsvValue = csv;

                            dbmanager.SaveSample(dbsample);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(ex.Message, ex);
                }
            }
        }

        internal void SetProfiles(bool enableCustomAgents, bool enableDatabaseWriting, bool enableHookToCentral)
        {
            _enableCustomAgents = enableCustomAgents;
            _enableDatabaseWriting = enableDatabaseWriting;
            _enableHookToCentral = enableHookToCentral;
        }

        internal void SetRole(ServerRole Role)
        {
            _role = Role;
        }

        private void CalculateAndUpdateStatistics(string nuriKey, int Bytes, long ElapsedMillisecondsForWriters)
        {
            lock (_stlock)
            {
                if (!_readersSequenceNumber.ContainsKey(nuriKey))
                    _readersSequenceNumber.Add(nuriKey, 0);

                if (!_readersFirstTriggeredOn.ContainsKey(nuriKey))
                    _readersFirstTriggeredOn.Add(nuriKey, DateTime.Now);

                if (!_readersLastTriggeredOn.ContainsKey(nuriKey))
                    _readersLastTriggeredOn.Add(nuriKey, DateTime.Now);

                if (_role == ServerRole.Data)
                {
                    if (!_writersMaxTime.ContainsKey(nuriKey))
                        _writersMaxTime.Add(nuriKey, Double.MinValue);

                    if (!_writersMinTime.ContainsKey(nuriKey))
                        _writersMinTime.Add(nuriKey, Double.MaxValue);

                    if (!_writersMeanTime.ContainsKey(nuriKey))
                        _writersMeanTime.Add(nuriKey, 0);
                }

                if (!_readersPacketReceived.ContainsKey(nuriKey))
                    _readersPacketReceived.Add(nuriKey, 0);

                if (!_readersBytesReceived.ContainsKey(nuriKey))
                    _readersBytesReceived.Add(nuriKey, 0);

                _readersSequenceNumber[nuriKey]++;
                _readersLastTriggeredOn[nuriKey] = DateTime.Now;

                if (_role == ServerRole.Data)
                {
                    if (ElapsedMillisecondsForWriters > _writersMaxTime[nuriKey])
                        _writersMaxTime[nuriKey] = ElapsedMillisecondsForWriters;

                    if (ElapsedMillisecondsForWriters < _writersMinTime[nuriKey])
                        _writersMinTime[nuriKey] = ElapsedMillisecondsForWriters;

                    if (_readersSequenceNumber[nuriKey] == 1)
                        _writersMeanTime[nuriKey] = ElapsedMillisecondsForWriters;
                    else
                        _writersMeanTime[nuriKey] = ((_writersMeanTime[nuriKey] * (_readersSequenceNumber[nuriKey] - 1)) + ElapsedMillisecondsForWriters) / _readersSequenceNumber[nuriKey];
                }

                _readersPacketReceived[nuriKey]++;
                _readersBytesReceived[nuriKey] += Bytes;
            }
        }

        private void RegisterException(String Module, String UniqueName, DateTime When, Int32 SequenceNumber, Exception Exception)
        {
            ExceptionContainer ec = new ExceptionContainer();
            ec.UniqueName = UniqueName;
            ec.When = When;
            ec.Module = Module;
            ec.Order = SequenceNumber;
            ec.ex = Exception;

            lock (_exlock)
            {
                _exceptions.Add(ec);
            }
        }

        internal Infrastructure.Statistics.Statistics GetStatistics()
        {
            Infrastructure.Statistics.Statistics statistics = new Infrastructure.Statistics.Statistics();

            foreach (var d in _devicesUniqueNames)
            {
                var moduleName = _role.ToString() + " " + UniqueName + " " + d;

                lock (_stlock)
                {
                    statistics.Add(moduleName, "SamplingSequenceNumber", _readersSequenceNumber[d].ToString(), NoteDomain.System);
                    statistics.Add(moduleName, "FirstTriggeredOn", _readersFirstTriggeredOn[d].ToString(), NoteDomain.System);
                    statistics.Add(moduleName, "LastTriggeredOn", _readersLastTriggeredOn[d].ToString(), NoteDomain.System);

                    if (_role == ServerRole.Data)
                    {
                        statistics.Add(moduleName, "MinDuration", _writersMaxTime[d].ToString(), NoteDomain.System);
                        statistics.Add(moduleName, "MaxDuration", _writersMinTime[d].ToString(), NoteDomain.System);
                        statistics.Add(moduleName, "MeanDuration", _writersMeanTime[d].ToString(), NoteDomain.System);
                    }

                    statistics.Add(moduleName, "PacketsReceived", _readersPacketReceived[d].ToString(), NoteDomain.Network);
                    statistics.Add(moduleName, "BytesReceived", _readersBytesReceived[d].ToString(), NoteDomain.Network);
                }
            }

            return statistics;
        }

        internal ErrorResume GetErrors()
        {
            ErrorResume _resume = new ErrorResume();

            lock (_exlock)
            {
                _resume = IoTWorkHelper.ToErrorResume(_exceptions);
            }

            return _resume;
        }
    }
}
