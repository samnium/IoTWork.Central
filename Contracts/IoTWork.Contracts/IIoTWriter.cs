using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Contracts
{
    public interface IIoTWriter
    {
        void Init(String Parameter);

        void Write(DateTime SentAt, DateTime ReadAt, Int64 SequenceNumber, string SourceAdrress, Int32 Network, Int32 Region, Int32 Ring, Int32 Device, Int32 Sensor, string DeviceUniqueAddress, IIoTSample Sample);
    }
}
