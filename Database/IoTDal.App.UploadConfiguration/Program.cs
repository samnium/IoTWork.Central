using IoTDal.Provider.Npgsql;
using IoTWork.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IoTDal.App.UploadConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            IoTWorkNetwork();
        }

        private static void IoTWorkNetwork()
        {
            try
            {
                XElement root = XElement.Load(@"..\..\..\IoTWork.Things\Setup\Setup.config");
                var configuration = root.Elements("param").ToList();

                DbConnectionManager connmanager = DbConnectionManager.Instance;
                var dbconn = connmanager.IoTWorkNetworkConnection(false);

                using (var db = new NetworkContext(dbconn))
                {
                    foreach (var param in configuration)
                    {
                        string operation = String.Empty;

                        var key = param.Attribute("key").Value;
                        var value = param.Attribute("value").Value;

                        var item = db.Parameters.Where(x => x.Key == key).FirstOrDefault();

                        if (item != null)
                        {
                            item.Value = value;
                            db.SaveChanges();

                            operation = "Updated";
                        }
                        else
                        {
                            db.Parameters.Add(new Model.DB.IoTWork.Network.Parameter() { Key = key, Value = value });
                            db.SaveChanges();

                            operation = "Added";
                        }

                        System.Console.WriteLine("{0} parameter {1}", operation, key);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                    System.Console.WriteLine(ex.InnerException.Message);
                System.Console.ReadLine();
                System.Console.ReadLine();
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Configuration done. Press <enter> twice!");
            System.Console.ReadLine();
            System.Console.ReadLine();
        }
    }
}
