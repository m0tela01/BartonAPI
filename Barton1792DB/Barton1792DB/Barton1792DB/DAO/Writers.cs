using Barton1792DB.DBO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Barton1792DB.DAO
{
    public class Writers
    {
        private string BSConnectionString = CreateDB.BSConnectionString;
        public string InsertCurrentScheduleSql { get { return "InsertCurrentSchedule"; } }
        public string UpdateCurrentScheduleTemplateSql { get { return "InsertCurrentScheduleTemplate"; } }

        public string InsertSql { get { return ""; } }
        public string UpdateSql { get { return ""; } }
        public string DeleteSql { get { return ""; } }

        //Need Before insert should copy previous schedule into json for old schedules
        public void InsertCurrentSchedule(List<Schedule> Schedules)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    //MySqlTransaction trans = conn.BeginTransaction();
                    foreach (var item in Schedules)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(InsertCurrentScheduleSql, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Transaction = trans;
                            cmd.Parameters.Add(new MySqlParameter("@senioritynumber", item.SeniorityNumber));
                            cmd.Parameters.Add(new MySqlParameter("@clocknumber", item.ClockNumber));
                            cmd.Parameters.Add(new MySqlParameter("@empname", item.EmployeeName));
                            cmd.Parameters.Add(new MySqlParameter("@jobname", item.JobName));
                            cmd.Parameters.Add(new MySqlParameter("@departmentname", item.DepartmentName));
                            cmd.Parameters.Add(new MySqlParameter("@shift", item.Shift));
                            cmd.Parameters.Add(new MySqlParameter("@shiftpref", item.ShiftPreference));
                            cmd.Parameters.Add(new MySqlParameter("@scheduledate", item.ScheduleDate));


                            //cmd.Parameters.AddWithValue("@senioritynumber", item.SeniorityNumber);
                            //cmd.Parameters.AddWithValue("@clocknumber", item.ClockNumber);
                            //cmd.Parameters.AddWithValue("@empname", item.EmployeeName);
                            //cmd.Parameters.AddWithValue("@jobname", item.JobName);
                            //cmd.Parameters.AddWithValue("@departmentname", item.DepartmentName);
                            //cmd.Parameters.AddWithValue("@shift", item.Shift);
                            //cmd.Parameters.AddWithValue("@shiftpref", item.ShiftPreference);
                            //cmd.Parameters.AddWithValue("@scheduledate", item.ScheduleDate.ToString("yyyy-MM-dd HH:mm:ss"));
                            //cmd.Parameters.Clear();

                            //cmd.Parameters["senioritynumber"].Value = item.SeniorityNumber;
                            //cmd.Parameters["clocknumber"].Value = item.ClockNumber;
                            //cmd.Parameters["employeename"].Value = item.EmployeeName;
                            //cmd.Parameters["jobname"].Value = item.JobName;
                            //cmd.Parameters["departmentname"].Value = item.DepartmentName;
                            //cmd.Parameters["shift"].Value = item.Shift;
                            //cmd.Parameters["shiftpreference"].Value = item.ShiftPreference;
                            //cmd.Parameters["scheduledate"].Value = item.ScheduleDate;

                            //cmd.Parameters[0].Value = item.SeniorityNumber;
                            //cmd.Parameters[1].Value = item.ClockNumber;
                            //cmd.Parameters[2].Value = item.EmployeeName;
                            //cmd.Parameters[3].Value = item.JobName;
                            //cmd.Parameters[4].Value = item.DepartmentName;
                            //cmd.Parameters[5].Value = item.Shift;
                            //cmd.Parameters[6].Value = item.ShiftPreference;
                            //cmd.Parameters[7].Value = item.ScheduleDate;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    //trans.Commit();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        //Need should be an update?
        public void UpdateCurrentScheduleTemplate(List<Template> Templates)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlTransaction trans = conn.BeginTransaction();
                    using (MySqlCommand cmd = new MySqlCommand(UpdateCurrentScheduleTemplateSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = trans;
                        foreach (var item in Templates)
                        {

                            cmd.Parameters[0].Value = item.JobName;
                            cmd.Parameters[1].Value = item.DepartmentName;
                            cmd.Parameters[2].Value = item.Shift1;
                            cmd.Parameters[3].Value = item.Shift2;
                            cmd.Parameters[4].Value = item.Shift3;
                        }
                    }
                    trans.Commit();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        #region Make Generics
        /// <summary>
        /// For easy Insert, Delete, Update
        /// </summary>
        /// <param name="sql"></param>
        public void DoNonQuery(string sql)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        #endregion Make Generics
    }
}
