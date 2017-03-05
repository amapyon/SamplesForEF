using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample01
{
    [Table("Persons")]
    public class Person
    {
        private List<Board> _boards = new List<Board>();

        public int PersonId { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }

        public List<Board> Boards
        {
            get { return _boards; }
            set { _boards = value; }
        }

        public void AddBoard(Board board)
        {
            Boards.Add(board);
        }

    }
}
