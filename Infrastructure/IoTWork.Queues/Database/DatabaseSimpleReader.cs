using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoTWork.Queues.Database
{
    public class DatabaseSimpleReader : IMessageQueueReader
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

        public void SetReadTimeout(int Milleseconds)
        {
        }

        public void Initialize()
        {
            lastReceivedMessage = DateTime.Now;
        }

        public byte[] ReadMessage()
        {
            switch (_provider)
            {
                case "SqlServer":
                    return ReadMessageSqlServer();
                default:
                    return null;
            }
        }

        private byte[] ReadMessageSqlServer()
        {
            byte[] data = null;

            try
            {
                //declare @Label nvarchar(128) = null

                //declare @id bigint = null
                //declare @message nvarchar(max) = null

                //set @id = (
                //    select top 1 ID from dbo.Queue with(nolock)
                //    where

                //        Status = 1

                //        AND Name = '.\Private$\TestQueue'

                //        AND(@Label IS NULL OR Label = @Label)

                //    order by ID asc
                // )

                //IF NOT @id IS NULL
                //BEGIN

                //    update dbo.Queue with(rowlock)
                //    set

                //        Status = 2

                //    where ID = @id


                //    set @message = (select Message from dbo.Queue with(nolock) where Id = @Id)
                //END

                //select @message AS Message

                using (var cn = new SqlConnection(_connection))
                {
                    cn.Open();

                    string query = "";
                    query = @"
                        declare @id	bigint = null
                        declare @message nvarchar(max) = null

                        set @id = (
	                        select top 1 ID from dbo.Queue with(nolock)
	                        where 
		                        Status = 1 
		                        AND Name = @name
	                        order by ID asc
	                        )

                        IF NOT @id IS NULL
                        BEGIN
	                        update dbo.Queue with(rowlock)
	                        set
		                        Status = 2
	                        where ID = @id

	                        set @message = (select Message from dbo.Queue with(nolock) where Id = @Id)
                        END

                        select @message AS Message
                    ";

                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@name", _queueName);

                    var result = command.ExecuteScalar();

                    data = System.Text.Encoding.UTF8.GetBytes((string)result);

                }
            }
            catch (Exception ex)
            {
                data = null;
            }

            return data;
        }
    }
}
