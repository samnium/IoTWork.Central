using IoTWork.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Protocol
{
    public class ExtendedHeader : Header
    {
        public ServiceCodes SCode { get; set; }

        public PacketCodes PCode { get; set; }

        public ExtendedHeader(Header h)
        {
            VersionMajor = h.VersionMajor;
            VersionMinor = h.VersionMinor;
            DeviceUniqueAddress = h.DeviceUniqueAddress;
            SensorUniqueAddress = h.SensorUniqueAddress;
            SensorUniqueAddress = h.SensorUniqueAddress;
            Region = h.Region;
            SourceAdrress = h.SourceAdrress;
            Traversed = h.Traversed;
            ServiceCode = h.ServiceCode;
            PacketCode = h.PacketCode;
            SentAt = h.SentAt;
            SequenceNumber = h.SequenceNumber;
            HMacPayload = h.HMacPayload;
            HMacPacket = h.HMacPacket;

            SCode = (ServiceCodes)h.ServiceCode;
            PCode = (PacketCodes)h.PacketCode;
        }
    }
}