using MySqlTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest.Mapping
{
    public class SubjectMap : EntityTypeConfiguration<BaseSubject>
    {
        public SubjectMap()
        {
            this.ToTable("Subject");
        }
    }
}
