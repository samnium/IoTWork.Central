using IoTDal.Model.DB.IoTWork.Data;
using IoTDal.Model.DB.IoTWork.Network;
using IoTDal.Provider.Npgsql;
using IoTWork.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Access
{
    public class IoTWorkDataDalManager : IDisposable
    {
        private DataContext db = null;

        public IoTWorkDataDalManager()
        {
            DbConnectionManager connmanager = DbConnectionManager.Instance;
            var dbconn = connmanager.IoTWorkDataConnection(false);
            db = new DataContext(dbconn);
        }

        public void Dispose()
        {
            db = null;
        }


        public void SaveSample(Sample Sample)
        {
            switch (Sample.OrdinalForRegion)
            {
                case 1:
                    {
                        SaveSampleR1(Sample);
                    }
                    break;
                case 2:
                    {
                        SaveSampleR2(Sample);
                    }
                    break;
                case 3:
                    {
                        SaveSampleR3(Sample);
                    }
                    break;
                case 4:
                    {
                        SaveSampleR4(Sample);
                    }
                    break;
                case 5:
                    {
                        SaveSampleR5(Sample);
                    }
                    break;
            }
        }

        private void SaveSampleR5(Sample Sample)
        {
            Sample_Archive sA = new Sample_Archive();
            sA.TriggeredOn = Sample.TriggeredOn;
            sA.SentOn = Sample.SentOn;
            sA.ReceivedOn = Sample.ReceivedOn;
            sA.CreatedOn = Sample.CreatedOn;
            sA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            sA.OrdinalForRegion = Sample.OrdinalForRegion;
            sA.OrdinalForRing = Sample.OrdinalForRing;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.OrdinalForChain = Sample.OrdinalForChain;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.BinValue = Sample.BinValue;
            sA.XmlValue = Sample.XmlValue;
            sA.CsvValue = Sample.CsvValue;

            SampleR5 rs = new SampleR5();
            rs.TriggeredOn = Sample.TriggeredOn;
            rs.SentOn = Sample.SentOn;
            rs.ReceivedOn = Sample.ReceivedOn;
            rs.CreatedOn = Sample.CreatedOn;
            rs.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rs.OrdinalForRegion = Sample.OrdinalForRegion;
            rs.OrdinalForRing = Sample.OrdinalForRing;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.OrdinalForChain = Sample.OrdinalForChain;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.BinValue = Sample.BinValue;
            rs.XmlValue = Sample.XmlValue;
            rs.CsvValue = Sample.CsvValue;

            SampleR5_Archive rsA = new SampleR5_Archive();
            rsA.TriggeredOn = Sample.TriggeredOn;
            rsA.SentOn = Sample.SentOn;
            rsA.ReceivedOn = Sample.ReceivedOn;
            rsA.CreatedOn = Sample.CreatedOn;
            rsA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rsA.OrdinalForRegion = Sample.OrdinalForRegion;
            rsA.OrdinalForRing = Sample.OrdinalForRing;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.OrdinalForChain = Sample.OrdinalForChain;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.BinValue = Sample.BinValue;
            rsA.XmlValue = Sample.XmlValue;
            rsA.CsvValue = Sample.CsvValue;

            db.Samples.Add(Sample);
            db.Samples_Archive.Add(sA);
            db.SamplesR5.Add(rs);
            db.SamplesR5_Archive.Add(rsA);
            db.SaveChanges();
        }

        private void SaveSampleR4(Sample Sample)
        {
            Sample_Archive sA = new Sample_Archive();
            sA.TriggeredOn = Sample.TriggeredOn;
            sA.SentOn = Sample.SentOn;
            sA.ReceivedOn = Sample.ReceivedOn;
            sA.CreatedOn = Sample.CreatedOn;
            sA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            sA.OrdinalForRegion = Sample.OrdinalForRegion;
            sA.OrdinalForRing = Sample.OrdinalForRing;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.OrdinalForChain = Sample.OrdinalForChain;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.BinValue = Sample.BinValue;
            sA.XmlValue = Sample.XmlValue;
            sA.CsvValue = Sample.CsvValue;

            SampleR4 rs = new SampleR4();
            rs.TriggeredOn = Sample.TriggeredOn;
            rs.SentOn = Sample.SentOn;
            rs.ReceivedOn = Sample.ReceivedOn;
            rs.CreatedOn = Sample.CreatedOn;
            rs.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rs.OrdinalForRegion = Sample.OrdinalForRegion;
            rs.OrdinalForRing = Sample.OrdinalForRing;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.OrdinalForChain = Sample.OrdinalForChain;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.BinValue = Sample.BinValue;
            rs.XmlValue = Sample.XmlValue;
            rs.CsvValue = Sample.CsvValue;

            SampleR4_Archive rsA = new SampleR4_Archive();
            rsA.TriggeredOn = Sample.TriggeredOn;
            rsA.SentOn = Sample.SentOn;
            rsA.ReceivedOn = Sample.ReceivedOn;
            rsA.CreatedOn = Sample.CreatedOn;
            rsA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rsA.OrdinalForRegion = Sample.OrdinalForRegion;
            rsA.OrdinalForRing = Sample.OrdinalForRing;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.OrdinalForChain = Sample.OrdinalForChain;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.BinValue = Sample.BinValue;
            rsA.XmlValue = Sample.XmlValue;
            rsA.CsvValue = Sample.CsvValue;

            db.Samples.Add(Sample);
            db.Samples_Archive.Add(sA);
            db.SamplesR4.Add(rs);
            db.SamplesR4_Archive.Add(rsA);
            db.SaveChanges();
        }

        private void SaveSampleR3(Sample Sample)
        {
            Sample_Archive sA = new Sample_Archive();
            sA.TriggeredOn = Sample.TriggeredOn;
            sA.SentOn = Sample.SentOn;
            sA.ReceivedOn = Sample.ReceivedOn;
            sA.CreatedOn = Sample.CreatedOn;
            sA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            sA.OrdinalForRegion = Sample.OrdinalForRegion;
            sA.OrdinalForRing = Sample.OrdinalForRing;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.OrdinalForChain = Sample.OrdinalForChain;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.BinValue = Sample.BinValue;
            sA.XmlValue = Sample.XmlValue;
            sA.CsvValue = Sample.CsvValue;

            SampleR3 rs = new SampleR3();
            rs.TriggeredOn = Sample.TriggeredOn;
            rs.SentOn = Sample.SentOn;
            rs.ReceivedOn = Sample.ReceivedOn;
            rs.CreatedOn = Sample.CreatedOn;
            rs.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rs.OrdinalForRegion = Sample.OrdinalForRegion;
            rs.OrdinalForRing = Sample.OrdinalForRing;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.OrdinalForChain = Sample.OrdinalForChain;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.BinValue = Sample.BinValue;
            rs.XmlValue = Sample.XmlValue;
            rs.CsvValue = Sample.CsvValue;

            SampleR3_Archive rsA = new SampleR3_Archive();
            rsA.TriggeredOn = Sample.TriggeredOn;
            rsA.SentOn = Sample.SentOn;
            rsA.ReceivedOn = Sample.ReceivedOn;
            rsA.CreatedOn = Sample.CreatedOn;
            rsA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rsA.OrdinalForRegion = Sample.OrdinalForRegion;
            rsA.OrdinalForRing = Sample.OrdinalForRing;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.OrdinalForChain = Sample.OrdinalForChain;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.BinValue = Sample.BinValue;
            rsA.XmlValue = Sample.XmlValue;
            rsA.CsvValue = Sample.CsvValue;

            db.Samples.Add(Sample);
            db.Samples_Archive.Add(sA);
            db.SamplesR3.Add(rs);
            db.SamplesR3_Archive.Add(rsA);
            db.SaveChanges();
        }

        private void SaveSampleR2(Sample Sample)
        {
            Sample_Archive sA = new Sample_Archive();
            sA.TriggeredOn = Sample.TriggeredOn;
            sA.SentOn = Sample.SentOn;
            sA.ReceivedOn = Sample.ReceivedOn;
            sA.CreatedOn = Sample.CreatedOn;
            sA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            sA.OrdinalForRegion = Sample.OrdinalForRegion;
            sA.OrdinalForRing = Sample.OrdinalForRing;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.OrdinalForChain = Sample.OrdinalForChain;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.BinValue = Sample.BinValue;
            sA.XmlValue = Sample.XmlValue;
            sA.CsvValue = Sample.CsvValue;

            SampleR2 rs = new SampleR2();
            rs.TriggeredOn = Sample.TriggeredOn;
            rs.SentOn = Sample.SentOn;
            rs.ReceivedOn = Sample.ReceivedOn;
            rs.CreatedOn = Sample.CreatedOn;
            rs.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rs.OrdinalForRegion = Sample.OrdinalForRegion;
            rs.OrdinalForRing = Sample.OrdinalForRing;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.OrdinalForChain = Sample.OrdinalForChain;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.BinValue = Sample.BinValue;
            rs.XmlValue = Sample.XmlValue;
            rs.CsvValue = Sample.CsvValue;

            SampleR2_Archive rsA = new SampleR2_Archive();
            rsA.TriggeredOn = Sample.TriggeredOn;
            rsA.SentOn = Sample.SentOn;
            rsA.ReceivedOn = Sample.ReceivedOn;
            rsA.CreatedOn = Sample.CreatedOn;
            rsA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rsA.OrdinalForRegion = Sample.OrdinalForRegion;
            rsA.OrdinalForRing = Sample.OrdinalForRing;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.OrdinalForChain = Sample.OrdinalForChain;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.BinValue = Sample.BinValue;
            rsA.XmlValue = Sample.XmlValue;
            rsA.CsvValue = Sample.CsvValue;

            db.Samples.Add(Sample);
            db.Samples_Archive.Add(sA);
            db.SamplesR2.Add(rs);
            db.SamplesR2_Archive.Add(rsA);
            db.SaveChanges();
        }

        private void SaveSampleR1(Sample Sample)
        {
            Sample_Archive sA = new Sample_Archive();
            sA.TriggeredOn = Sample.TriggeredOn;
            sA.SentOn = Sample.SentOn;
            sA.ReceivedOn = Sample.ReceivedOn;
            sA.CreatedOn = Sample.CreatedOn;
            sA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            sA.OrdinalForRegion = Sample.OrdinalForRegion;
            sA.OrdinalForRing = Sample.OrdinalForRing;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.OrdinalForChain = Sample.OrdinalForChain;
            sA.OrdinalForDevice = Sample.OrdinalForDevice;
            sA.BinValue = Sample.BinValue;
            sA.XmlValue = Sample.XmlValue;
            sA.CsvValue = Sample.CsvValue;

            SampleR1 rs = new SampleR1();
            rs.TriggeredOn = Sample.TriggeredOn;
            rs.SentOn = Sample.SentOn;
            rs.ReceivedOn = Sample.ReceivedOn;
            rs.CreatedOn = Sample.CreatedOn;
            rs.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rs.OrdinalForRegion = Sample.OrdinalForRegion;
            rs.OrdinalForRing = Sample.OrdinalForRing;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.OrdinalForChain = Sample.OrdinalForChain;
            rs.OrdinalForDevice = Sample.OrdinalForDevice;
            rs.BinValue = Sample.BinValue;
            rs.XmlValue = Sample.XmlValue;
            rs.CsvValue = Sample.CsvValue;

            SampleR1_Archive rsA = new SampleR1_Archive();
            rsA.TriggeredOn = Sample.TriggeredOn;
            rsA.SentOn = Sample.SentOn;
            rsA.ReceivedOn = Sample.ReceivedOn;
            rsA.CreatedOn = Sample.CreatedOn;
            rsA.OrdinalForNetwork = Sample.OrdinalForNetwork;
            rsA.OrdinalForRegion = Sample.OrdinalForRegion;
            rsA.OrdinalForRing = Sample.OrdinalForRing;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.OrdinalForChain = Sample.OrdinalForChain;
            rsA.OrdinalForDevice = Sample.OrdinalForDevice;
            rsA.BinValue = Sample.BinValue;
            rsA.XmlValue = Sample.XmlValue;
            rsA.CsvValue = Sample.CsvValue;

            db.Samples.Add(Sample);
            db.Samples_Archive.Add(sA);
            db.SamplesR1.Add(rs);
            db.SamplesR1_Archive.Add(rsA);
            db.SaveChanges();
        }
    }
}
