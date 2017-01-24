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
    [KnownType(typeof(IoTCommExcpectionMessage))]
    public class IoTCommMessage
    {
        [DataMember]
        public DateTime Timestamp { get; set; }

        [DataMember]
        public UInt16 Age { get; set; }

        [DataMember]
        public String Source { get; set; }

        [DataMember]
        public IoTCommLogLevel Level { get; set; }

        [DataMember]
        public IoTCommNuri Nuri { get; set; }

        [DataMember]
        public String Title { get; set; }

        [DataMember]
        public String Message { get; set; }

        [DataMember]
        public IoTCommStatus Status { get; set; }
    }

    [DataContract(Namespace = "http://iotwork.communication/data")]
    [Serializable]
    public class IoTCommExcpectionMessage: IoTCommMessage
    {
        [DataMember]
        public String Exception { get; set; }
    }

}