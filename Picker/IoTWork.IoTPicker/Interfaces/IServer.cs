using IoTWork.IoTPicker.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.IoTPicker.Interfaces
{
    internal interface IServer
    {
        void SetUri(Uri uri);

        void Start();

        void Stop();

        bool TryGetMessage(out NetworkPacket packet);
    }
}
