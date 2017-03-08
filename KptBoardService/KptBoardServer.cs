using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using KptBoardModel;

namespace KptBoardService
{
    internal class KptBoardServer
    {
        private int _port;

        public KptBoardServer(int port)
        {
            _port = port;
        }

        internal void Listen()
        {
            var db = new KptRepository();

            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:" + _port + "/");
            listener.Start();

            Console.WriteLine("Listen...");
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest req = context.Request;
                HttpListenerResponse res = context.Response;

                Console.WriteLine($"{req.HttpMethod} {req.RawUrl} ");

                string method = req.HttpMethod;
                var cmd = Command.GetCommand(req.RawUrl);

                string responseString = @"{ ""system"":""kpt""}";

                try
                {
                    if (method == "GET")
                    {
                        responseString = GetApi(db, req, cmd);
                    }
                    else if (method == "POST")
                    {
                        responseString = PostApi(db, req, cmd);
                    }
                    else if (method == "PUT")
                    {
                        responseString = PutApi(db, req, cmd);
                    }
                    else if (method == "DELETE")
                    {
                        responseString = DeleteApi(db, req, cmd);
                    }
                    else
                    {
                        Console.WriteLine("不明");
                    }

                    res.StatusCode = 200;
                    res.ContentType = " application/json; charset=utf-8";
                    byte[] content = System.Text.Encoding.UTF8.GetBytes(responseString);
                    res.ContentLength64 = content.Length;
                    res.OutputStream.Write(content, 0, content.Length);
                }
                catch (Exception ex)
                {
                    res.StatusCode = 500; // 404 でも良いのだがここは雑に 500 にまとめておく
                    byte[] content = Encoding.UTF8.GetBytes(ex.Message);
                    res.OutputStream.Write(content, 0, content.Length);

                    content = Encoding.UTF8.GetBytes("\n");
                    res.OutputStream.Write(content, 0, content.Length);

                    content = Encoding.UTF8.GetBytes(ex.StackTrace);
                    res.OutputStream.Write(content, 0, content.Length);
                }
                res.Close();
            }

        }

        private string DeleteApi(KptRepository db, HttpListenerRequest req, Command cmd)
        {
            if (cmd.IsUsers && cmd.UserId != 0 && cmd.IsKptBoards && cmd.KptBoardId != 0)
            {
                // DELETE /v1/users/n/kptboards/n
                Console.WriteLine("ボード削除");

                var kptBoard = new KptBoard
                {
                    KptBoardId = cmd.KptBoardId
                };

                db.DeleteKptBoard(kptBoard);
                return "";
            }
            throw new ArgumentException("指定したAPIがありません。DELETE " + cmd.ToString());
        }

        private string PutApi(KptRepository db, HttpListenerRequest req, Command cmd)
        {
            if (!req.HasEntityBody)
            {
                throw new ArgumentException("指定したAPIに必要なデータがありません。POST " + cmd.ToString());
            }
            var body = new StreamReader(req.InputStream).ReadToEnd();
            var keyValues = HttpUtility.ParseQueryString(body);

            if (cmd.IsUsers && cmd.UserId != 0 && cmd.IsKptBoards && cmd.KptBoardId != 0)
            {
                // PUT /v1/users/n/kptboards/n
                Console.WriteLine("ボード修正");

                var kptBoard = db.GetKptBoardsById(cmd.UserId, cmd.KptBoardId);
                kptBoard.Time = DateTime.Now;
                kptBoard.Keep = keyValues.Get("keep");
                kptBoard.Problem = keyValues.Get("problem");
                kptBoard.Try = keyValues.Get("try");
                db.UpdateKptBoard(kptBoard);
                return "";
            }

            throw new ArgumentException("指定したAPIがありません。PUT " + cmd.ToString());
        }

        private static string PostApi(KptRepository db, HttpListenerRequest req, Command cmd)
        {
            if (!req.HasEntityBody)
            {
                throw new ArgumentException("指定したAPIに必要なデータがありません。POST " + cmd.ToString());
            }
            var body = new StreamReader(req.InputStream).ReadToEnd();
            var keyValues = HttpUtility.ParseQueryString(body);

            if (cmd.IsUsers && cmd.UserId == 0 && !cmd.IsKptBoards && cmd.KptBoardId == 0)
            {
                // POST /v1/users/
                Console.WriteLine("人追加");

                int? age = null;
                if (keyValues.Get("age") != null)
                {
                    age = int.Parse(keyValues.Get("age"));
                }

                var user = new User
                {
                    Name = keyValues.Get("name"),
                    Age = age
                };
                db.AddUser(user);
                return "";

            }
            if (cmd.IsUsers && cmd.UserId != 0 && cmd.IsKptBoards && cmd.KptBoardId == 0)
            {
                // POST /v1/users/n/kptboards/
                Console.WriteLine("ボード追加");

                var kptBoard = new KptBoard
                {
                    Time = DateTime.Now,
                    Keep = keyValues.Get("keep"),
                    Problem = keyValues.Get("problem"),
                    Try = keyValues.Get("try"),
                    UserId = cmd.UserId
                };
                db.AddKptBoard(kptBoard);
                return "";
            }

            throw new ArgumentException("指定したAPIがありません。POST " + cmd.ToString());
        }

        private static string GetApi(KptRepository db, HttpListenerRequest req, Command cmd)
        {
            string responseString;

            if (cmd.IsUsers && cmd.UserId == 0 && !cmd.IsKptBoards)
            {
                // GET /v1/users/
                Console.WriteLine("人の一覧");

                var sb = new StringBuilder("[");

                var users = db.GetUsers();
                foreach (var user in users)
                {
                    sb.Append("{");
                    sb.Append(string.Format($@"""userId"":{user.UserId},"));
                    sb.Append(string.Format($@"""name"":""{user.Name}"""));
                    sb.Append("},");
                }
                if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
                return sb.ToString();
            }

            if (cmd.IsUsers && cmd.UserId != 0 && !cmd.IsKptBoards)
            {
                // GET /v1/users/n
                Console.WriteLine("特定の人の詳細");

                var user = db.UserFindById(cmd.UserId);
                var sb = new StringBuilder("{");
                sb.Append(string.Format($@"""userId"":{user.UserId},"));
                sb.Append(string.Format($@"""name"":""{user.Name}"","));
                if (user.Age == null)
                {
                    sb.Append(string.Format($@"""age"":null"));
                }
                else
                {
                    sb.Append(string.Format($@"""age"":{user.Age}"));
                }
                sb.Append("}");
                return sb.ToString();
            }

            if (cmd.IsUsers && cmd.UserId != 0 && cmd.IsKptBoards && cmd.KptBoardId == 0)
            {
                // GET /v1/users/n/kptboards
                Console.WriteLine("特定の人のボードの一覧");

                // User user = db.UserFindById(cmd.userId);
                var kptBoards = db.GetKptBoardsByUserId(cmd.UserId);

                var sb = new StringBuilder("[");
                foreach (var kptBoard in kptBoards)
                {
                    sb.Append("{");
                    sb.Append(string.Format($@"""boardId"":{kptBoard.KptBoardId},"));
                    sb.Append(string.Format($@"""time"":""{kptBoard.Time}"","));
                    sb.Append(string.Format($@"""keep"":""{kptBoard.Keep}"","));
                    sb.Append(string.Format($@"""problem"":""{kptBoard.Problem}"","));
                    sb.Append(string.Format($@"""try"":""{kptBoard.Try}"""));
                    sb.Append("},");
                }
                if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
                sb.Append("]");
                return sb.ToString();
            }

            if (cmd.IsUsers && cmd.UserId != 0 && cmd.IsKptBoards && cmd.KptBoardId != 0)
            {
                // GET /v1/users/n/kptboards/n
                Console.WriteLine("特定の人の特定のボードの詳細");

                var kptBoard = db.GetKptBoardsById(cmd.UserId, cmd.KptBoardId);
                return responseString = string.Format(@"{{""boardId"":{0},""date"":""{1}"",""keep"":""{2}"",""problem"":""{3}"",""try"":""{4}""}}", kptBoard.KptBoardId, kptBoard.Time, kptBoard.Keep, kptBoard.Problem, kptBoard.Try);
            }

            throw new ArgumentException("指定したAPIがありません。GET " + cmd.ToString());
        }
    }
}
