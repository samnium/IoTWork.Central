using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.IoTPicker.Writers;
using IoTWork.Protocol.Types;

namespace IoTWork.IoTPicker.Interfaces
{
    internal class ChannelWriter : IChannelWriter
    {
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

        public IWriter writer
        {
            get;
            set;
        }
    }
}
