using IoTDal.Model;
using IoTDal.Model.DB.IoTWork.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDal.Provider.Npgsql
{
    public class DataContext : DbContext
    {
        public DataContext(System.Data.Common.DbConnection connection)
        : base(connection, true)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sample> Samples { get; set; }
        public DbSet<Sample_Archive> Samples_Archive { get; set; }

        public DbSet<SampleR1> SamplesR1 { get; set; }
        public DbSet<SampleR2> SamplesR2 { get; set; }
        public DbSet<SampleR3> SamplesR3 { get; set; }
        public DbSet<SampleR4> SamplesR4 { get; set; }
        public DbSet<SampleR5> SamplesR5 { get; set; }

        public DbSet<SampleR1_Archive> SamplesR1_Archive { get; set; }
        public DbSet<SampleR2_Archive> SamplesR2_Archive { get; set; }
        public DbSet<SampleR3_Archive> SamplesR3_Archive { get; set; }
        public DbSet<SampleR4_Archive> SamplesR4_Archive { get; set; }
        public DbSet<SampleR5_Archive> SamplesR5_Archive { get; set; }
    }
}
