using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum NetworkStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2,
        Dismissed = 3
    }

    [Table("Network", Schema = "public")]
    public class Network
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(32)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }

        public DateTime SetUpDate { get; set; }

        public NetworkStatus Status { get; set; }

        public ICollection<Region> Regions { get; set; }
    } 
}
