using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KptBoardSystem
{
    [Table("KptBards")] // テーブル名をクラス名の複数形に指定
    public class KptBoard
    {
        public int KptBoardId { get; set; } // 主キー

        [Required]
        public DateTime Time { get; set; }

        public string Keep { get; set; }
        public string Problem { get; set; }
        public string Try { get; set; }

        public int UserId { get; set; } // 外部キーとして、UserクラスのUserIdと同じ名称を指定
    }
}
