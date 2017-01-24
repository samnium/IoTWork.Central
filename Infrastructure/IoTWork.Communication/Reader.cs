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
    public class Reader
    {
        IMessageQueueReader _reader;
        QueueMessageConfiguration _configuration;

        public Reader()
        {
        }

        private static readonly Lazy<Reader> instance = new Lazy<Reader>();

        public static Reader Instance
        {
            get { return instance.Value; }
        }

        public void SetReader(string name)
        {
            switch (name)
            {
                case "rabbitmq":
                    {
                        _reader = new RabbitMQSimpleReader();
                    }
                    break;
                case "msmq":
                    {
                        _reader = new MsmqSimpleReader();
                    }
                    break;
                case "database":
                    {
                        _reader = new DatabaseSimpleReader();
                    }
                    break;
            }
        }

        public void Configure(QueueMessageConfiguration Configuration)
        {
            _configuration = Configuration;
            _reader.SetQueueMessageConfiguration(_configuration);
            _reader.SetConnection(_configuration.Provider, _configuration.Connection);
            _reader.SetQueue(_configuration.QueueName);
            _reader.SetRoutingKey(_configuration.RouteKey);
            _reader.SetReadTimeout(500);
        }

        public IoTCommMessage Read()
        {
            var message = _reader.ReadMessage();
            if (message != null)
            {
                var str = Encoding.UTF8.GetString(message);
                var obj = (IoTCommMessage)SerializerHelper.FromXml(str, typeof(IoTCommMessage));
                return obj;
            }
            else
                return null;
        }
    }
}
