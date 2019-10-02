using System;
using DVAC;
using System.IO;
using Barton1792DB.DAO;
using Barton1792DB.DBO;
using System.Collections.Generic;

namespace Barton1792DB
{
    class Program
    {
        public static void Main(string[] args)
        {
            //CreateDB.ConnectToDB();
            //CreateDB.CreateAndCleanEmployeeTable();
            //CreateDB.CallProcedure();
            Readers reader = new Readers();

            List<Employee> EmployeeData = reader.GetEmployees();
            Context emp = Context.from_generic(EmployeeData);
            util.print(emp);
            Console.ReadKey();

        }
    }
}
