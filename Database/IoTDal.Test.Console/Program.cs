using IoTDal.Model;
using IoTDal.Model.DB.IoTWork.Data;
using IoTDal.Model.DB.IoTWork.Network;
using IoTDal.Provider.Npgsql;
using IoTWork.Configuration;
using IoTWork.Connections;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// http://lvasquez.github.io/2014/11/24/EntityFramework-PostgreSql/
// http://www.bricelam.net/2012/10/entity-framework-on-postgresql.html
// http://www.jasoncavett.com/blog/postgresql-and-entity-framework-6-code-first/
// http://www.entityframeworktutorial.net/code-first/simple-code-first-example.aspx

namespace IoTDal.App.InitDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to IoTWork database initialization tool.");
            System.Console.WriteLine();
            System.Console.WriteLine("Please select the database to initialize.");
            System.Console.WriteLine("1. Network");
            System.Console.WriteLine("2. Data");
            System.Console.WriteLine(">");

            Int32 selection = Int32.Parse(System.Console.ReadLine());
                
            switch(selection)
            {
                case 1:
                    {
                        IoTWorkNetwork();
                    }
                    break;
                case 2:
                    {
                        IoTWorkData();
                    }
                    break;
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Initialization done. Press <enter> twice!");
            System.Console.ReadLine();
            System.Console.ReadLine();
        }

        static void IoTWorkData()
        {
            System.Console.WriteLine();
            System.Console.WriteLine("Database IoTWork.Data will be created if not exist");

            DbConnectionManager connmanager = DbConnectionManager.Instance;

            var dbconn = connmanager.IoTWorkDataConnection(false);

            try
            {
                using (var db = new DataContext(dbconn))
                {
                    Sample s = new Sample();
                    s.TriggeredOn = s.ReceivedOn = s.SentOn = s.CreatedOn = DateTime.Now;
                    s.BinValue = s.CsvValue = s.XmlValue = String.Empty;
                    s.OrdinalForChain = s.OrdinalForDevice = s.OrdinalForNetwork = s.OrdinalForRegion = s.OrdinalForRing = s.OrdinalForSensor = 0;
                    db.Samples.Add(s);
                    db.SaveChanges();

                    SampleR1 s1 = new SampleR1();
                    s1.TriggeredOn = s1.ReceivedOn = s1.SentOn = s1.CreatedOn = DateTime.Now;
                    s1.BinValue = s1.CsvValue = s1.XmlValue = String.Empty;
                    s1.OrdinalForChain = s1.OrdinalForDevice = s1.OrdinalForNetwork = s1.OrdinalForRegion = s1.OrdinalForRing = s1.OrdinalForSensor = 0;
                    db.SamplesR1.Add(s1);
                    db.SaveChanges();

                    SampleR1_Archive s1a = new SampleR1_Archive();
                    s1a.TriggeredOn = s1a.ReceivedOn = s1a.SentOn = s1a.CreatedOn = DateTime.Now;
                    s1a.BinValue = s1a.CsvValue = s1a.XmlValue = String.Empty;
                    s1a.OrdinalForChain = s1a.OrdinalForDevice = s1a.OrdinalForNetwork = s1a.OrdinalForRegion = s1a.OrdinalForRing = s1a.OrdinalForSensor = 0;
                    db.SamplesR1_Archive.Add(s1a);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    System.Console.WriteLine(ex.InnerException.Message);
                System.Console.ReadLine();
                System.Console.ReadLine();
            }
        }

        static void IoTWorkNetwork()
        {
            #region Read Defaults
            var NetworkBaseName = System.Configuration.ConfigurationManager.AppSettings["NetworkBaseName"];
            var NetworkNumber = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["NetworkNumber"]);

            var RegionBaseName = System.Configuration.ConfigurationManager.AppSettings["RegionBaseName"];
            var RegionPerBranch = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["RegionPerBranch"]);

            var DataServerBaseName = System.Configuration.ConfigurationManager.AppSettings["DataServerBaseName"];

            var ManagementServerBaseName = System.Configuration.ConfigurationManager.AppSettings["ManagementServerBaseName"];

            var RingBaseName = System.Configuration.ConfigurationManager.AppSettings["RingBaseName"];
            var RingPerBranch = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["RingPerBranch"]);

            var DeviceBaseName = System.Configuration.ConfigurationManager.AppSettings["DeviceBaseName"];
            var DevicePerBranch = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["DevicePerBranch"]);

            var ChainBaseName = System.Configuration.ConfigurationManager.AppSettings["ChainBaseName"];
            var ChainPerDevice = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["ChainPerDevice"]);

            var SensorBaseName = System.Configuration.ConfigurationManager.AppSettings["SensorBaseName"];

            var TriggerBaseName = System.Configuration.ConfigurationManager.AppSettings["TriggerBaseName"];

            var PipeBaseName = System.Configuration.ConfigurationManager.AppSettings["PipeBaseName"];
            var PipePerChain = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["PipePerChain"]);

            var DefaultBindingPacketsThrottling = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultBindingPacketsThrottling"]);
            var DefaultBindingPacketsMaxIntervalTimeout = Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["DefaultBindingPacketsMaxIntervalTimeout"]);

            #endregion

            System.Console.WriteLine();
            System.Console.WriteLine("Database IoTWork.Network will be created if not exist");

            DbConnectionManager connmanager = DbConnectionManager.Instance;
            var dbconn = connmanager.IoTWorkNetworkConnection(false);

            try
            {
                using (var db = new NetworkContext(dbconn))
                {
                    foreach(var k in System.Configuration.ConfigurationManager.AppSettings.AllKeys)
                    {
                        Parameter p = new Parameter();
                        p.Key = k;
                        p.Value = System.Configuration.ConfigurationManager.AppSettings[k];
                        db.Parameters.Add(p);
                        db.SaveChanges();

                        System.Console.WriteLine("Added parameter {0}", p.Key);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    System.Console.WriteLine(ex.InnerException.Message);
                System.Console.ReadLine();
                System.Console.ReadLine();
            }

            Thread.Sleep(2000);

            DateTime SetUpDateTime = DateTime.Now;
            List<string> NUris = new List<string>();

            try
            {
                using (var db = new NetworkContext(dbconn))
                {
                    for (var nid = 1; nid <= NetworkNumber; nid++)
                    {
                        var network = new Network();
                        network.Name = NetworkBaseName + " " + nid;
                        network.SetUpDate = SetUpDateTime;
                        network.NUri = nid.ToString();
                        network.Ordinal = nid;
                        network.Status = NetworkStatus.NotActive;
                        db.Networks.Add(network);
                        db.SaveChanges();
                        var key_networkId = network.Id;

                        System.Console.WriteLine("Added network {0} :: {1}", network.Name, key_networkId);
                        NUris.Add(network.NUri);

                        var binding = new Binding();
                        binding.PacketsThrottling = DefaultBindingPacketsThrottling;
                        binding.PacketsMaxIntervalTimeout = DefaultBindingPacketsMaxIntervalTimeout;
                        db.Bindings.Add(binding);
                        db.SaveChanges();
                        var key_defaultBindingId = binding.Id;

                        System.Console.WriteLine("Default Binding {0}", key_defaultBindingId);

                        for (var rid = 1; rid <= RegionPerBranch; rid++)
                        {
                            var dataServer = new Server();
                            dataServer.Name = DataServerBaseName + " " + rid;
                            dataServer.NUri = nid.ToString() + "." + rid.ToString() + ".ds";
                            dataServer.Status = ServerStatus.NotActive;
                            dataServer.Type = ServerType.Data;
                            dataServer.Uri = String.Empty;
                            dataServer.Protocol = Protocol.Undefined;
                            db.Servers.Add(dataServer);
                            db.SaveChanges();
                            var key_dataServerId = dataServer.Id;

                            System.Console.WriteLine("Added dataServer {0} :: {1}", dataServer.Name, key_dataServerId);

                            var managementServer = new Server();
                            managementServer.Name = ManagementServerBaseName + " " + rid;
                            managementServer.NUri = nid.ToString() + "." + rid.ToString() + ".ms";
                            managementServer.Status = ServerStatus.NotActive;
                            managementServer.Type = ServerType.Management;
                            managementServer.Uri = String.Empty;
                            managementServer.Protocol = Protocol.Undefined;
                            db.Servers.Add(managementServer);
                            db.SaveChanges();
                            var key_managementServerId = managementServer.Id;

                            System.Console.WriteLine("Added managementServer {0} :: {1}", managementServer.Name, key_managementServerId);

                            var region = new Region();
                            region.Name = RegionBaseName + " " + rid;
                            region.NUri = nid.ToString() + "." + rid.ToString();
                            region.Ordinal = rid;
                            region.OrdinalForNetwork = nid;
                            region.SetUpDate = SetUpDateTime;
                            region.Status = RegionStatus.NotActive;
                            region.NetworkId = key_networkId;
                            region.DataServerId = key_dataServerId;
                            region.ManagementServerId = key_managementServerId;
                            db.Regions.Add(region);
                            db.SaveChanges();
                            var key_regionId = region.Id;

                            System.Console.WriteLine("Added region {0} :: {1}", region.Name, key_regionId);
                            NUris.Add(region.NUri);

                            for (var gid = 1; gid <= RingPerBranch; gid++)
                            {
                                var ring = new Ring();
                                ring.Name = RingBaseName + " " + gid;
                                ring.NUri = nid.ToString() + "." + rid.ToString() + "." + gid.ToString();
                                ring.Ordinal = gid;
                                ring.OrdinalForRegion = rid;
                                ring.OrdinalForNetwork = nid;
                                ring.SetUpDate = SetUpDateTime;
                                ring.Status = RingStatus.NotActive;
                                ring.NetworkId = key_networkId;
                                ring.RegionId = key_regionId;
                                db.Rings.Add(ring);
                                db.SaveChanges();
                                var key_ringId = ring.Id;

                                System.Console.WriteLine("Added ring {0} :: {1}", ring.Name, key_ringId);
                                NUris.Add(ring.NUri);

                                for (var did = 1; did <= DevicePerBranch; did++)
                                {
                                    var device = new Device();
                                    device.Name = DeviceBaseName + " " + did;
                                    device.NUri = nid.ToString() + "." + rid.ToString() + "." + gid.ToString() + "." + did.ToString();
                                    device.Ordinal = did;
                                    device.OrdinalForRing = gid;
                                    device.OrdinalForRegion = rid;
                                    device.OrdinalForNetwork = nid;
                                    device.SetUpDate = SetUpDateTime;
                                    device.Status = DeviceStatus.NotActive;
                                    device.Type = DeviceType.Custom;
                                    device.Latitude = 0;
                                    device.Longitude = 0;
                                    device.NetworkId = key_networkId;
                                    device.RingId = key_ringId;
                                    device.RegionId = key_regionId;
                                    device.BindingId = key_defaultBindingId;
                                    db.Devices.Add(device);
                                    db.SaveChanges();
                                    var key_deviceId = device.Id;

                                    System.Console.WriteLine("Added device {0} :: {1}", device.Name, key_deviceId);
                                    NUris.Add(device.NUri);

                                    for (var cid = 1; cid <= ChainPerDevice; cid++)
                                    {
                                        int sid = 1;
                                        int tid = 1;

                                        var chain = new Chain();
                                        chain.Name = ChainBaseName + " " + cid;
                                        chain.NUri = nid.ToString() + "." + rid.ToString() + "." + gid.ToString() + "." + did.ToString() + "." + cid.ToString();
                                        chain.Ordinal = cid;
                                        chain.OrdinalForDevice = did;
                                        chain.OrdinalForRing = gid;
                                        chain.OrdinalForRegion = rid;
                                        chain.OrdinalForNetwork = nid;
                                        chain.Status = ChainStatus.NotActive;
                                        chain.Type = ChainType.SensorListner;
                                        chain.DeviceId = key_deviceId;
                                        db.Chains.Add(chain);
                                        db.SaveChanges();

                                        var key_chainId = chain.Id;

                                        System.Console.WriteLine("Added chain {0} :: {1}", chain.Name, key_chainId);
                                        NUris.Add(chain.NUri);

                                        var sensor = new Sensor();
                                        sensor.Name = SensorBaseName + " " + cid;
                                        sensor.NUri = nid.ToString() + "." + rid.ToString() + "." + gid.ToString() + "." + did.ToString() + "." + cid.ToString() + ".s" + sid;
                                        sensor.Ordinal = sid;
                                        sensor.Status = SensorStatus.NotActive;
                                        sensor.Type = SensorType.Void;
                                        sensor.Parameter = String.Empty;
                                        sensor.ModuleName = null;
                                        sensor.ModulePath = null;
                                        sensor.DeviceId = key_deviceId;
                                        sensor.ChainId = key_chainId;
                                        db.Sensors.Add(sensor);
                                        db.SaveChanges();

                                        var key_sensorId = sensor.Id;

                                        System.Console.WriteLine("Added sensor {0} :: {1}", sensor.Name, key_deviceId);
                                        NUris.Add(sensor.NUri);

                                        var trigger = new Trigger();
                                        trigger.Name = TriggerBaseName + " " + cid;
                                        trigger.NUri = nid.ToString() + "." + rid.ToString() + "." + gid.ToString() + "." + did.ToString() + "." + cid.ToString() + ".t" + tid;
                                        trigger.Ordinal = tid;
                                        trigger.Status = TriggerStatus.NotActive;
                                        trigger.Type = TriggerType.Void;
                                        trigger.RunOnlyOnce = true;
                                        trigger.TimeoutInMilliseconds = 0;
                                        trigger.DeviceId = key_deviceId;
                                        trigger.ChainId = key_chainId;
                                        db.Triggers.Add(trigger);
                                        db.SaveChanges();

                                        var key_triggerId = trigger.Id;

                                        System.Console.WriteLine("Added trigger {0} :: {1}", trigger.Name, key_triggerId);
                                        NUris.Add(trigger.NUri);

                                        for (var pid = 1; pid <= PipePerChain; pid++)
                                        {
                                            var pipe = new Pipe();
                                            pipe.Name = PipeBaseName + " " + pid;
                                            pipe.NUri = nid.ToString() + "." + rid.ToString() + "." + gid.ToString() + "." + did.ToString() + "." + cid.ToString() + ".p" + pid;
                                            pipe.Ordinal = pid;
                                            pipe.Status = PipeStatus.NotActive;
                                            pipe.Type = PipeType.Simple;
                                            pipe.Parameter = String.Empty;
                                            pipe.ModuleName = null;
                                            pipe.ModulePath = null;
                                            pipe.DeviceId = key_deviceId;
                                            pipe.ChainId = key_chainId;
                                            db.Pipes.Add(pipe);
                                            db.SaveChanges();

                                            var key_pipeId = pipe.Id;

                                            System.Console.WriteLine("Added pipe {0} :: {1}", pipe.Name, key_pipeId);
                                            NUris.Add(pipe.NUri);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                foreach (var nuri in NUris)
                {
                    System.Console.WriteLine("{0}", nuri);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    System.Console.WriteLine(ex.InnerException.Message);
                System.Console.ReadLine();
                System.Console.ReadLine();
            }
        }
    }
}
