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
        public int PersonId { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }

        public List<Board> Boards { get; set; }

    }
}
