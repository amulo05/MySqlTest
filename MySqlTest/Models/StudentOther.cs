using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest.Models
{
    public class StudentOther
    {
        public String Name { get; set; }
        public int Age { get; set; }

        public string Other { get; set; }
    }
}
