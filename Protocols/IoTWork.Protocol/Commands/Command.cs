﻿using IoTWork.Contracts;
using IoTWork.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Protocol.Datas
{
    [DataContract(Namespace = "http://iotwork.protocol/data")]
    [Serializable]
    public  abstract class Command : Payload
    {
        [DataMember(Name = "GUID", Order = 0)]
        public String GUID { get; set; }
    }
}
