using MySqlTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest.Mapping
{
    public class StudentMap : EntityTypeConfiguration<BaseStudent>
    {
        public StudentMap()
        {
            this.ToTable("Student");
        }
    }
}
