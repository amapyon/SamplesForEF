using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KptBoardSystem
{
    public class KptRepository : IDisposable
    {
        private KptBoardModel _db = new KptBoardModel();

        public KptRepository()
        {
            // ロガーを設定する
            var serviceProvider = _db.GetInfrastructure<IServiceProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new LoggerProvider());
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        // 人の一覧を返す
        public List<User> GetUsers()
        {
            return _db.Users.ToList();
        }

        // 指定された人を返す
        public User UserFindById(int userId)
        {
            return _db.Users.Where(u => u.UserId == userId).First();
        }

        // 指定された人のボードの一覧を返す
        public List<KptBoard> GetKptBoardsByUserId(int userId)
        {
            return _db.KptBoards.Where(u => u.UserId == userId).ToList();
        }

        // 指定された人の、指定されたボードを返す
        public KptBoard GetKptBoardsById(int userId, int kptBoardId)
        {
            return _db.KptBoards.Where(u => (u.UserId == userId && u.KptBoardId == kptBoardId)).First();
        }

        // ボードを追加する
        public void AddKptBoard(KptBoard kptBoard)
        {
            User user = _db.Users.Where(u => u.UserId == kptBoard.UserId).First();
            user.AddKptBoard(kptBoard);

            _db.SaveChanges();
        }

        // 人を追加する
        public void AddUser(User user)
        {
            _db.Add(user);
            _db.SaveChanges();
        }

        // ボードを更新する
        public void UpdateKptBoard(KptBoard kptBoard)
        {
            _db.Update(kptBoard);
            _db.SaveChanges();
        }

        // ボードを削除する
        public void DeleteKptBoard(KptBoard kptBoard)
        {
            _db.Remove(kptBoard);
            _db.SaveChanges();
        }
    }

}
