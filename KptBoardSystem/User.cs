using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KptBoardSystem
{
    [Table("Users")]
    public class User
    {
        private List<KptBoard> _kptBoards = new List<KptBoard>();  //

        public int UserId { get; set; }  // 主キー
        [Required] // 名前は必須
        public string Name { get; set; }
        public int? Age { get; set; } // nullも可能な「int?」型

        public List<KptBoard> KptBoards
        {
            get { return _kptBoards; }
            set { _kptBoards = value; }
        }

        public void AddKptBoard(KptBoard kptBoard)
        {
            _kptBoards.Add(kptBoard);
        }
    }
}
