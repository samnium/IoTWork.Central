using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum TriggerType
    {
        Undefined = 0,
        Simple = 1,
        Void = 2
    }

    public enum TriggerStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2,
    }

    [Table("Trigger", Schema = "public")]
    public class Trigger
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(256)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }

        public TriggerType Type { get; set; }

        public TriggerStatus Status { get; set; }

        public Int32 TimeoutInMilliseconds { get; set; }

        public Boolean RunOnlyOnce { get; set; }

        // Foreign Keys
        public Int32 DeviceId { get; set; }
        public Int32 ChainId { get; set; }
    }
}
