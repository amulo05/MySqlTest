using MySqlTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest.Mapping
{
    public class ScoreMap : EntityTypeConfiguration<BaseScore>
    {
        public ScoreMap()
        {
            this.ToTable("Score");
            this.HasKey(x => new { x.StudentId, x.SubjectId });
        }
    }
}
