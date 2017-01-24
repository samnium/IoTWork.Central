using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.SHat.Writers
{
    public class PiSHatContext : DbContext
    {
        public PiSHatContext(System.Data.Common.DbConnection connection)
        : base(connection, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PiSHatData_Humidity> Humidities { get; set; }
        public DbSet<PiSHatData_Pressure> Pressures { get; set; }
        public DbSet<PiSHatData_Temperature> Temperatures { get; set; }
    }
}
