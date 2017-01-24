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
    public enum IoTPickerMessageName
    {
        #region public
        [EnumMember]
        Stop,

        [EnumMember]
        Statistics,
        [EnumMember]
        Errors,
        [EnumMember]
        Alive,
        [EnumMember]
        UpTime
        #endregion
    }
}
