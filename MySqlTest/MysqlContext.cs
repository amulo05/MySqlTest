using MySql.Data.Entity;
using MySqlTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MysqlContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public MysqlContext()
            : base()
        {

        }

        public MysqlContext(DbConnection existingConnection, bool contextOwnsConnection)
             : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>().MapToStoredProcedures();
        }
    }
}
