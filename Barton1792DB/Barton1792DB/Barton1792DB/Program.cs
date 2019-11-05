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
            //CreateDB.CleanAndCreateTables();
            //Console.ReadKey();


            Readers reader = new Readers();
            Employee emp = reader.GetEmployeeById(new Employee(), 1248);
            util.print(emp);
            List<Schedule> scheduleByDate = reader.GetScheduleHistoryByScheduleDate(new List<Schedule>(), "2019-10-14 00:00:00");
            util.print(scheduleByDate);

            List<HistoryDate> histories = reader.GetScheduleHistoryDates(new List<HistoryDate>());
            util.print(histories);
            //List<Employee> EmployeeData = reader.GetStuff(new List<Employee>());
            //List<Employee> CurrentEmployeeData = reader.GetEmployees(new List<Employee>());
            //List<Template> CurrentSchedulingTemplate = reader.GetTemplate(new List<Template>());

            //BartonScheduler.GenerateWeekdaySchedule();
            //List<Schedule> CurrentScheduled = reader.GetSchedules(new List<Schedule>());

            //Context sch = Context.from_generic(CurrentScheduled);
            //Context temps = Context.from_generic(CurrentSchedulingTemplate);
            //Dictionary<string, Context> tempss = new Dictionary<string, Context>();
            //tempss["CurrentTemplate"] = temps;
            //Context.to_jsons("CurrentSchedule.json", tempss);
            
            util.print("HI");
            Console.ReadKey();
        }
    }
}
