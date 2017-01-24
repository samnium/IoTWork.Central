using IoTWork.IoTPicker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.Protocol.Types;
using System.Reflection;
using IoTWork.Contracts;
using IoTWork.Infrastructure;

namespace IoTWork.IoTPicker.Writers
{
    internal class CustomWriter : IWriter, IWriterInitializer
    {
        IIoTWriter _writer;

        public CustomWriter()
        {
            _writer = null;
        }

        public void Init(string LibraryPath, string InitParameter)
        {
            try
            {
                // http://stackoverflow.com/questions/1137781/c-sharp-correct-way-to-load-assembly-find-class-and-call-run-method
                // http://stackoverflow.com/questions/23735122/get-all-c-sharp-types-that-implements-an-interface-first-but-no-derived-classes

                Assembly assembly = Assembly.LoadFile(LibraryPath);

                if (assembly == null)
                    throw new Exception("Assembly  " + LibraryPath + " not found. Writer can't be allocated");

                Type type = assembly.GetTypes().Where(x => typeof(IIoTWriter).IsAssignableFrom(x)).FirstOrDefault();

                if (type == null)
                    throw new Exception("Instance of IIoTWriter not found on dll " + LibraryPath + ". Writer can't be allocated");

                _writer = Activator.CreateInstance(type) as IIoTWriter;

                if (_writer == null)
                    throw new Exception("Instance of IIoTWriter can't be activated on dll " + LibraryPath + ". Writer can't be allocated");

                _writer.Init(InitParameter);
            }
            catch (Exception ex)
            {
                LogManager.Error(ex.Message, ex);
                _writer = null;
            }
        }

        public void Write(ushort serviceCode, ushort packetCode, DateTime sentAt, ulong sequenceNumber, string sourceAdrress, string region, string deviceUniqueAddress, Payload p)
        {
            if (_writer != null)
            {
                try
                {
                    IoTWork.Protocol.Datas.Sample[] samples = null;
                    Boolean issample = false;

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
                            var ordinals = deviceUniqueAddress.Replace("s", "").
                                Split('.').Select(x => Int32.Parse(x)).ToArray();

                            var OrdinalForNetwork = ordinals[0];
                            var OrdinalForRegion = ordinals[1];
                            var OrdinalForRing = ordinals[2];
                            var OrdinalForDevice = ordinals[3];
                            var OrdinalForChain = ordinals[4];
                            var OrdinalForSensor = ordinals[5];

                            foreach (var s in samples)
                            {
                                Assembly[] asmblies = AppDomain.CurrentDomain.GetAssemblies();
                                _writer.Write(sentAt, DateTime.Now, (long)sequenceNumber, sourceAdrress,
                                    OrdinalForNetwork, OrdinalForRegion, OrdinalForRing, OrdinalForDevice, OrdinalForSensor,
                                    deviceUniqueAddress, s.data);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error(ex.Message, ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error(ex.Message, ex);
                }
            }
        }
    }
}
