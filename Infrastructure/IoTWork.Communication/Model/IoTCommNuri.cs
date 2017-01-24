using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Communication.Model
{
    [DataContract(Namespace = "http://iotwork.communication/data")]
    [Serializable]
    public enum IoTCommNuriLayer
    {
        [EnumMember]
        Network,
        [EnumMember]
        Region,
        [EnumMember]
        Ring,
        [EnumMember]
        Device,
        [EnumMember]
        DeviceEntity
    }


    [DataContract(Namespace = "http://iotwork.communication/data")]
    [Serializable]
    public class IoTCommNuri
    {
        #region public
        [DataMember]
        public IoTCommNuriLayer Layer { get; set; }

        [DataMember]
        public Int32 NetworkOrdinal { get; set; }

        [DataMember]
        public Int32 RegionOrdinal { get; set; }

        [DataMember]
        public Int32? RingOrdinal { get; set; }

        [DataMember]
        public Int32? DeviceOrdinal { get; set; }

        [DataMember]
        public String DeviceEntityOrdinal { get; set; }
        #endregion

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(NetworkOrdinal.ToString());
            sb.Append('.');
            sb.Append(RegionOrdinal.ToString());
            if (RingOrdinal.HasValue)
            {
                sb.Append('.');
                sb.Append(RingOrdinal.ToString());
            }
            if (DeviceOrdinal.HasValue)
            {
                sb.Append('.');
                sb.Append(DeviceOrdinal.ToString());
            }
            if (!String.IsNullOrEmpty(DeviceEntityOrdinal))
            {
                sb.Append('.');
                sb.Append(DeviceEntityOrdinal.ToString());
            }

            return sb.ToString();
        }
    }
}
