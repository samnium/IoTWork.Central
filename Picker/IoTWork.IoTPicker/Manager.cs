using IoTWork.IoTPicker.Helpers;
using IoTWork.IoTPicker.Management;
using IoTWork.Infrastructure;
using IoTWork.XML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel.Description;
using System.ServiceModel;
using IoTWork.Infrastructure.Interfaces;
using IoTWork.Infrastructure.Types;
using System.Collections.Concurrent;
using IoTWork.Protocol;
using IoTWork.Communication.Model;
using IoTWork.Communication;
using IoTWork.Queues;
using IoTWork.Infrastructure.Helpers;
using IoTWork.Infrastructure.Statistics;
using IoTWork.Communication.Helpers;
using IoTWork.Protocol.Devices;
using System.IO;

namespace IoTWork.IoTPicker
{
    [Serializable]
    internal class CorePluginProvider
    {
        public CorePluginProvider()
        {
            Initialize();
        }

        public IDictionary<string, Assembly> Plugins { get; private set; }

        public void Initialize()
        {
            Plugins = new SortedDictionary<string, Assembly>();
            foreach (var file in Directory.GetFiles(@"C:\iotserver\modules", "*.dll", SearchOption.TopDirectoryOnly))
                Plugins.Add(AssemblyName.GetAssemblyName(file).FullName, Assembly.LoadFile(file));

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (!Plugins.ContainsKey(args.Name))
                throw new InvalidOperationException(
                      String.Format("Could not determine valid assembly in context for assembly name '{0}'.", args.Name));
            return Plugins[args.Name];
        }
    }

    public class Manager
    {
        private Context _context;

        private Boolean _webhttpOrBasichttp = false;

        private List<PickerManager> _managers = null;

        private Boolean _mustrun = false;

        private ConcurrentQueue<IIotPacket> _packetsQueue;

        private Publisher<IoTCommMessage> _publisher;

        private IoTWork.Configuration.ConfigurationManager _cm;

        public Manager()
        {
            _context = new Context();
            _managers = new List<PickerManager>();
            _packetsQueue = new ConcurrentQueue<IIotPacket>();
        }

        public void Run(int NetworkOrdinal, int RingOrdinal, int RegionOrdinal, string ManagementAddress,
             bool EnableCustomAgents, bool EnableDatabaseWriting, bool EnableHookToCentral, bool EnableManagementInterface)
        {
            if (EnableHookToCentral)
            {
                _cm = IoTWork.Configuration.ConfigurationManager.Instance;
            }


            DateTime StartedOn = DateTime.Now;

            LogManager.Init(ConfigurationManager.AppSettings["Configuration_FilePath_Log4Net"] + RegionOrdinal.ToString() + ".xml", "IoTReader" + RegionOrdinal.ToString());

            LogManager.Info("***************************************************");
            LogManager.Info("* IoTPicker                                       *");
            LogManager.Info("***************************************************");
            LogManager.Info("");
            LogManager.Info(String.Format("I'm the number {0}", RegionOrdinal));

            IoTCommNuri nuri = new IoTCommNuri();
            nuri.DeviceEntityOrdinal = String.Empty;
            nuri.DeviceOrdinal = null;
            nuri.NetworkOrdinal = NetworkOrdinal;
            nuri.RegionOrdinal = RegionOrdinal;
            nuri.RingOrdinal = RingOrdinal;
            nuri.Layer = IoTCommNuriLayer.Region;


            #region Self Hosted Web Service
            ManagementService managementService = null;
            ServiceHost svcHost = null;

            if (EnableManagementInterface)
            {
                // https://blogs.msdn.microsoft.com/alikl/2010/06/04/walkthrough-build-host-and-test-simple-restful-wcf-service/
                // https://msdn.microsoft.com/en-us/library/aa738489(v=vs.110).aspx

                managementService = new ManagementService();

                svcHost = null;
                bool openSucceeded = false;

                String address = String.Empty;

                try
                {
                    address = ManagementAddress + "/ManagementService.svc";

                    svcHost = new ServiceHost(managementService, new Uri(address));

                    // Check to see if the service host already has a ServiceMetadataBehavior
                    ServiceMetadataBehavior smb = svcHost.Description.Behaviors.Find<ServiceMetadataBehavior>();

                    // If not, add one
                    if (smb == null)
                        smb = new ServiceMetadataBehavior();

                    smb.HttpGetEnabled = true;
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

                    svcHost.Description.Behaviors.Add(smb);

                    // Add MEX endpoint
                    svcHost.AddServiceEndpoint(
                      ServiceMetadataBehavior.MexContractName,
                      MetadataExchangeBindings.CreateMexHttpBinding(),
                      "mex"
                    );

                    openSucceeded = false;

                    ServiceEndpoint sep = null;
                    if (_webhttpOrBasichttp)
                    {
                        sep = svcHost.AddServiceEndpoint(typeof(IPickerManagementInterface), new WebHttpBinding(), "Hosting");
                        sep.Behaviors.Add(new WebHttpBehavior());
                    }
                    else
                    {
                        sep = svcHost.AddServiceEndpoint(typeof(IPickerManagementInterface), new BasicHttpBinding(), "Hosting");
                    }

                    svcHost.Open();
                    openSucceeded = true;
                }
                catch (Exception ex)
                {
                    LogManager.Error("Service host failed to open " + ex.Message, ex);
                }
                finally
                {
                    if (!openSucceeded)
                    {
                        svcHost.Abort();
                    }
                }

                if (svcHost.State == CommunicationState.Opened)
                {
                    LogManager.Info("The Service Management Interface is running at " + address);
                }
                else
                {
                    LogManager.Info("Server failed to open.");
                }
            }
            #endregion

            #region read configuration file
            try
            {
                iotpicker configuration;

                Uri uripath = new System.Uri(ConfigurationManager.AppSettings["Configuration_FilePath_Main"] + RegionOrdinal.ToString() + ".xml");

                LogManager.Info(String.Format("Loading configuration path at {0}", ConfigurationManager.AppSettings["Configuration_FilePath_Main"]));

                ILoader loader = new FileLoader();
                loader.SetUri(uripath);
                var content = loader.Load();

                IParser parser = new XSDParser();
                var parsed = parser.ParseIoTConfigurationFile<iotpicker>(content, out configuration);

                if (!parsed)
                {
                    _context.configuration = null;

                    LogManager.Info("Error in parsing configuration file.");
                }
                else
                {
                    if (configuration.device != null)
                    {
                        _context.configuration = configuration;
                        _context.PickerId = configuration.device.id;
                        _context.PickerName = configuration.device.name;
                        _context.PickerNuri = IoTCommHelper.StringToNuri(_context.PickerName);

                        LogFormat.Init(_context.PickerId);
                    }
                    else
                    {
                        throw new FormatException("Missing device tag in configuration file.");
                    }

                    LogManager.Info("Configuration file read and parsed.");
                }
            }
            catch (Exception ex)
            {
                LogManager.Fatal(ex.Message, ex);

                _context.configuration = null;
            }
            #endregion

            if (_context.configuration == null)
            {
                Environment.Exit(19751109);
            }

            #region Publisher (feedback)
            QueueMessageConfiguration publisherConfiguration = null;
            if (EnableHookToCentral)
            {
                publisherConfiguration = new QueueMessageConfiguration();

                publisherConfiguration.HostName = _cm["Queue_RegionToNetwork_HostName"];
                publisherConfiguration.UserName = _cm["Queue_RegionToNetwork_UserName"];
                publisherConfiguration.Password = _cm["Queue_RegionToNetwork_Password"];
                publisherConfiguration.VirtualHost = _cm["Queue_RegionToNetwork_VirtualHost"];
                publisherConfiguration.Uri = _cm["Queue_RegionToNetwork_Uri"];
                publisherConfiguration.Topic = _cm["Queue_RegionToNetwork_Topic"];
                publisherConfiguration.Connection = _cm["Queue_RegionToNetwork_Connection"];
                publisherConfiguration.Provider = _cm["Queue_RegionToNetwork_Provider"];
                publisherConfiguration.QueueName = _cm["Queue_RegionToNetwork_QueueName"];
                publisherConfiguration.RouteKey = String.Empty;

                _publisher = Publisher<IoTCommMessage>.Instance;
                _publisher.SetPublisher(_cm["Queue_RegionToNetwork_Publisher"]);
                _publisher.Configure(publisherConfiguration);
            }
            #endregion

            #region load custom dll into current  AppDomain
            CorePluginProvider cpp = new CorePluginProvider();
            #endregion

            if (_context.configuration != null && _context.configuration.manager != null)
            {
                #region parse and build managers
                try
                {
                    _context.PickerManagers = new List<PickerManager>();

                    foreach (var m in _context.configuration.manager)
                    {
                        PickerManager manager = new PickerManager();

                        manager.UniqueId = m.UniqueId;
                        manager.UniqueName = m.UniqueName;

                        if (m.DataServer != null)
                        {
                            manager.DataServer = IoTPickerHelper.AllocateDataServer(_context.configuration,
                                m.DataServer.ServerUniqueName, m.DataServer.Uri, m.DataServer.WriterUniqueName);

                            manager.DataServer.SetProfiles(EnableCustomAgents, EnableDatabaseWriting, EnableHookToCentral);

                            manager.DataServer.SetPacketsQueue(_packetsQueue);

                            manager.DataServer.SetRole(ServerRole.Data);
                        }

                        if (m.MonitoryServer != null)
                        {
                            manager.ManagementServer = IoTPickerHelper.AllocateDataServer(_context.configuration,
                                m.MonitoryServer.ServerUniqueName, m.MonitoryServer.Uri, null);

                            manager.ManagementServer.SetPacketsQueue(_packetsQueue);

                            manager.ManagementServer.SetRole(ServerRole.Management);
                        }

                        _context.PickerManagers.Add(manager);

                        manager.Start();

                        _managers.Add(manager);

                        LogManager.Info(String.Format("Manager {0} run.", manager.UniqueName));
                    }
                }
                catch (Exception ex)
                {
                    LogManager.Error("Error in running managers: " + ex.Message, ex);
                }
                #endregion

                #region Main Thread

                _mustrun = true;

                while (_mustrun)
                {
                    #region Manage Commands
                    if (EnableManagementInterface)
                    {
                        bool dequeued = false;
                        Tuple<IoTPickerCommandName, List<object>> command = null;

                        try
                        {
                            dequeued = managementService.TryGetCommand(out command);
                        }
                        catch (Exception)
                        {
                            dequeued = false;
                        }

                        try
                        {
                            if (dequeued && command != null)
                            {
                                LogManager.Info(String.Format("Executing command {0}.", command.Item1.ToString()));

                                switch (command.Item1)
                                {
                                    case IoTPickerCommandName.Stop:
                                        {
                                            LogManager.Info("Stopping");
                                            foreach (var m in _managers)
                                            {
                                                m.Stop();
                                            }

                                            LogManager.Info("Waiting");
                                            foreach (var m in _managers)
                                            {
                                                m.Wait();
                                            }

                                            _mustrun = false;
                                        }
                                        break;
                                    case IoTPickerCommandName.AskForStatistics:
                                        {
                                            IoTWork.Infrastructure.Statistics.Statistics Statistics = new Infrastructure.Statistics.Statistics();

                                            foreach (var m in _managers)
                                            {
                                                Statistics.Add(m.GetStatistics());
                                            }

                                            List<Note> _protocolNotes = new List<Note>();
                                            foreach (var s in Statistics)
                                            {
                                                string code = s.GetModule();
                                                string name = s.GetName();
                                                string value = s.GetValue();
                                                NoteDomain domain = s.GetDomain();

                                                _protocolNotes.Add(new Note() { Domain = domain, Module = code, Name = name, Value = value });
                                            }

                                            GivePickerFeedback(IoTPickerMessageName.Statistics, _protocolNotes, _context.PickerNuri, IoTCommStatus.None, IoTCommLogLevel.Info);
                                        }
                                        break;
                                    case IoTPickerCommandName.AskForErrors:
                                        {
                                            ErrorResume Resume = new ErrorResume();

                                            foreach (var m in _managers)
                                            {
                                                Resume.Add(m.GetErrors());
                                            }

                                            List<Note> _protocolNotes = new List<Note>();
                                            foreach (var s in Resume)
                                            {
                                                DateTime when = s.GetWhen();
                                                string code = s.GetModule();
                                                string name = s.GetMessage();
                                                string value = SerializerHelper.BinarySerialize(s.GetValue());
                                                NoteDomain domain = NoteDomain.Error;

                                                _protocolNotes.Add(new Note() { When = when, Module = code, Name = name, Value = value });
                                            }

                                            GivePickerFeedback(IoTPickerMessageName.Errors, _protocolNotes, _context.PickerNuri, IoTCommStatus.None, IoTCommLogLevel.Info);
                                        }
                                        break;
                                    case IoTPickerCommandName.AskForAlive:
                                        {
                                            SendAlive(nuri);
                                        }
                                        break;
                                    case IoTPickerCommandName.AskForUpTime:
                                        {
                                            SendUpTime(nuri, StartedOn);
                                        }
                                        break;
                                }

                                Thread.Sleep(100);
                            }
                            else
                                Thread.Sleep(250);
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error("Error in excuting command: " + ex.Message, ex);
                        }
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                    #endregion

                    #region Manage Packets
                    if (EnableHookToCentral)
                    {
                        Boolean dequeued = false;
                        IIotPacket packet = null;

                        try
                        {
                            dequeued = _packetsQueue.TryDequeue(out packet);
                        }
                        catch (Exception)
                        {
                            dequeued = false;
                        }

                        try
                        {
                            if (dequeued && packet != null)
                            {
                                GiveDeviceFeedback(packet, nuri, IoTCommStatus.None, IoTCommLogLevel.Info);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogManager.Error("Error in forwarding packet: " + ex.Message, ex);
                        }
                    }
                    #endregion
                }

                LogManager.Info("Main thread exited.");
                #endregion

                #region Self Hosted Web Service
                if (EnableManagementInterface)
                {
                    bool closeSucceed = false;
                    try
                    {
                        svcHost.Close();
                        closeSucceed = true;
                        LogManager.Info("Management interface closed.");
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("Service failed to close. Exception {0}", ex.ToString());
                    }
                    finally
                    {
                        if (!closeSucceed)
                        {
                            svcHost.Abort();
                        }
                    }
                }
                #endregion

                LogManager.Info("Bye Bye!!!");
                Environment.Exit(0);
            }
        }

        private void SendAlive(IoTCommNuri nuri)
        {
            nuri.Layer = IoTCommNuriLayer.Region;

            IoTCommMessage message = new IoTCommMessage();
            message.Age = 0;
            message.Title = IoTPickerMessageName.Alive.ToString();
            message.Message = String.Empty;
            message.Nuri = nuri;
            message.Source = "Picker";
            message.Status = IoTCommStatus.Success;
            message.Timestamp = DateTime.Now;
            message.Level = IoTCommLogLevel.Info;
            _publisher.Publish(message);
        }

        private void SendUpTime(IoTCommNuri nuri, DateTime startedOn)
        {
            Double elapsed = (DateTime.Now - startedOn).TotalMilliseconds;

            nuri.Layer = IoTCommNuriLayer.Region;

            IoTCommMessage message = new IoTCommMessage();
            message.Age = 0;
            message.Title = IoTPickerMessageName.UpTime.ToString();
            message.Message = SerializerHelper.XmlSerialize(elapsed);
            message.Nuri = nuri;
            message.Source = "Picker";
            message.Status = IoTCommStatus.Success;
            message.Timestamp = DateTime.Now;
            message.Level = IoTCommLogLevel.Info;
            _publisher.Publish(message);
        }

        private void GiveDeviceFeedback(IIotPacket Packet, IoTCommNuri nuri, IoTCommStatus Status, IoTCommLogLevel Level)
        {
            nuri.Layer = IoTCommNuriLayer.Device;

            IoTCommMessage message = new IoTCommMessage();
            message.Age = 0;
            message.Title = "DevicePacket";
            message.Message = SerializerHelper.XmlSerialize(Packet);
            message.Nuri = nuri;
            message.Source = "Picker";
            message.Status = Status;
            message.Timestamp = DateTime.Now;
            message.Level = Level;
            _publisher.Publish(message);
        }

        private void GivePickerFeedback(IoTPickerMessageName Title, Object obj, IoTCommNuri nuri, IoTCommStatus Status, IoTCommLogLevel Level)
        {
            nuri.Layer = IoTCommNuriLayer.Region;

            IoTCommMessage message = new IoTCommMessage();
            message.Age = 0;
            message.Title = Title.ToString();
            message.Message = SerializerHelper.XmlSerialize(obj);
            message.Nuri = nuri;
            message.Source = "Picker";
            message.Status = Status;
            message.Timestamp = DateTime.Now;
            message.Level = Level;
            _publisher.Publish(message);
        }
    }
}
