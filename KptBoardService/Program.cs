using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KptBoardService
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                new KptBoardServer(8080).Listen();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
