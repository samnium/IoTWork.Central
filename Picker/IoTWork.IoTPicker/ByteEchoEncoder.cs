using System;
using System.Collections.Generic;
using System.CommunicationFramework.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.IoTPicker
{
    public class ByteEchoEncoder : IMessageEncoder<byte[]>
    {
        public byte[] DecodeMessage(byte[] buffer, int index, int size)
        {
            return buffer;
        }

        public int EncodeMessage(byte[] message, byte[] buffer, int index)
        {
            buffer = message;
            return buffer.Length;
        }
    }

}
