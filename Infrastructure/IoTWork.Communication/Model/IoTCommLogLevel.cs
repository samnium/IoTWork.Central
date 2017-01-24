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
    public enum IoTCommLogLevel : int
    {
        [EnumMember]
        Critical = 0,

        [EnumMember]
        Error = 1,

        [EnumMember]
        Warning = 2,

        [EnumMember]
        Info = 3,

        [EnumMember]
        Verbose = 4,

        [EnumMember]
        Debug = 5
    };
}
