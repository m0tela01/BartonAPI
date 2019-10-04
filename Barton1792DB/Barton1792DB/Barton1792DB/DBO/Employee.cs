using System;
using System.Collections.Generic;
using System.Text;

namespace Barton1792DB.DBO
{
    public class Employee
    {
        public int SeniorityNumber { get; set; }
        public int ClockNumber { get; set; }
        public DateTime SeniorityDate { get; set; }
        public string EmployeeName { get; set; }
        public int ShiftPreference { get; set; }
        public string JobName { get; set; }
        public string DepartmentName { get; set; }
        public int PrebuiltHours { get; set; }
        public int WeekendOTHours { get; set; }
        public int TotalHours { get; set; }
    }
}
