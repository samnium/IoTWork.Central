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
    public enum IoTPickerCommandName
    {
        #region public
        [EnumMember]
        Stop,

        [EnumMember]
        AskForStatistics,
        [EnumMember]
        AskForErrors,
        [EnumMember]
        AskForAlive,
        [EnumMember]
        AskForUpTime
        #endregion
    }
}

