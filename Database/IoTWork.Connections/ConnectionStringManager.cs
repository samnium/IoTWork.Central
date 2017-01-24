using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IoTWork.Connections
{
    public class DbConnectionManager
    {
        private Dictionary<string, string> _conf;

        public DbConnectionManager()
        {
            XElement root = XElement.Load(@"C:\iotserver\conf\connections.conf");
            var configuration = root.Elements("param").ToList();
            _conf = new Dictionary<string, string>();
            foreach (var param in configuration)
            {
                var key = param.Attribute("key").Value;
                var value = param.Attribute("value").Value;

                _conf.Add(key, value);
            }
        }

        private static readonly Lazy<DbConnectionManager> instance = new Lazy<DbConnectionManager>();

        public static DbConnectionManager Instance
        {
            get { return instance.Value; }
        }

        public DbConnection IoTWorkDataConnection(bool openit)
        {
            DbConnection dbconn = null;

            string providername = _conf["DB.IoTWork.Data.ProviderName"];
            string connectionstring = _conf["DB.IoTWork.Data.ConnectionString"];

            if (providername == "npgsql")
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionstring);

                if (openit)
                    conn.Open();

                dbconn = conn;
            }

            return dbconn;
        }

        public DbConnection IoTWorkNetworkConnection(bool openit)
        {
            DbConnection dbconn = null;

            string providername = _conf["DB.IoTWork.Network.ProviderName"];
            string connectionstring = _conf["DB.IoTWork.Network.ConnectionString"];

            if (providername == "npgsql")
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionstring);

                if (openit)
                    conn.Open();

                dbconn = conn;
            }

            return dbconn;
        }
    }
}
