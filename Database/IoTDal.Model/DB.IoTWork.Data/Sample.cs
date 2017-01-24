using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Data
{
    [Table("Sample", Schema = "public")]
    public class Sample
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }

    [Table("Sample_Archive", Schema = "public")]
    public class Sample_Archive
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }

    #region 1
    [Table("SampleR1")]
    public class SampleR1
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }

    [Table("SampleR1_Archive")]
    public class SampleR1_Archive
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }
    #endregion

    #region 2
    [Table("SampleR2")]
    public class SampleR2 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }

    [Table("SampleR2_Archive")]
    public class SampleR2_Archive 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }
    #endregion

    #region 3
    [Table("SampleR3")]
    public class SampleR3 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }

    [Table("SampleR3_Archive")]
    public class SampleR3_Archive 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }
    #endregion

    #region 4
    [Table("SampleR4")]
    public class SampleR4 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }

    [Table("SampleR4_Archive")]
    public class SampleR4_Archive 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }
    #endregion

    #region 5
    [Table("SampleR5")]
    public class SampleR5 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }

    [Table("SampleR5_Archive")]
    public class SampleR5_Archive 
    {
        public Int64 Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime TriggeredOn { get; set; }
        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public String BinValue { get; set; }
        public String XmlValue { get; set; }
        public String CsvValue { get; set; }
    }
    #endregion
}
