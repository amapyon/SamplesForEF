namespace Sample01
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new KptBoardContext())
            {
                //DropDatabase(db);
                //CreateDatabase(db);

                InsertRecord(db);

            }
        }


        private static void InsertRecord(KptBoardContext db)
        {
            var person = new Person { Name = "Test01" };
            db.Add(person);

            person = new Person { Name = "Test02" };
            db.Add(person);

//            person = new Person { Name = "Test02", Age = 2 };
//            db.Add(person);

            db.SaveChanges();
        }

        private static void CreateDatabase(KptBoardContext db)
        {
            db.Database.EnsureCreated();
        }

        private static void DropDatabase(KptBoardContext db)
        {
            db.Database.EnsureDeleted();
        }
    }
}
