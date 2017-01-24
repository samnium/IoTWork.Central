using IoTDal.Access;
using IoTDal.Model.DB.IoTWork.Network;
using Npgsql;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace IoTWork.Configuration
{
    public class ConfigurationManager
    {
        ConcurrentDictionary<string, string> _parameters;
        DateTime _lastread;
        Object _locker;
        Timer _timer;
        Int32 _minutes;

        public ConfigurationManager()
        {
            _minutes = 5;
            _parameters = new ConcurrentDictionary<string, string>();
            _locker = new object();
            AddParameters();
            _timer = new Timer(_minutes * 60 * 1000 + 5000);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            _timer.Enabled = true;
            _timer.Start();
        }

        private static readonly Lazy<ConfigurationManager> instance = new Lazy<ConfigurationManager>();

        public static ConfigurationManager Instance
        {
            get { return instance.Value; }
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateParameters(false);
        }

        private void AddParameters()
        {
            using (var manager = new IoTWorkNetworkDalManager())
            {
                var _parametersfromdb = manager.GetParameters();
                foreach (var p in _parametersfromdb)
                {
                    _parameters.TryAdd(p.Key, p.Value);
                }
                _lastread = DateTime.Now;
            }
        }

        private void UpdateParameters(bool force)
        {
            _timer.Stop();
            lock (_locker)
            {
                try
                {
                    if (force || ((DateTime.Now - _lastread).TotalMinutes > _minutes))
                    {
                        using (var manager = new IoTWorkNetworkDalManager())
                        {
                            var _parametersfromdb = manager.GetParameters();
                            foreach (var p in _parametersfromdb)
                            {
                                _parameters.TryUpdate(p.Key, p.Value, p.Value);
                            }
                            _lastread = DateTime.Now;
                        }
                    }

                }
                catch (Exception)
                {

                }
            }
            _timer.Start();
        }

        public Int32 NetworkNumber()
        {
            return Int32.Parse(_parameters["NetworkNumber"]);
        }

        public Int32 RegionPerBranch()
        {
            return Int32.Parse(_parameters["RegionPerBranch"]);
        }

        public Int32 RingPerBranch()
        {
            return Int32.Parse(_parameters["RingPerBranch"]);
        }

        public Int32 GetNetworkNumber()
        {
            return Int32.Parse(_parameters["NetworkNumber"]);
        }

        public Int32 DevicePerBranch()
        {
            return Int32.Parse(_parameters["DevicePerBranch"]);
        }

        public Int32 ChainPerDevice()
        {
            return Int32.Parse(_parameters["ChainPerDevice"]);
        }

        public Int32 PipePerChain()
        {
            return Int32.Parse(_parameters["PipePerChain"]);
        }

        public String ServerPath_SensorModules
        {
            get
            {
                return _parameters["ServerPath_SensorModules"];
            }
        }

        public String ServerPath_PipeModules
        {
            get
            {
                return _parameters["ServerPath_PipeModules"];
            }
        }

        public String ServerPath_WriterModules
        {
            get
            {
                return _parameters["ServerPath_WriterModules"];
            }
        }

        public String ServerPath_PickerAppPath
        {
            get
            {
                return _parameters["ServerPath_PickerAppPath"];
            }
        }

        public String ServerPath_PickerAppName
        {
            get
            {
                return _parameters["ServerPath_PickerAppName"];
            }
        }

        public string NetworkManager_BaseManagementAddress
        {
            get
            {
                return _parameters["NetworkManager_BaseManagementAddress"];
            }
        }

        public string NetworkManager_BaseManagementPort
        {
            get
            {
                return _parameters["NetworkManager_BaseManagementPort"];
            }
        }

        public string ServerPath_NetworkManagerLogConfigurationFile
        {
            get
            {
                return _parameters["ServerPath_NetworkManagerLogConfigurationFile"];
            }
        }

        public string ServerPath_Configuration
        {
            get
            {
                return _parameters["ServerPath_Configuration"];
            }
        }

        public string RegionPickers_BaseManagementAddress
        {
            get
            {
                return _parameters["RegionPickers_BaseManagementAddress"];
            }
        }

        public int RegionPickers_BaseManagementPort
        {
            get
            {
                return Int32.Parse(_parameters["RegionPickers_BaseManagementPort"]);
            }
        }

        public string this[string key]
        {
            get
            {
                return _parameters[key];
            }
        }
    }
}
