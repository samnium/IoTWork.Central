﻿using IoTWork.Contracts;
using IoTWork.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Protocol.Devices
{
    [DataContract(Namespace = "http://iotwork.protocol/device")]
    [Serializable]
    public class UpTime : Payload
    {
        [DataMember(Name = "VAL", Order = 0)]
        public TimeSpan Value { get; set; }
    }
}
