using IoTWork.Infrastructure.Types;
using IoTWork.Protocol.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Protocol
{
    public class ExtendedPayload: Payload
    {
        Payload _p;

        public ExtendedPayload(Payload p)
        {
            _p = p;
        }

        public Payload Base()
        {
            return _p;
        }
    }

    public class ExtendedPickerPayload : ExtendedPayload
    {
        public IoTPickerMessageName MessageName { get; }

        public ExtendedPickerPayload(Payload p, IoTPickerMessageName MessageName) : base(p)
        {
            this.MessageName = MessageName;
        }
    }
}
