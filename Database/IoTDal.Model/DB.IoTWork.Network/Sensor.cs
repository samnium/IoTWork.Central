using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum SensorType
    {
        Undefined = 0,
        Custom = 1,
        DateTime = 2,
        Random = 3,
        Void = 4
    }

    public enum SensorStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2
    }

    [Table("Sensor", Schema = "public")]
    public class Sensor
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(256)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }

        public SensorType Type { get; set; }

        public SensorStatus Status { get; set; }

        [MaxLength(1024)]
        public String Parameter { get; set; }

        [MaxLength(128)]
        public String ModuleName { get; set; }

        [MaxLength(1024)]
        public String ModulePath { get; set; }

        // Foreign Key
        public Int32 DeviceId { get; set; }
        public Int32 ChainId { get; set; }
    }
}
