using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Barton1792DB.DBO;
using DVAC;
using MySql.Data.MySqlClient;

namespace Barton1792DB.DAO
{
    public class Readers
    {
        private string BSConnectionString = CreateDB.BSConnectionString;
        private IDbConnection conn => new MySqlConnection(BSConnectionString);
        private string GetEmployeesSql { get { return "GetEmployeeData"; } }
        private string GetTemplateSql { get { return "GetTemplate"; } }
        private string GetCurrentScheduleSql { get { return "GetCurrentSchedule"; } }
        private string DataFolder { get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\DataFiles\\"; } }
        private string ProceduresFolder { get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Procedures\\"; } }

        #region Create Objects
        public Employee CreateEmployee(MySqlDataReader rdr)
        {
            Employee emp = new Employee()
            {
                ClockNumber = int.Parse(rdr["clocknumber"].ToString()),
                SeniorityNumber = int.Parse(rdr["senioritynumber"].ToString()),
                ShiftPreference = int.Parse(rdr["shiftpref"].ToString()),
                EmployeeName = rdr["empname"].ToString(),
                SeniorityDate = DateTime.Parse(rdr["senioritydate"].ToString()),      //NEED fix date format
                PrebuiltHours = int.Parse(rdr["prebuilthours"].ToString()),
                WeekendOTHours = int.Parse(rdr["weekendothours"].ToString()),
                TotalHours = int.Parse(rdr["totalhours"].ToString()),
                JobName = rdr["jobname"].ToString(),
                DepartmentName = rdr["departmentname"].ToString()
            };
            return emp;
        }
        public Schedule CreateSchedule(MySqlDataReader rdr)
        {
            Schedule sch = new Schedule()
            {
                SeniorityNumber = int.Parse(rdr["senioritynumber"].ToString()),
                ClockNumber = int.Parse(rdr["clocknumber"].ToString()),
                EmployeeName = rdr["empname"].ToString(),
                JobName = rdr["jobname"].ToString(),
                DepartmentName = rdr["departmentname"].ToString(),
                Shift = int.Parse(rdr["shift"].ToString()),
                ShiftPreference = int.Parse(rdr["shiftpref"].ToString()),
                ScheduleDate = DateTime.Parse(rdr["scheduledate"].ToString())
            };
            return sch;
        }
        public Template CreateTemplate(MySqlDataReader rdr)
        {
            Template temp = new Template()
            {
                DepartmentName = rdr["departmentname"].ToString(),
                JobName = rdr["jobname"].ToString(),
                Shift1 = int.Parse(rdr["s1"].ToString()),
                Shift2 = int.Parse(rdr["s2"].ToString()),
                Shift3 = int.Parse(rdr["s3"].ToString())
            };
            return temp;
        }
        #endregion Create Objects

        #region Get Objects
        //Need scalar - GetEmployee(Employee employee)
        public List<Employee> GetEmployees(List<Employee> Employees)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(GetEmployeesSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Employees.Add(CreateEmployee(rdr));
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return Employees;
        }
        public List<Schedule> GetSchedules(List<Schedule> Schedules)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(GetCurrentScheduleSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Schedules.Add(CreateSchedule(rdr));
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return Schedules;
        }
        public List<Template> GetTemplate(List<Template> Templates)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(GetTemplateSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Templates.Add(CreateTemplate(rdr));
                        }
                        rdr.Close();
                    }
                    conn.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return Templates;
        }
        #endregion Get Objects

        #region Need to make generic readers
        public Context GetEmployees()
        {
            Context c = Context.from_sql_query(conn, "select * from  sazerac.employee");
            return c;
        }
        public List<T> GetStuff<T>(List<T> objectType, MySqlCommand cmd)
        {
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                try
                {
                    objectType = GetStuff<T>(objectType, GetEmployeesSql, CreateEmployee(rdr));
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return objectType;
        }
        public List<T> GetStuff<T>(List<T> objectType, string sqlString, object CreateObject)
        {
            //Type objectsType = objectOfChoice.GetType().GetGenericArguments().Single();
            //List<objectsType> objects = new List<T>();

            //objectType = new List<objectsType>();
            //(List<T>)objectType = Convert.ChangeType(objectType, typeof(List<T>))
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sqlString, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        objectType.Add((T)CreateObject);
                    }
                    rdr.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return objectType;
        }
        #endregion Need to make generic readers
    }
}
