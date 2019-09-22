using System;
using DVAC;
using System.IO;


namespace Barton1792DB
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateDB.ConnectToDB();
            CreateDB.CreateAndCleanEmployeeTable();
            Console.ReadKey();
        }
    }
}
