using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTWork.Queues.RabbitMQ
{
    public class RabbitMQSimpleReader: IMessageQueueReader
    {
        private QueueMessageConfiguration _conf;
        private string _queueName;
        private DateTime lastReceivedMessage = DateTime.Now;
        private double _queueReconnectionDelayInMinutes;

        private IConnection _queueConnection;
        private IModel _queueChannel;
        private QueueingBasicConsumer _queueConsumer;
        private String _queueConsumerTag;

        private int _readtimeout;

        #region Constructor
        public RabbitMQSimpleReader()
        {
            _readtimeout = 1000;
            _queueReconnectionDelayInMinutes = 10.0;
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

        public void SetReadTimeout(int Milleseconds)
        {
            _readtimeout = Milleseconds;
        }


        public void Initialize()
        {
            _CreateQueue();
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
                            byte[] message = eventArgs.Body;

                            if (message != null)
                            {
                                _queueChannel.BasicAck(eventArgs.DeliveryTag, false);
                                lastReceivedMessage = DateTime.Now;
                                return message;

                            }
                            else
                            {
                                _queueChannel.BasicAck(eventArgs.DeliveryTag, false);
                                Exception ex = new Exception("Deserialized Message is null for the queue: " + _queueName);
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

        #region Queue Management
        private void _CreateQueue()
        {
            //
            // Istantiate QUEUE
            //

            ConnectionFactory factory = new ConnectionFactory();

            factory.Uri = _conf.Uri;

            _queueConnection = factory.CreateConnection();
            _queueChannel = _queueConnection.CreateModel();
            _queueChannel.BasicQos(0, 1, false);
            QueueDeclareOk ok = _queueChannel.QueueDeclare(_queueName, true, false, false, null);

            if (ok == null)
            {
                throw new InvalidOperationException("Couldn't connect to message queue.");
            }

            _queueConsumer = new QueueingBasicConsumer(_queueChannel);
            _queueConsumerTag = _queueChannel.BasicConsume(_queueName, false, _queueConsumer);
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
                    _CreateQueue();
                    recreated = true;
                    lastReceivedMessage = DateTime.Now;
                }
                catch (Exception ex)
                {
                    //LogManager.LogError(LogManager.AppName, "Dispatcher Type: " + dispatcherType.ToString() + "\r\n" + ex.Message, ex);
                    Thread.Sleep(5000);
                    continue;
                }
            }
        }

        #endregion
    }
}
