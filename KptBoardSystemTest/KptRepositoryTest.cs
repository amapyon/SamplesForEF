using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace KptBoardSystem.Test
{
    public class KptRepositoryTest : IDisposable
    {
        private ITestOutputHelper _output;

        public KptRepositoryTest(ITestOutputHelper output)
        {
            _output = output;
            Truncate();
        }

        public void Dispose()
        {
            //Truncate();
        }

        // テーブルを削除する
        private void Truncate()
        {
            using (var db = new KptBoardModel())
            {
                foreach (var u in db.Users.ToArray())
                {
                    db.Remove<User>(u);
                }
                db.SaveChanges();
            }
        }

        [Fact(DisplayName = "Userが登録できるか")]
        public void TestAddUser()
        {
            using (var db = new KptRepository())
            {
                Assert.Equal(0, db.GetUsers().Count);
                db.AddUser(new User { Name = "Test01" });
                Assert.Equal(1, db.GetUsers().Count);
                db.AddUser(new User { Name = "Test02", Age = 2 });
                Assert.Equal(2, db.GetUsers().Count);
            }
        }

        [Fact(DisplayName = "KptBoardを持ったUserが登録できるか")]
        public void TestAddUserWithKptBoard()
        {
            using (var db = new KptRepository())
            {
                var user = new User
                {
                    Name = "Test01",
                    KptBoards =
                    {
                        new KptBoard {Time = DateTime.Now, Keep = "K011", Problem = "P011", Try="T011"}
                    }
                };
                Assert.Equal(0, user.UserId);
                Assert.Equal(0, db.GetUsers().Count);
                db.AddUser(user);
                Assert.NotEqual(0, user.UserId);    // AddUserした時点でUserIdが設定される
                Assert.Equal(1, db.GetUsers().Count);
                Assert.Equal(1, db.GetKptBoardsByUserId(user.UserId).Count);
            }
        }

        [Fact(DisplayName = "IDを指定してUserを取得する")]
        public void TestFindUserById()
        {

        }

        [Fact(DisplayName = "名前を指定してUserを検索する")]
        public void TestSearchUserByName()
        {

        }

    }
}
