using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.IoTPicker.Servers;

namespace IoTWork.IoTPicker.Interfaces
{
    internal interface IChannelServer
    {
        IServer server { get; set; }
        ushort UniqueId { get; set; }
        string UniqueName { get; set; }

        void SetUri(string uri);
    }
}
