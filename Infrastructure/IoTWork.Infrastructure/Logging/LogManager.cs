using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure
{
    public static class LogManager
    {
        // http://stackoverflow.com/questions/166438/what-would-a-log4net-wrapper-class-look-like

        private static log4net.ILog _log = null;
        //private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Init(String ConfigurationFilePath, String AppName)
        {
            FileInfo finfo = new FileInfo(ConfigurationFilePath);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(finfo);
            _log = log4net.LogManager.GetLogger(AppName);
        }

        public static void Debug(object message, Exception ex = null)
        {
            if (_log.IsDebugEnabled)
            {
                if (ex == null)
                {
                    _log.Debug(message);
                }
                else
                {
                    _log.Debug(message, ex);
                }
            }
        }

        public static void Info(object message, Exception ex = null)
        {
            if (_log.IsInfoEnabled)
            {
                if (ex == null)
                {
                    _log.Info(message);
                }
                else
                {
                    _log.Info(message, ex);
                }
            }
        }

        public static void Warn(object message, Exception ex = null)
        {
            if (_log.IsWarnEnabled)
            {
                if (ex == null)
                {
                    _log.Warn(message);
                }
                else
                {
                    _log.Warn(message, ex);
                }
            }
        }

        public static void Error(object message, Exception ex = null)
        {
            if (_log.IsErrorEnabled)
            {
                if (ex == null)
                {
                    _log.Error(message);
                }
                else
                {
                    _log.Error(message, ex);
                }
            }
        }

        public static void Fatal(object message, Exception ex = null)
        {
            if (_log.IsFatalEnabled)
            {
                if (ex == null)
                {
                    _log.Fatal(message);
                }
                else
                {
                    _log.Fatal(message, ex);
                }
            }
        }
    }
}
