using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTWork.Queues.MSMQ
{
    public class MsmqSimpleReader : IMessageQueueReader
    {
        private QueueMessageConfiguration _conf;
        private string _queueName;
        private DateTime lastReceivedMessage = DateTime.Now;

        private int _readtimeout;

        public MsmqSimpleReader()
        {
            _readtimeout = 1000;
        }

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

        public void SetReadTimeout(int Milleseconds)
        {
            _readtimeout = Milleseconds;
        }

        public void Initialize()
        {
            lastReceivedMessage = DateTime.Now;
        }

        public byte[] ReadMessage()
        {
            try
            {
                using (var helloQueue = new MessageQueue(_queueName))
                {
                    Message message = helloQueue.Receive(TimeSpan.FromMilliseconds(_readtimeout));
                    
                    if (message != null)
                    {
                        byte[] data = ReadFully(message.BodyStream);
                        return data;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
}
