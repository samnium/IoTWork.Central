using IoTWork.Queues;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace IoTWork.Queues.RabbitMQ
{
    public class RabbitMQTopicReader : IDisposable, IMessageQueueReader
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

        private int _readtimeout;

        public RabbitMQTopicReader()
        {
            _readtimeout = 1000;
            _queueReconnectionDelayInMinutes = 10.0;
        }

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

        public void SetReadTimeout(int Milleseconds)
        {
            _readtimeout = Milleseconds;
        }

        public void Dispose()
        {
            _DisposeQueue();
        }

        #region Management
        public void Initialize()
        {
            _CreateQueue(_routingKey);
            lastReceivedMessage = DateTime.Now;
        }

        public void Update()
        {
            if (lastReceivedMessage.AddMinutes(_queueReconnectionDelayInMinutes) < DateTime.Now)
            {
                _RecreateQueue();
            }
        }

        public byte[] ReadMessage()
        {
            try
            {
                BasicDeliverEventArgs eventArgs;

                if (_queueConsumer.Queue.Dequeue(_readtimeout, out eventArgs))
                {
                    if (eventArgs != null)
                    {
                        try
                        {
                            var message = eventArgs.Body;

                            if (message != null)
                            {
                                _queueChannel.BasicAck(eventArgs.DeliveryTag, false);
                                lastReceivedMessage = DateTime.Now;
                                return message;
                            }
                            else
                            {
                                _queueChannel.BasicAck(eventArgs.DeliveryTag, false);
                                lastReceivedMessage = DateTime.Now;
                                return null;
                            }
                        }
                        catch (TimeoutException ex)
                        {
                            _queueChannel.BasicAck(eventArgs.DeliveryTag, false);
                            return null;
                        }
                        catch (IOException ex)
                        {
                            _queueChannel.BasicAck(eventArgs.DeliveryTag, false);
                            return null;
                        }
                        catch (Exception ex)
                        {
                            _queueChannel.BasicAck(eventArgs.DeliveryTag, false);
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    // timeout
                    return null;
                }
            }
            catch (Exception e)
            {
                _RecreateQueue();
                return null;
            }
        }

        #endregion

        #region Queue Management
        private void _CreateQueue(String RoutingKey)
        {
            //
            // Istantiate QUEUE
            //

            ConnectionFactory factory = new ConnectionFactory();

            //factory.HostName = _queueHostName;
            //factory.Port = _queuePort;
            //factory.UserName = _queueUserName;
            //factory.Password = _queuePassword;
            //factory.VirtualHost = _queueVirtualHost;
            factory.Uri = _conf.Uri;

            _queueConnection = factory.CreateConnection();
            _queueChannel = _queueConnection.CreateModel();
            _queueChannel.ExchangeDeclare(exchange: _conf.Topic, type: "topic");
            _queueChannel.BasicQos(0, 1, false);

            _personalQueueName = _queueChannel.QueueDeclare().QueueName;

            _queueChannel.QueueBind(queue: _personalQueueName, exchange: _conf.Topic, routingKey: RoutingKey);

            _queueConsumer = new QueueingBasicConsumer(_queueChannel);
            _queueConsumerTag = _queueChannel.BasicConsume(_personalQueueName, false, _queueConsumer);
        }

        private void _DisposeQueue()
        {
            if (_queueChannel != null && !string.IsNullOrEmpty(_queueConsumerTag) && !string.IsNullOrWhiteSpace(_queueConsumerTag))
            {
                try
                {
                    try
                    {
                        _queueChannel.BasicCancel(_queueConsumerTag);
                    }
                    catch (Exception e)
                    {
                    }
                    try
                    {
                        _queueChannel.Dispose();
                    }
                    catch (Exception e)
                    {
                        _queueChannel.Abort();
                    }
                    try
                    {
                        _queueChannel.Dispose();
                    }
                    catch (Exception e)
                    {
                        _queueChannel.Abort();
                    }
                }
                catch (Exception e)
                {
                }
            }
        }

        private void _RecreateQueue()
        {
            bool recreated = false;
            while (!recreated)
            {
                try
                {
                    _DisposeQueue();
                    _CreateQueue(_routingKey);
                    recreated = true;
                    lastReceivedMessage = DateTime.Now;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(5000);
                    continue;
                }
            }
        }

        #endregion

    }

}
