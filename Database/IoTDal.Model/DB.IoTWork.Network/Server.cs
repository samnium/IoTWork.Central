using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Model.DB.IoTWork.Network
{
    public enum Protocol
    {
        Undefined = 0,
        Udp = 1,
        Tcp = 2
    }

    public enum ServerType
    {
        Undefined = 0,
        Data = 1,
        Management = 2
    }

    public enum ServerStatus
    {
        Undefined = 0,
        Active = 1,
        NotActive = 2,
    }

    [Table("Server", Schema = "public")]
    public class Server
    {
        public Int32 Id { get; set; }

        [MaxLength(64)]
        public String Name { get; set; }

        [MaxLength(64)]
        public String NUri { get; set; }

        public Protocol Protocol { get; set; }

        public ServerType Type { get; set; }

        public ServerStatus Status { get; set; }

        public String Uri { get; set; }
    } 
}
