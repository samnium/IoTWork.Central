using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.XML;
using IoTWork.IoTPicker.Interfaces;
using IoTWork.IoTPicker.Servers;
using IoTWork.IoTPicker.Writers;
using IoTWork.IoTPicker.Management;
using System.Reflection;

namespace IoTWork.IoTPicker.Helpers
{
    internal static class IoTPickerHelper
    {
        internal static ServerManager AllocateDataServer(iotpicker configuration, string serverUniqueName, string uri, string writerUniqueName)
        {
            ServerManager smanager = new ServerManager();

            IChannelServer server = null;
            IChannelWriter writer = null;

            if (!String.IsNullOrEmpty(serverUniqueName) && configuration.server != null)
            {
                var serverconf = configuration.server.Where(x => x.UniqueName == serverUniqueName).FirstOrDefault();
                if (serverconf != null)
                {
                    server = IoTPickerHelper.AllocateServer(serverconf);
                }
            }

            if (!String.IsNullOrEmpty(writerUniqueName) && configuration.writer != null)
            {
                var writerconf = configuration.writer.Where(x => x.UniqueName == writerUniqueName).FirstOrDefault();
                if (writerconf != null)
                {
                    writer = IoTPickerHelper.AllocateWriter(writerconf);
                }
            }

            if (server != null)
                server.SetUri(uri);

            smanager.Server = server;
            smanager.Writer = writer;

            return smanager;
        }

        private static IChannelServer AllocateServer(iotpickerServer conf)
        {
            IChannelServer server = new ChannelServer();
            
            switch (conf.type)
            {
                case "udp":
                    {
                        server.server = new UdpServer();
                    }
                    break;
                case "tcp":
                    {
                        server.server = new UdpServer();
                    }
                    break;
            }

            server.UniqueId = conf.UniqueId;
            server.UniqueName = conf.UniqueName;

            return server;
        }


        private static IChannelWriter AllocateWriter(iotpickerWriter conf)
        {
            IChannelWriter writer = new ChannelWriter();

            switch (conf.type)
            {
                case "console":
                    {
                        writer.writer = new ConsoleWriter();
                    }
                    break;
                case "void":
                    {
                        writer.writer = new VoidWriter();
                    }
                    break;
                case "csv":
                    {
                        writer.writer = new VoidWriter();
                    }
                    break;
                case "custom":
                    {
                        writer.writer = new CustomWriter();
                        ((IWriterInitializer)writer.writer).Init(conf.LibraryPath, conf.Init);
                    }
                    break;
            }

            writer.UniqueId = conf.UniqueId;
            writer.UniqueName = conf.UniqueName;

            return writer;
        }
    }
}
