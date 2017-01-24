using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace IoTWork.Infrastructure.IPC.WcfPipe
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class PipeService : IPipeService
    {
        public static string URI
            = "net.pipe://localhost/Pipe";

        #region IPipeService Members

        public void PipeIn(List<string> data)
        {
            if (DataReady != null)
                DataReady(data);
        }

        public delegate void DataIsReady(List<string> hotData);
        public DataIsReady DataReady = null;

        #endregion
    }
}
