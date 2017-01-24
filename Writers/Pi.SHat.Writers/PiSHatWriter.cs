using IoTWork.Contracts;
using Npgsql;
using Pi.SHat.Sensor.Humidity;
using Pi.SHat.Sensor.Pressure;
using Pi.SHat.Sensor.Temperature;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.SHat.Writers
{
    class PiSHatWriter: IIoTWriter
    {
        PiSHatContext db;

        public PiSHatWriter()
        {
            db = null;
        }

        public void Init(string Parameter)
        {
            DbConnection dbconn = null;

            var splitted = Parameter.Split(new string[] { "::::" }, StringSplitOptions.None);

            string providername = splitted[0];
            string connectionstring = splitted[1];
            bool openit = Boolean.Parse(splitted[2]);

            if (providername == "npgsql")
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionstring);

                if (openit)
                    conn.Open();

                dbconn = conn;
            }

            if (dbconn != null)
            {
                db = new PiSHatContext(dbconn);
            }
        }

        public void Write(DateTime SentAt, DateTime ReadAt, long SequenceNumber, string SourceAdrress, int Network, int Region, int Ring, int Device, int Sensor, string DeviceUniqueAddress, IIoTSample Sample)
        {
            if (db != null)
            {
                Console.WriteLine("....check for type {0}", Sample.GetType());
                Console.WriteLine("....type 1 {0}", Sample.GetType().ToString() == typeof(PiSHatSample_Temperature).ToString());
                Console.WriteLine("....type 2 {0}", Sample.GetType().ToString() == typeof(PiSHatSample_Pressure).ToString());
                Console.WriteLine("....type 3 {0}", Sample.GetType().ToString() == typeof(PiSHatSample_Humidity).ToString());

                if (Sample.GetType().ToString() == typeof(PiSHatSample_Temperature).ToString())
                {
                    Console.Write("....>>>>>> Writing PiSHatSample_Temperature");

                    PiSHatData_Temperature data = new PiSHatData_Temperature();
                    data.SentOn = SentAt;
                    data.ReceivedOn = ReadAt;
                    data.SequenceNumber = SequenceNumber;
                    data.OrdinalForNetwork = Network;
                    data.OrdinalForRegion = Region;
                    data.OrdinalForRing = Ring;
                    data.OrdinalForDevice = Device;
                    data.OrdinalForChain = Sensor;
                    data.OrdinalForSensor = Sensor;
                    data.Value = ((PiSHatSample_Temperature)Sample).Value;

                    db.Temperatures.Add(data);
                    db.SaveChanges();

                    Console.WriteLine("<<<<<<.... SAVED");
                }
                else if (Sample.GetType().ToString() == typeof(PiSHatSample_Pressure).ToString())
                {
                    Console.Write("....>>>>>> Writing PiSHatSample_Pressure");

                    PiSHatData_Pressure data = new PiSHatData_Pressure();
                    data.SentOn = SentAt;
                    data.ReceivedOn = ReadAt;
                    data.SequenceNumber = SequenceNumber;
                    data.OrdinalForNetwork = Network;
                    data.OrdinalForRegion = Region;
                    data.OrdinalForRing = Ring;
                    data.OrdinalForDevice = Device;
                    data.OrdinalForChain = Sensor;
                    data.OrdinalForSensor = Sensor;
                    data.Value = ((PiSHatSample_Pressure)Sample).Value;

                    db.Pressures.Add(data);
                    db.SaveChanges();

                    Console.WriteLine("<<<<<<.... SAVED");
                }
                else if (Sample.GetType().ToString() == typeof(PiSHatSample_Humidity).ToString())
                {
                    Console.Write("....>>>>>> Writing PiSHatSample_Humidity");

                    PiSHatData_Humidity data = new PiSHatData_Humidity();
                    data.SentOn = SentAt;
                    data.ReceivedOn = ReadAt;
                    data.SequenceNumber = SequenceNumber;
                    data.OrdinalForNetwork = Network;
                    data.OrdinalForRegion = Region;
                    data.OrdinalForRing = Ring;
                    data.OrdinalForDevice = Device;
                    data.OrdinalForChain = Sensor;
                    data.OrdinalForSensor = Sensor;
                    data.Value = ((PiSHatSample_Humidity)Sample).Value;

                    db.Humidities.Add(data);
                    db.SaveChanges();

                    Console.WriteLine("<<<<<<.... SAVED");
                }
            }
            else
            {
                Console.WriteLine("<<<<<<.... DB is NULL");
            }
        }
    }
}
