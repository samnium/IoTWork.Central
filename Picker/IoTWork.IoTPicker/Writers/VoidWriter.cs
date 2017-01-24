using IoTWork.IoTPicker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.Protocol.Types;

namespace IoTWork.IoTPicker.Writers
{
    internal class VoidWriter : IWriter
    {
        public void Write(ushort serviceCode, ushort packetCode, DateTime sentAt, ulong sequenceNumber, string sourceAdrress, string region, string deviceUniqueAddress, Payload p)
        {
            return;
        }
    }
}
