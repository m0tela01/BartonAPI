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
using Newtonsoft.Json;

namespace Barton1792DB.DAO
{
    public class Readers
    {
        private string BSConnectionString = CreateDB.BSConnectionString;
        private IDbConnection conn => new MySqlConnection(BSConnectionString);
        private string DataFolder { get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\DataFiles\\"; } }
        private string ProceduresFolder { get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Procedures\\"; } }

        private string GetEmployeesSql { get { return "GetEmployeeData"; } }
        private string GetTemplateSql { get { return "GetTemplate"; } }
        private string GetCurrentScheduleSql { get { return "GetCurrentSchedule"; } }
        private string GetScheduleHistoryDatesSql => "GetScheduleHistoryDates";
        private string GetScheduleHistoryByScheduleDateSql => "GetScheduleHistoryByScheduleDate";

        #region Scalars
        private string GetEmployeeByIdSql => "GetEmployeeById";

        #endregion Scalars


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
                JobId = int.Parse(rdr["jobid"].ToString()),
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
        public HistoryDate CreateHistoryDate(MySqlDataReader rdr)
        {
            HistoryDate historyDate = new HistoryDate()
            {
                ScheduleDate = DateTime.Parse(rdr["scheduledate"].ToString())
            };
            return historyDate;
        }
        #endregion Create Objects

        #region Readers
        //Need scalar - GetEmployee(Employee employee)
        #region Scalars
        /// <summary>
        /// Get employee by their id (clocknumber).
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public Employee GetEmployeeById(Employee employee, int employeeId)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(GetEmployeeByIdSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("@clockNumber", employeeId));
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            employee = CreateEmployee(rdr);
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
            return employee;
        }
        #endregion Scalars

        /// <summary>
        /// Get employees table as list of employees.
        /// </summary>
        /// <param name="Employees"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get current schedule table as list of schedules.
        /// </summary>
        /// <param name="Schedules"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Get template as list of templates.
        /// </summary>
        /// <param name="Templates"></param>
        /// <returns></returns>
        public List<Template> GetTemplates(List<Template> Templates)
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
        /// <summary>
        /// Get schedule as list of schedules based on the date of scheduling.
        /// </summary>
        /// <param name="Schedules"></param>
        /// <param name="ScheduleDate"></param>
        /// <returns></returns>
        public List<Schedule> GetScheduleHistoryByScheduleDate(List<Schedule> Schedules, string ScheduleDate)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(GetScheduleHistoryByScheduleDateSql, conn))
                    {// ToString("yyyy-MM-dd HH:mm:ss")
                        cmd.Parameters.Add(new MySqlParameter("@scheduleDate", ScheduleDate));
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while(rdr.Read())
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
        /// <summary>
        /// Get history dates for previous schedules as list of histories.
        /// </summary>
        /// <param name="HistoryDates"></param>
        /// <returns></returns>
        public List<HistoryDate> GetScheduleHistoryDates(List<HistoryDate> HistoryDates)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(GetScheduleHistoryDatesSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            HistoryDates.Add(CreateHistoryDate(rdr));
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
            return HistoryDates;
        }

        #endregion Readers

        #region Converters
        public List<Template> ConvertJsonToTemplates(string response)
        {
            List<Template> templates = new List<Template>();
            Template template;

            try
            {
                var data = JsonConvert.DeserializeObject<List<Template>>(response);
                foreach (var item in data)
                {
                    template = new Template();
                    template.DepartmentName = item.DepartmentName;
                    template.JobName = item.JobName;
                    template.Shift1 = item.Shift1;
                    template.Shift2 = item.Shift2;
                    template.Shift3 = item.Shift3;
                    templates.Add(template);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return templates;
        }
        #endregion Converters

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
