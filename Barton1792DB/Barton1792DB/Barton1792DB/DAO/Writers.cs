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
        public string ClearScheduleBeforeInsertCurrentScheduleSql { get { return "ClearScheduleBeforeInsertCurrentSchedule"; } }
        public string ClearScheduleTemplateBeforeInsertCurrentTemplateSql { get { return "ClearScheduleTemplateBeforeInsertCurrentTemplate"; } }
        public string InsertCurrentScheduleSql { get { return "InsertCurrentSchedule"; } }
        public string InsertPreviousScheduleToScheduleHistorySql { get { return "InsertPreviousScheduleToScheduleHistory"; } }
        public string InsertCurrentScheduleTemplateSql { get { return "InsertCurrentScheduleTemplate"; } }


        #region Clear
        public void ClearScheduleBeforeInsert()
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(ClearScheduleBeforeInsertCurrentScheduleSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public void ClearScheduleTemplateBeforeInsert()
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(ClearScheduleTemplateBeforeInsertCurrentTemplateSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        #endregion Clear

        #region Insert
        public void InsertPreviousScheduleToScheduleHistory()
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(InsertPreviousScheduleToScheduleHistorySql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
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
        public void InsertCurrentScheduleTemplate(List<Template> Templates)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    foreach (var item in Templates)
                    {
                        //MySqlTransaction trans = conn.BeginTransaction();
                        using (MySqlCommand cmd = new MySqlCommand(InsertCurrentScheduleTemplateSql, conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            //cmd.Transaction = trans;
                            cmd.Parameters.Add(new MySqlParameter("@jobName", item.JobName));
                            cmd.Parameters.Add(new MySqlParameter("@departmentName", item.DepartmentName));
                            cmd.Parameters.Add(new MySqlParameter("@shift1", item.Shift1));
                            cmd.Parameters.Add(new MySqlParameter("@shift2", item.Shift2));
                            cmd.Parameters.Add(new MySqlParameter("@shift3", item.Shift3));
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
        #endregion Insert
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
