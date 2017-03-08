using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KptBoardService
{
    public class Command
    {
        // "/v1/users/userId/kptboards/kptBoardId/
        private static Regex _r = new Regex(@"\/v1\/([a-z]*)\/?([0-9]*)\/?([a-z]*)\/?([0-9]*)");

        public bool IsUsers { get; private set; }
        public int UserId { get; private set; }
        public bool IsKptBoards { get; private set; }
        public int KptBoardId { get; private set; }

        private string _url;
        private Command(string url)
        {
            _url = url;
        }

        public static Command GetCommand(string uri)
        {
            var match = _r.Match(uri);

            var c = new Command(uri);
            if (match.Groups[1].Value == "users") { c.IsUsers = true; }
            if (match.Groups[2].Value != "") { c.UserId = int.Parse(match.Groups[2].Value); }
            if (match.Groups[3].Value == "kptboards") { c.IsKptBoards = true; }
            if (match.Groups[4].Value != "") { c.KptBoardId = int.Parse(match.Groups[4].Value); }

            return c;
        }

        public override string ToString()
        {
            return _url;
        }
    }
}
