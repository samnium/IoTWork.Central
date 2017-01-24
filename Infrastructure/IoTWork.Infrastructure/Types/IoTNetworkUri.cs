using IoTWork.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Types
{
    [DataContract]
    [Serializable]
    public class IoTNetworkUri
    {
        #region public
        [DataMember]
        public Int32 NetworkOrdinal { get; set; }

        [DataMember]
        public Int32 RegionOrdinal { get; set; }

        [DataMember]
        public Int32? RingOrdinal { get; set; }

        [DataMember]
        public Int32? DeviceOrdinal { get; set; }

        [DataMember]
        public String DeviceEntity { get; set; }
        #endregion
    }
}
