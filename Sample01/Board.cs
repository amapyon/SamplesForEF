using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample01
{
    [Table("Boards")]
    public class Board
    {
        public int BoardID { get; set; }
        public DateTime Date { get; set; }
        public string Keep { get; set; }

        public int PersonID { get; set; }
    }
}
