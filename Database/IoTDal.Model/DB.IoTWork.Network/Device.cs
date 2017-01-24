using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum DeviceType
    {
        Undefined = 0,
        Custom = 1
    }

    public enum DeviceStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2
    }

    [Table("Device", Schema = "public")]
    public class Device
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(256)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }
        public Int32 OrdinalForRing { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public DeviceType Type { get; set; }

        public DeviceStatus Status { get; set; }

        public DateTime SetUpDate { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        // Foreign Keys
        public Int32 NetworkId { get; set; }
        public Int32 RegionId { get; set; }
        public Int32 RingId { get; set; }
        public Int32 BindingId { get; set; }

        public ICollection<Chain> Chains { get; set; }
        public Binding Binding { get; set; }
    }
}
