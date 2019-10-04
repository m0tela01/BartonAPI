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
                    MySqlTransaction trans = conn.BeginTransaction();
                    using (MySqlCommand cmd = new MySqlCommand(InsertCurrentScheduleSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Transaction = trans;
                        foreach (var item in Schedules)
                        {
                            cmd.Parameters[0].Value = item.SeniorityNumber;
                            cmd.Parameters[1].Value = item.ClockNumber;
                            cmd.Parameters[2].Value = item.EmployeeName;
                            cmd.Parameters[3].Value = item.JobName;
                            cmd.Parameters[4].Value = item.DepartmentName;
                            cmd.Parameters[5].Value = item.Shift1;
                            cmd.Parameters[6].Value = item.Shift2;
                            cmd.Parameters[7].Value = item.Shift3;
                            cmd.Parameters[8].Value = item.ShiftPreference;
                        }
                    }
                    trans.Commit();
                }
                catch(MySqlException ex)
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
