using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum ChainType
    {
        Undefined = 0,
        SensorListner = 1,
        Monitoring = 2
    }

    public enum ChainStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2
    }

    [Table("Chain", Schema = "public")]
    public class Chain
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(256)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }
        public Int32 OrdinalForNetwork { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForDevice { get; set; }

        public ChainType Type { get; set; }

        public ChainStatus Status { get; set; }

        public Trigger Trigger { get; set; }

        public Sensor Sensor { get; set; }

        // Foreign Keys
        public Int32 DeviceId { get; set; }

        public ICollection<Pipe> Pipes { get; set; }
    }
}
