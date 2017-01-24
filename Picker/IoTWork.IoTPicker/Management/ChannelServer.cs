using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.IoTPicker.Servers;

namespace IoTWork.IoTPicker.Interfaces
{
    internal class ChannelServer : IChannelServer
    {
        public IServer server
        {
            get;
            set;
        }

        public ushort UniqueId
        {
            get;
            set;
        }

        public string UniqueName
        {
            get;
            set;
        }

        void IChannelServer.SetUri(string uristr)
        {
            Uri uri = new Uri(uristr);
            server.SetUri(uri);
        }
    }
}
