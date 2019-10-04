using Barton1792DB.DBO;
using System;
using System.Collections.Generic;
using System.Text;
using Barton1792DB.DAO;

namespace Barton1792DB.BO
{
    public static class BartonSchedulerWeekday
    {
        private static string NotEligibleToWork { get { return "ABSENT"; } }

        public static Readers readers = new Readers();
        public static Writers writers = new Writers();

        public static List<Schedule> GenerateSchedule()
        {
            List<Schedule> schedules = new List<Schedule>();    // For insert
            List<Employee> employees = readers.GetEmployees(new List<Employee>());
            //Need deserialized object from web app to update existing
            writers.UpdateCurrentScheduleTemplate(new List<Template>());
            List<Template> template = readers.GetTemplate(new List<Template>());
            //
            // put business logic here for scheduling
            //
            writers.InsertCurrentSchedule(schedules);
            return schedules;
        }
    }
}
