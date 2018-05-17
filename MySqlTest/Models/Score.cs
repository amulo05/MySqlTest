using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlTest.Models
{
    public class Score : BaseScore
    {
        public static readonly Score Dao = new Score();
    }
}
