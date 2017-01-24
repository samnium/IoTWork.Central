using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;

namespace IoTWork.Infrastructure.IPC.WcfPipe
{
    public class Sender
    {
        public static void SendMessage(String message)
        {
            List<string> messages = new List<string>();
            messages.Add(message);
            SendMessage(messages, Receiver.DefaultPipeName);
        }

        public static void SendMessage(List<string> messages)
        {
            SendMessage(messages, Receiver.DefaultPipeName);
        }

        public static void SendMessage(List<string> messages, string PipeName)
        {
            try
            {
                EndpointAddress ep
                    = new EndpointAddress(
                        string.Format("{0}/{1}",
                           PipeService.URI,
                           PipeName));
                IPipeService proxy = ChannelFactory<IPipeService>.CreateChannel(new NetNamedPipeBinding(), ep);
                proxy.PipeIn(messages);
            }
            catch(Exception)
            {

            }
        }
    }
}
