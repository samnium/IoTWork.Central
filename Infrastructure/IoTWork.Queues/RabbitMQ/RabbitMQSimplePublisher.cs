using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTWork.Queues.RabbitMQ
{
    public class RabbitMQSimplePublisher: IMessageQueuePublisher
    {
        QueueMessageConfiguration _conf;

        private string _queueName;

        #region Constructor
        public RabbitMQSimplePublisher()
        {
        }
        #endregion

        private String _provider;
        private String _connection;

        public void SetConnection(string provider, string connection)
        {
            _provider = provider;
            _connection = connection;
        }

        public void SetQueueMessageConfiguration(QueueMessageConfiguration conf)
        {
            _conf = conf;
        }

        public void SetQueue(string QueueName)
        {
            _queueName = QueueName;
        }

        public void SetRoutingKey(string RoutingKey)
        {
            
        }

        public bool Publish(byte[] data)
        {
            bool published = false;

            var factory = new ConnectionFactory();

            //factory.HostName = _rabbit_HostName;
            //factory.Port = _rabbit_Port;
            //factory.UserName = _rabbit_UserName;
            //factory.Password = _rabbit_Password;
            //factory.VirtualHost = _rabbit_VirtualHost;

            factory.Uri = _conf.Uri;

            Boolean done = false;
            int count = 1;
            int maxcount = 3;

            while (count < maxcount)
            {
                try
                {
                    using (var connection = factory.CreateConnection())
                    {
                        using (var channel = connection.CreateModel())
                        {
                            QueueDeclareOk ok = channel.QueueDeclare(_queueName, true, false, false, null);
                            channel.BasicPublish(String.Empty, _queueName, null, data);
                            published = true;
                        }
                    }
                    done = true;
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    count = done ? maxcount + 1 : count + 1;
                }
            }

            return published;
        }
    }
}
