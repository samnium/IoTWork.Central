using IoTWork.Communication.Model;
using IoTWork.Infrastructure.Helpers;
using IoTWork.Queues;
using IoTWork.Queues.Database;
using IoTWork.Queues.MSMQ;
using IoTWork.Queues.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Communication
{
    public class Publisher<T>
    {
        IMessageQueuePublisher _publisher;
        QueueMessageConfiguration _configuration;

        public Publisher()
        {
        }

        private static readonly Lazy<Publisher<T>> instance = new Lazy<Publisher<T>>();

        public static Publisher<T> Instance
        {
            get { return instance.Value; }
        }

        public void SetPublisher(string name)
        {
            switch (name)
            {
                case "rabbitmq":
                    {
                        _publisher = new RabbitMQSimplePublisher();
                    }
                    break;
                case "msmq":
                    {
                        _publisher = new MsmqSimplePublisher();
                    }
                    break;
                case "database":
                    {
                        _publisher = new DatabaseSimplePublisher();
                    }
                    break;
            }
        }

        public void Configure(QueueMessageConfiguration Configuration)
        {
            _configuration = Configuration;
            _publisher.SetQueueMessageConfiguration(_configuration);
            _publisher.SetConnection(_configuration.Provider, _configuration.Connection);
            _publisher.SetQueue(_configuration.QueueName);
            _publisher.SetRoutingKey(_configuration.RouteKey);
        }

        public void Publish(T message)
        {
            var str = SerializerHelper.ToXml(message);
            var data = Encoding.UTF8.GetBytes(str);
            _publisher.Publish(data);
        }

        private void Publish(string message)
        {
            var data = Encoding.UTF8.GetBytes(message);
            _publisher.Publish(data);
        }
    }
}
