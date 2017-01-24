using IoTDal.Model.DB.IoTWork.Network;
using IoTDal.Provider.Npgsql;
using IoTWork.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Access
{
    public class IoTWorkNetworkDalManager : IDisposable
    {
        private NetworkContext db = null;

        public IoTWorkNetworkDalManager()
        {
            DbConnectionManager connmanager = DbConnectionManager.Instance;
            var dbconn = connmanager.IoTWorkNetworkConnection(false);
            db = new NetworkContext(dbconn);
        }

        private string _serverPath_PipeModules;
        public void SetPipeModulePath(string serverPath_PipeModules)
        {
            _serverPath_PipeModules = serverPath_PipeModules;
        }

        private string _serverPath_SensorModules;
        public void SetSensorModulePath(string serverPath_SensorModules)
        {
            _serverPath_SensorModules = serverPath_SensorModules;
        }

        public List<Parameter> GetParameters()
        {
            return db.Parameters.ToList();
        }

        public void Dispose()
        {
            db = null;
        }

        public List<Region> GetRegions(int networkOrd, bool onlyActives)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var regions = db.Regions.Where(x => x.NetworkId == network.Id && x.Status == RegionStatus.Active).ToList();

                if (regions != null && regions.Count > 0)
                {
                    foreach (var r in regions)
                    {
                        var ds = db.Servers.Where(x => x.Id == r.DataServerId).FirstOrDefault();
                        var ms = db.Servers.Where(x => x.Id == r.ManagementServerId).FirstOrDefault();

                        r.DataServer = ds;
                        r.ManagementServer = ms;
                    }
                }

                return regions ?? new List<Region>();
            }
            else return new List<Region>();
        }

        public Device GetDevice(int networkOrd, int RegionOrdinal, int DeviceOrdinal)
        {
            Device _device = null;

            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                _device = db.Devices.Where(x => x.NetworkId == network.Id && x.OrdinalForRegion == RegionOrdinal && x.Ordinal == DeviceOrdinal).FirstOrDefault();

                _device.Binding = db.Bindings.Where(x => x.Id == _device.BindingId).FirstOrDefault();
            }

            return _device;
        }

        public Region GetFullRegion(int networkOrd, int ordinal)
        {
            Region _region = null;

            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                _region = db.Regions.Where(x => x.NetworkId == network.Id && x.Ordinal == ordinal).FirstOrDefault();
                _region.DataServer = db.Servers.Where(x => x.Id == _region.DataServerId).FirstOrDefault();
                _region.ManagementServer = db.Servers.Where(x => x.Id == _region.ManagementServerId).FirstOrDefault();
                _region.Writer = db.Writers.Where(x => x.RegionId == _region.Id).FirstOrDefault();
            }

            return _region;
        }

        public Chain GetFullChain(int networkOrd, int RegionOrdinal, int DeviceOrdinal, int ChainOrdinal)
        {
            Chain _chain = null;

            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var _device = db.Devices.Where(x => x.NetworkId == network.Id && x.OrdinalForRegion == RegionOrdinal && x.Ordinal == DeviceOrdinal).FirstOrDefault();

                if (_device != null)
                {
                    _chain = db.Chains.Where(x => x.OrdinalForNetwork == networkOrd
                        && x.OrdinalForRegion == RegionOrdinal
                        && x.OrdinalForRing == _device.OrdinalForRing
                        && x.OrdinalForDevice == DeviceOrdinal
                        && x.Ordinal == ChainOrdinal).FirstOrDefault();

                    if (_chain != null)
                    {
                        _chain.Sensor = db.Sensors.Where(x => x.ChainId == _chain.Id).FirstOrDefault();

                        _chain.Trigger = db.Triggers.Where(x => x.ChainId == _chain.Id).FirstOrDefault();

                        _chain.Pipes = db.Pipes.Where(x => x.ChainId == _chain.Id).ToList().OrderBy(x => x.Ordinal).ToList();
                    }
                }
            }

            return _chain;
        }

        public List<Device> GetDevices(int networkOrd, bool onlyActives)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var devices = db.Devices.Where(x => x.NetworkId == network.Id && x.Status == DeviceStatus.Active).ToList();

                foreach (var d in devices)
                {
                    d.Binding = db.Bindings.Where(x => x.Id == d.BindingId).FirstOrDefault();
                }

                return devices ?? new List<Device>();
            }
            else return new List<Device>();
        }

        public void UpdateRegions(int networkOrd, Region region)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var dbregion = db.Regions.Where(x => x.NetworkId == network.Id && x.Ordinal == region.Ordinal).FirstOrDefault();

                if (dbregion != null)
                {
                    bool update_dataserver_key = false;
                    bool update_managementserver_key = false;

                    int key_dataServerId = 0;
                    int key_managementServerId = 0;

                    Server dbdataserver = null;
                    if (region.DataServer != null)
                    {
                        if (region.DataServer.Id > 0)
                            dbdataserver = db.Servers.Where(x => x.Id == region.DataServer.Id).FirstOrDefault();
                        else
                            dbdataserver = db.Servers.Where(x => x.Id == dbregion.DataServerId).FirstOrDefault();
                    }

                    Server dbmanagementserver = null;
                    if (region.ManagementServer != null)
                    {
                        if (region.ManagementServer.Id > 0)
                            dbmanagementserver = db.Servers.Where(x => x.Id == region.ManagementServer.Id).FirstOrDefault();
                        else
                            dbmanagementserver = db.Servers.Where(x => x.Id == dbregion.ManagementServerId).FirstOrDefault();

                    }


                    if (dbdataserver != null)
                    {
                        dbdataserver.Protocol = region.DataServer.Protocol;
                        dbdataserver.Uri = region.DataServer.Uri;
                    }
                    else
                    {
                        var dsbasename = db.Parameters.Where(x => x.Key == "DataServerBaseName").FirstOrDefault();

                        var dataServer = new Server();
                        dataServer.Name = (dsbasename != null ? dsbasename.Value : "DS") + " " + dbregion.Ordinal;
                        dataServer.NUri = networkOrd.ToString() + "." + dbregion.Ordinal.ToString() + ".ds";
                        dataServer.Status = ServerStatus.Active;
                        dataServer.Type = ServerType.Data;
                        dataServer.Protocol = region.DataServer.Protocol;
                        dataServer.Uri = region.DataServer.Uri;
                        db.Servers.Add(dataServer);
                        db.SaveChanges();
                        key_dataServerId = dataServer.Id;

                        update_dataserver_key = true;
                    }
                    db.SaveChanges();


                    if (dbmanagementserver != null)
                    {
                        dbmanagementserver.Protocol = region.ManagementServer.Protocol;
                        dbmanagementserver.Uri = region.ManagementServer.Uri;
                    }
                    else
                    {
                        var msbasename = db.Parameters.Where(x => x.Key == "ManagementServerBaseName").FirstOrDefault();

                        var managementServer = new Server();
                        managementServer.Name = (msbasename != null ? msbasename.Value : "MS") + " " + dbregion.Ordinal;
                        managementServer.NUri = networkOrd.ToString() + "." + dbregion.Ordinal.ToString() + ".ms";
                        managementServer.Status = ServerStatus.Active;
                        managementServer.Type = ServerType.Management;
                        managementServer.Protocol = region.ManagementServer.Protocol;
                        managementServer.Uri = region.ManagementServer.Uri;
                        db.Servers.Add(managementServer);
                        db.SaveChanges();
                        key_managementServerId = managementServer.Id;

                        update_managementserver_key = true;
                    }
                    db.SaveChanges();

                    dbregion.Name = region.Name;
                    dbregion.Status = region.Status;
                    if (update_dataserver_key)
                        dbregion.DataServerId = key_dataServerId;
                    if (update_managementserver_key)
                        dbregion.ManagementServerId = key_managementServerId;
                    db.SaveChanges();

                }
            }
        }

        public void UpdateWriterForRegion(int networkOrd, int RegionOrdinal, Writer writer)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var dbregion = db.Regions.Where(x => x.NetworkId == network.Id && x.Ordinal == RegionOrdinal).FirstOrDefault();

                if (dbregion != null)
                {
                    var dbwriter = db.Writers.Where(x => x.RegionId == dbregion.Id).FirstOrDefault();

                    if (dbwriter != null)
                    {
                        dbwriter.Type = writer.Type;
                        dbwriter.ModuleName = writer.ModuleName;
                        dbwriter.ModulePath = writer.ModulePath;
                        dbwriter.Parameter = writer.Parameter;
                        db.SaveChanges();
                    }
                    else
                    {
                        Writer newwriter = new Writer();
                        newwriter.Type = writer.Type;
                        newwriter.ModuleName = writer.ModuleName;
                        newwriter.ModulePath = writer.ModulePath;
                        newwriter.Parameter = writer.Parameter;
                        newwriter.RegionId = dbregion.Id;
                        db.Writers.Add(newwriter);
                        db.SaveChanges();
                    }
                }
            }
        }

        public void UpdateBindingForDevice(int networkOrd, int RegionOrdinal, int DeviceOrdinal, int PacketsThrottling, int PacketsMaxIntervalTimeout)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var _device = db.Devices.Where(x => x.NetworkId == network.Id && x.OrdinalForRegion == RegionOrdinal && x.Ordinal == DeviceOrdinal).FirstOrDefault();

                var _binding = db.Bindings.Where(x => x.PacketsThrottling == PacketsThrottling && x.PacketsMaxIntervalTimeout == PacketsMaxIntervalTimeout).FirstOrDefault();

                int key_defaultBindingId = 0;

                if (_binding == null)
                {
                    var binding = new Binding();
                    binding.PacketsThrottling = PacketsThrottling;
                    binding.PacketsMaxIntervalTimeout = PacketsMaxIntervalTimeout;
                    db.Bindings.Add(binding);
                    db.SaveChanges();
                    key_defaultBindingId = binding.Id;
                }
                else
                    key_defaultBindingId = _binding.Id;

                _device.BindingId = key_defaultBindingId;
                db.SaveChanges();
            }
        }

        public void UpdateFullChain(int networkOrd, int RegionOrdinal, int DeviceOrdinal, int ChainOrdinal, Chain chain)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var _device = db.Devices.Where(x => x.NetworkId == network.Id && x.OrdinalForRegion == RegionOrdinal && x.Ordinal == DeviceOrdinal).FirstOrDefault();

                if (_device != null)
                {
                    var _chain = db.Chains.Where(x => x.OrdinalForNetwork == networkOrd
                        && x.OrdinalForRegion == RegionOrdinal
                        && x.OrdinalForRing == _device.OrdinalForRing
                        && x.OrdinalForDevice == DeviceOrdinal
                        && x.Ordinal == ChainOrdinal).FirstOrDefault();

                    if (_chain != null)
                    {
                        var _sensor = db.Sensors.Where(x => x.ChainId == _chain.Id).FirstOrDefault();

                        _sensor.Type = chain.Sensor.Type;

                        if (chain.Sensor.Type == SensorType.Custom)
                        {
                            _sensor.ModuleName = chain.Sensor.ModuleName;
                            _sensor.ModulePath = _serverPath_SensorModules + "\\" + chain.Sensor.ModuleName;
                        }
                        else
                        {
                            _sensor.ModuleName = null;
                            _sensor.ModulePath = null;
                        }

                        if (chain.Sensor.Type == SensorType.Custom
                            || chain.Sensor.Type == SensorType.DateTime
                            || chain.Sensor.Type == SensorType.Random)
                        {
                            _sensor.Parameter = chain.Sensor.Parameter;
                        }

                        db.SaveChanges();


                        var _trigger = db.Triggers.Where(x => x.ChainId == _chain.Id).FirstOrDefault();
                        _trigger.Type = chain.Trigger.Type;
                        _trigger.TimeoutInMilliseconds = chain.Trigger.TimeoutInMilliseconds;
                        _trigger.RunOnlyOnce = chain.Trigger.RunOnlyOnce;

                        db.SaveChanges();

                        var _pipes = db.Pipes.Where(x => x.ChainId == _chain.Id).ToList().OrderBy(x => x.Ordinal).ToList();

                        for (int i = 0; i < _pipes.Count; i++)
                        {
                            var _pipe = _pipes[i];
                            var chainpipe = chain.Pipes.Where(x => x.Ordinal == _pipe.Ordinal).FirstOrDefault();

                            if (chainpipe != null)
                            {
                                if (chainpipe.Type == PipeType.Custom)
                                {
                                    _pipe.ModuleName = chainpipe.ModuleName;
                                    _pipe.ModulePath = _serverPath_PipeModules + "\\" + chainpipe.ModuleName;
                                }
                                else
                                {
                                    _pipe.ModuleName = null;
                                    _pipe.ModulePath = null;
                                }

                                _pipe.Type = chainpipe.Type;

                                if (chainpipe.Type == PipeType.Custom
                                    || chainpipe.Type == PipeType.Delay)
                                {
                                    _pipe.Parameter = chainpipe.Parameter;
                                }

                                db.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        public void UpdateDevices(int networkOrd, Device device)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var dbdevice = db.Devices.Where(x => x.NetworkId == network.Id && x.Ordinal == device.Ordinal && x.OrdinalForRegion == device.OrdinalForRegion).FirstOrDefault();

                if (dbdevice != null)
                {
                    dbdevice.Name = device.Name;
                    dbdevice.Status = device.Status;
                    dbdevice.Latitude = device.Latitude;
                    dbdevice.Longitude = device.Longitude;
                    db.SaveChanges();
                }
            }
        }

        public void EnableRegion(int networkOrd, int RegionOrdinal)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var dbregion = db.Regions.Where(x => x.NetworkId == network.Id && x.Ordinal == RegionOrdinal).FirstOrDefault();
                dbregion.Status = RegionStatus.Active;
                db.SaveChanges();
            }
        }

        public void DisableRegion(int networkOrd, int RegionOrdinal)
        {
            var network = db.Networks.Where(x => x.Ordinal == networkOrd).FirstOrDefault();
            if (network != null)
            {
                var dbregion = db.Regions.Where(x => x.NetworkId == network.Id && x.Ordinal == RegionOrdinal).FirstOrDefault();
                dbregion.Status = RegionStatus.NotActive;
                db.SaveChanges();
            }
        }
    }
}
