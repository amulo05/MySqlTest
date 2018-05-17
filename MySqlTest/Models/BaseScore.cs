using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest.Models
{
    public class BaseScore : BaseModel<BaseScore>
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        public double Value { get; set; }
    }
}
