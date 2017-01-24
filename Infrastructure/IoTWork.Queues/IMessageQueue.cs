using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Queues
{
    public interface IMessageQueue
    {
        void SetQueueMessageConfiguration(QueueMessageConfiguration conf);

        void SetConnection(string provider, string connection);

        void SetQueue(string QueueName);

        void SetRoutingKey(string RoutingKey);
    }

    public interface IMessageQueueReader: IMessageQueue
    {
        void SetReadTimeout(int Milleseconds);

        void Initialize();

        byte[] ReadMessage();
    }

    public interface IMessageQueuePublisher: IMessageQueue
    {
        bool Publish(byte[] data);

    }
}
