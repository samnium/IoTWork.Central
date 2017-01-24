using IoTWork.Protocol;
using IoTWork.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Interfaces
{
    public interface IPacketManagerInput
    {
        ICompressor SetCompressor();

        IFormatter<IotPacket> SetPacketFormatter();

        IFormatter<Payload> SetPayloadFormatter();

        ISigner<IotPacket> SetPacketSigner();

        ISigner<Payload> SetPayloadSigner();

        IIotPacket Decode(byte[] buffer);
    }
}