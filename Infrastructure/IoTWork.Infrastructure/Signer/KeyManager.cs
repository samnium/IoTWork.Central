using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Signer
{
    public class KeyManager
    {
        public String DeviceKey { get; set; }

        public String HeaderKey { get; set; }

        public String PayloadKey { get; set; }

        public KeyManager()
        {
            if (File.Exists(@"C:\iotreader\keys\device.key"))
            {
                DeviceKey = File.ReadAllText(@"C:\iotreader\keys\device.key");
            }

            if (File.Exists(@"C:\iotreader\keys\header.key"))
            {
                HeaderKey = File.ReadAllText(@"C:\iotreader\keys\header.key");
            }

            if (File.Exists(@"C:\iotreader\keys\payload.key"))
            {
                PayloadKey = File.ReadAllText(@"C:\iotreader\keys\payload.key");
            }
        }
    }
}
