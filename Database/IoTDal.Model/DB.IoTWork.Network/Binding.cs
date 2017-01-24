using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    [Table("Binding", Schema = "public")]
    public class Binding
    {
        public Int32 Id { get; set; }

        public Int32 PacketsThrottling { get; set; }

        public Int32 PacketsMaxIntervalTimeout { get; set; }
    }
}
