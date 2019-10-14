using System;
using DVAC;
using System.IO;
using Barton1792DB.DAO;
using Barton1792DB.DBO;
using Barton1792DB.BO;
using System.Collections.Generic;

namespace Barton1792DB
{
    class Program
    {
        //Test for data layer and api.
        public static void Main(string[] args)
        {
            //CreateDB.ConnectToDB();
            CreateDB.CleanAndCreateTables();
            Console.ReadKey();


            Readers reader = new Readers();
            //List<Employee> EmployeeData = reader.GetStuff(new List<Employee>());
            List<Employee> CurrentEmployeeData = reader.GetEmployees(new List<Employee>());
            List<Template> CurrentSchedulingTemplate = reader.GetTemplate(new List<Template>());

            BartonSchedulerWeekday.GenerateSchedule(CurrentEmployeeData, CurrentSchedulingTemplate);
            List<Schedule> CurrentScheduled = reader.GetSchedules(new List<Schedule>());

            Context sch = Context.from_generic(CurrentScheduled);
            util.print(sch);
            util.print("HI");
            Console.ReadKey();
        }
    }
}
