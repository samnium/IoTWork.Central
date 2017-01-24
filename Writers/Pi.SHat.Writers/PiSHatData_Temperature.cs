using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.SHat.Writers
{
    [Table("PiSHatData_Temperature", Schema = "public")]
    public class PiSHatData_Temperature
    {
        public Int64 Id { get; set; }

        public DateTime SentOn { get; set; }
        public DateTime ReceivedOn { get; set; }

        public Int64 SequenceNumber { get; set; }

        public Int32 OrdinalForSensor { get; set; }
        public Int32 OrdinalForChain { get; set; }
        public Int32 OrdinalForDevice { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public Double Value { get; set; }
    }
}
