using Xunit;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KptBoardSystem.Test
{
    public class KptBoardModelTest
    {
        [Fact(DisplayName = "KptBoardを持っているUserを削除する")]
        public void TestDeleteUserHasKptBoard()
        {
            using (var db = new KptBoardModel())
            {
                // Includeしていないので、Userの情報しか取れない
                var user = db.Users.Where((u) => u.Name == "User03").First();

                db.Remove(user);   // 削除
                db.SaveChanges();
            }
        }

        [Fact(DisplayName = "KptBoardを削除する")]
        public void TestDeleteKptBoard()
        {
            using (var db = new KptBoardModel())
            {
                var kptBoard = db.KptBoards.Where(b => b.Problem == "問題だけ").First();
                db.Remove(kptBoard);   // 削除
                db.SaveChanges();
            }
        }

        [Fact(DisplayName = "KptBoardを修正する")]
        public void TestUpdateKptBoard()
        {
            using (var db = new KptBoardModel())
            {
                var kptBoard = db.KptBoards.Where(b => b.Problem.Contains("続ける")).First();
                kptBoard.Problem = "問題だけ";
                db.Update(kptBoard);   // 更新
                db.SaveChanges();
            }
        }

        [Fact(DisplayName = "新規にユーザーとKptBoardを追加する")]
        public void TestInsertUserAndKptBoard()
        {
            using (var db = new KptBoardModel())
            {
                var user = new User   // Userが1つ、KptBoardが3つのインスタンスを作り上げる
                {
                    Name = "User03", Age = 3,
                    KptBoards =
                    {
                        new KptBoard { Time = DateTime.Now, Keep = "続けることだけ" },
                        new KptBoard { Time = DateTime.Now, Problem = "続けることだけ" },
                        new KptBoard { Time = DateTime.Now, Try = "試すことだけ" },
                    }
                };
                db.Add(user);   // 追加
                db.SaveChanges();
            }
        }

        [Fact(DisplayName = "既存のユーザーにKptBoardを追加する")]
        public void TestInsertKptBoard()
        {
            using (var db = new KptBoardModel())
            {
                var user02 = db.Users.Where((u) => u.Name == "User02").Include("KptBoards").First();
                user02.AddKptBoard(new KptBoard
                {
                    Time = DateTime.Now,
                    Keep = "続けること", Problem = "不満なこと", Try = "次に試すこと"
                });

                db.Update(user02);  // 更新
                db.SaveChanges();
            }
        }

        [Fact(DisplayName = "Userが追加できるか")]
        public void TestInsertUser()
        {
            using (var db = new KptBoardModel())
            {
                // 全レコードを削除
                foreach (var u in db.Users.ToArray())
                {
                    db.Remove<User>(u);
                }

                db.Add(new User { Name = "User01" });
                db.Add(new User { Name = "User02", Age = 2 });
                db.SaveChanges();
            }
        }

    }
}
