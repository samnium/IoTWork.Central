using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum WriterType
    {
        Undefined = 0,
        Custom = 1,
        Console = 2,
        Csv = 3,
        Void = 4
    }

    [Table("Writer", Schema = "public")]
    public class Writer
    {
        public Int32 Id { get; set; }

        public WriterType Type { get; set; }

        [MaxLength(1024)]
        public String Parameter { get; set; }

        [MaxLength(128)]
        public String ModuleName { get; set; }

        [MaxLength(1024)]
        public String ModulePath { get; set; }

        // Foreign Key
        public Int32 RegionId { get; set; }
    }
}
