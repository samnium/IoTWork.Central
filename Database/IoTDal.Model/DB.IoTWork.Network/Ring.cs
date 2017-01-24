using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum RingStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2,
    }

    [Table("Ring", Schema = "public")]
    public class Ring
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(128)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }
        public Int32 OrdinalForRegion { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public DateTime SetUpDate { get; set; }

        public RingStatus Status { get; set; }

        // Foreign Keys
        public Int32 NetworkId { get; set; }
        public Int32 RegionId { get; set; }

        public ICollection<Device> Devices { get; set; }
    } 
}
