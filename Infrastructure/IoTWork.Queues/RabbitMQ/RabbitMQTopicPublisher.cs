using IoTWork.Queues;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace IoTWork.Queues.RabbitMQ
{
    public class RabbitMQTopicPublisher: IMessageQueuePublisher
    {
        QueueMessageConfiguration _conf;

        private double _queueReconnectionDelayInMinutes;

        private string _personalQueueName;
        private string _routingKey;

        private IConnection _queueConnection;
        private IModel _queueChannel;
        private QueueingBasicConsumer _queueConsumer;
        private String _queueConsumerTag;

        private DateTime lastReceivedMessage = DateTime.Now;

        public void SetQueueMessageConfiguration(QueueMessageConfiguration conf)
        {
            _conf = conf;
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
        }

        public void SetRoutingKey(string RoutingKey)
        {
            _routingKey = RoutingKey;
        }

        public RabbitMQTopicPublisher()
        {
            _queueReconnectionDelayInMinutes = 10.0;
        }

        public bool Publish(byte[] message)
        {
            try
            {
                return PublishOnRabbit(message, _routingKey);
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        private bool PublishOnRabbit(byte[] data, String routingKey)
        {
            bool published = false;
            var factory = new ConnectionFactory();

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
                            channel.ExchangeDeclare(exchange: _conf.Topic, type: "topic");
                            channel.BasicPublish(exchange: _conf.Topic, routingKey: routingKey, basicProperties: null, body: data);
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
