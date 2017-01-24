using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IoTWork.Communication.Model;
using IoTWork.XML;

namespace IoTWork.IoTPicker.Management
{
    internal class Context
    {
        internal ushort PickerId;
        internal string PickerName;
        internal IoTCommNuri PickerNuri { get; set; }

        internal iotpicker configuration { get; set; }

        internal List<PickerManager> PickerManagers { get; set; }
    }
}
