using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.IoTPicker.Writers;
using IoTWork.Protocol.Types;

namespace IoTWork.IoTPicker.Interfaces
{
    internal interface IChannelWriter
    {
        IWriter writer { get; set; }
        ushort UniqueId { get; set; }
        string UniqueName { get; set; }
    }
}
