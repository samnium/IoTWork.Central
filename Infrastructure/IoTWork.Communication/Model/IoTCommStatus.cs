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
    public enum IoTCommStatus
    {
        [EnumMember]
        None,
        [EnumMember]
        Error,
        [EnumMember]
        Success,
        [EnumMember]
        Warning
    }
}
