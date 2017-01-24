using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Queues.Database
{
    // http://www.codeproject.com/Articles/871746/Implementing-pub-sub-using-MSMQ-in-minutes
    // https://technet.microsoft.com/en-us/library/bb684791.aspx
    // https://technet.microsoft.com/en-us/library/cc755270(v=ws.11).aspx

    public class DatabaseSimplePublisher : IMessageQueuePublisher
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
           switch (_provider)
            {
                case "SqlServer":
                    return PublishSqlServer(data);
                default:
                    return false;
            }
        }

        public bool PublishSqlServer(byte[] data)
        {
            bool queued = false;

            try
            {
                using (var cn = new SqlConnection(_connection))
                {
                    cn.Open();

                    var str = System.Text.Encoding.UTF8.GetString(data);

                    string query =
                        String.Format("INSERT INTO [dbo].[Queue]([Name],[Label],[Message],[CreatedOn],[Status]) VALUES (@name, NULL, @message, @datetime, 1)");

                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@name", _queueName);
                    command.Parameters.AddWithValue("@message", str);
                    command.Parameters.AddWithValue("@datetime", DateTime.Now);

                    command.ExecuteNonQuery();
                }
                queued = true;
            }
            catch (Exception ex)
            {
                queued = false;
            }

            return queued;
        }
    }
}
