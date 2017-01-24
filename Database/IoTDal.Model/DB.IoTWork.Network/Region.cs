using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum RegionStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2,
    }

    [Table("Region", Schema = "public")]
    public class Region
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(64)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }
        public Int32 OrdinalForNetwork { get; set; }

        public DateTime SetUpDate { get; set; }

        public RegionStatus Status { get; set; }

        public Server DataServer { get; set; }

        public Server ManagementServer { get; set; }

        public Writer Writer { get; set; }

        // Foreign Keys
        public Int32 DataServerId { get; set; }
        public Int32 ManagementServerId { get; set; }
        public Int32 NetworkId { get; set; }

        public ICollection<Ring> Devices { get; set; }
    } 
}
