using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    [Table("Parameters", Schema = "public")]
    public class Parameter
    {
        [Key]
        public String Key { get; set; }

        public String Value { get; set; }
    }
}
