using System;
using System.Collections.Generic;
using System.Text;

namespace Barton1792DB.DBO
{
    public class Schedule
    {
        public int SeniorityNumber { get; set; }
        public int ClockNumber { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public string JobName { get; set; }
        public int Shift1 { get; set; }
        public int Shift2 { get; set; }
        public int Shift3 { get; set; }
        public int ShiftPreference { get; set; }
    }
}
