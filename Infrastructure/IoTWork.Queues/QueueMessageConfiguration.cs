using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Queues
{
    public class QueueMessageConfiguration
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public string Uri { get; set; }
        public string Topic { get; set; }
        public string Connection { get; set; }
        public string Provider { get; set; }
        public string QueueName { get; set; }
        public string RouteKey { get; set; }
    }
}
