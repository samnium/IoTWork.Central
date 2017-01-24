using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.Protocol.Types;

namespace IoTWork.IoTPicker.Interfaces
{
    internal interface IWriter
    {
        void Write(ushort serviceCode, ushort packetCode, DateTime sentAt, ulong sequenceNumber, string sourceAdrress, string region, string deviceUniqueAddress, Payload p);
    }

    internal interface IWriterInitializer
    {
        void Init(String LibraryPath, String InitParameter);
    }
}
