using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Queues.MSMQ
{
    // http://www.codeproject.com/Articles/871746/Implementing-pub-sub-using-MSMQ-in-minutes
    // https://technet.microsoft.com/en-us/library/bb684791.aspx
    // https://technet.microsoft.com/en-us/library/cc755270(v=ws.11).aspx

    public class MsmqSimplePublisher : IMessageQueuePublisher
    {
        private QueueMessageConfiguration _conf;
        private string _queueName;
        private DateTime lastReceivedMessage = DateTime.Now;

        private String _provider;
        private String _connection;

        public void SetConnection(string provider, string connection)
        {
            _provider = provider;
            _connection = connection;
        }

        public void SetQueue(string QueueName)
        {
            _queueName = QueueName;
        }

        public void SetQueueMessageConfiguration(QueueMessageConfiguration conf)
        {
            _conf = conf;
        }

        public void SetRoutingKey(string RoutingKey)
        {
        }

        public bool Publish(byte[] data)
        {
            bool queued = false;

            try
            {
                using (var queue = new MessageQueue(_queueName))
                {
                    Message msg = new Message();
                    msg.BodyStream = new MemoryStream(data);

                    queue.Send(msg);
                }
                queued = true;
            }
            catch(Exception ex)
            {
                queued = false;
            }

            return queued;
        }
    }
}
