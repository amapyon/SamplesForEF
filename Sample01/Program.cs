using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

                //InsertRecord(db);

                var person = db.Persons.Where(p => p.Name == "Test04").Include("Boards").First();
                person.AddBoard(new Board { Date = DateTime.Now, Keep = "KEEP" + DateTime.Now.Hour + DateTime.Now.Minute });
                db.Update(person);

                db.SaveChanges();

                SelectRecordAll(db);
                //SelectRecordByName(db, "Test03");
            }
        }

        private static void SelectRecordByName(KptBoardContext db, string name)
        {
            //            var persons = db.Persons.Where(p => p.Name == name).Include(person => person.Boards);
            var persons = db.Persons.Where(p => p.Name == name).Include("Boards");

            foreach (var person in persons)
            {
                Console.WriteLine($"Name={person.Name}, Age={person.Age}");

                foreach (var board in person.Boards)
                {
                    Console.WriteLine($"Date={board.Date}, Keep={board.Keep}");
                }
            }
        }

        private static void SelectRecordAll(KptBoardContext db)
        {
            var persons = db.Persons.Include(person => person.Boards);

            foreach (var person in persons)
            {
                Console.WriteLine($"Name={person.Name}, Age={person.Age}");

                foreach (var board in person.Boards)
                {
                    Console.WriteLine($"Date={board.Date}, Keep={board.Keep}");
                }
            }
        }

        private static void InsertRecord(KptBoardContext db)
        {
            Person person;

            //person = new Person { Name = "Test01" };
            //db.Add(person);

            //person = new Person { Name = "Test02" };
            //db.Add(person);

            //person = new Person { Name = "Test03", Age = 3 };
            //db.Add(person);

            //List<Board> boards = new List<Board>{
            //    new Board { Date= DateTime.Parse("2017/03/04"), Keep = "KEEP1"},
            //    new Board { Date= DateTime.Parse("2017/03/05"), Keep = "KEEP2"},
            //};
            //person = new Person { Name = "Test04", Age = 4, Boards = boards };
            //db.Add(person);

            //person = new Person
            //{
            //    Name = "Test05",
            //    Age = 5,
            //    Boards = new List<Board> { 
            //    new Board { Date = DateTime.Parse("2017/03/03"), Keep = "KEEP3" },
            //    new Board { Date = DateTime.Parse("2017/03/04"), Keep = "KEEP4" },
            //    }
            //};
            //db.Add(person);

            person = new Person
            {
                Name = "Test06",
                Age = 6,
            };
            person.AddBoard(new Board { Date = DateTime.Parse("2017/03/05"), Keep = "KEEP61" });
            person.AddBoard(new Board { Date = DateTime.Parse("2017/03/06"), Keep = "KEEP62" });

            db.Add(person);

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
