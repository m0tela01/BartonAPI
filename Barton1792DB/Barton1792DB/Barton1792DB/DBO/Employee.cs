using System;
using System.Collections.Generic;
using System.Text;

namespace Barton1792DB.DBO
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public int ShiftPreference { get; set; }
        public string EmployeeName { get; set; }
        public string Job { get; set; }
        public int JobId { get; set; }
    }
}
