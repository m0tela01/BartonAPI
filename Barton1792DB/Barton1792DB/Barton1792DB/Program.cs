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
        //Test for data layer and api.
        public static void Main(string[] args)
        {
            //CreateDB.ConnectToDB();
            //CreateDB.CreateAndCleanEmployeeTable();
            //CreateDB.CallProcedure();
            Readers reader = new Readers();

            //List<Employee> EmployeeData = reader.GetStuff(new List<Employee>());
            List<Employee> CurrentEmployeeData = reader.GetEmployees(new List<Employee>());
            List<Schedule> CurrentScheduled = reader.GetSchedules(new List<Schedule>());
            List<Template> CurrentSchedulingTemplate = reader.GetTemplate(new List<Template>());
            Context emp = Context.from_generic(CurrentEmployeeData);
            util.print(emp);
            Console.ReadKey();
        }
    }
}
