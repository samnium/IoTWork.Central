using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

// http://omegacoder.com/?p=101

namespace IoTWork.Infrastructure.IPC.WcfPipe
{
    [ServiceContract(Namespace = "http://IoTWork.Infrastructure.IPC.WcfPipe/NamedPipe1")]
    public interface IPipeService
    {
        [OperationContract]
        void PipeIn(List<string> data);
    }
}
