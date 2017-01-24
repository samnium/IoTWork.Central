using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum PipeType
    {
        Undefined = 0,
        Simple = 1,
        Delay = 2,
        Alternate = 3,
        Custom = 4
    }

    public enum PipeStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2,
    }

    [Table("Pipe", Schema = "public")]
    public class Pipe
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(256)]
        public String NUri { get; set; }

        public Int32 Ordinal { get; set; }

        public PipeType Type { get; set; }

        public PipeStatus Status { get; set; }

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
