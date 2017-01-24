using IoTDal.Model;
using IoTDal.Model.DB.IoTWork.Network;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Provider.Npgsql
{
    public class NetworkContext : DbContext
    {
        public NetworkContext(System.Data.Common.DbConnection connection)
        : base(connection, true)
        {
        }

        //public NetworkContext() : base("name=IoTWorkNetworkConnectionString")
        //{
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Network> Networks {get; set; }
        public DbSet<Binding> Bindings { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Ring> Rings { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Chain> Chains { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Trigger> Triggers { get; set; }
        public DbSet<Pipe> Pipes { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Writer> Writers { get; set; }
    }
}
