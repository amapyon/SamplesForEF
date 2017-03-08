using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace KptBoardSystem
{
    public class KptBoardModel : DbContext
    {
        public DbSet<User> Users { get; internal set; }
        public DbSet<KptBoard> KptBoards { get; internal set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = 
                new SqliteConnectionStringBuilder { DataSource = "../../../kpt.db" }.ToString();
            optionsBuilder.UseSqlite(new SqliteConnection(connectionString));
        }

    }
}
