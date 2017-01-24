using IoTWork.IoTPicker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.Protocol.Types;

namespace IoTWork.IoTPicker.Writers
{
    internal class ConsoleWriter : IWriter
    {
        public void Write(ushort serviceCode, ushort packetCode, DateTime sentAt, ulong sequenceNumber, string sourceAdrress, string region, string deviceUniqueAddress, Payload p)
        {
            System.Console.WriteLine("{0}.{1} {2} {3} {4}", serviceCode, packetCode, deviceUniqueAddress, sentAt, sequenceNumber);
        }
    }
}
