using Barton1792DB.DBO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Barton1792DB.DAO
{
    public class Writers
    {
        private string BSConnectionString = CreateDB.BSConnectionString;
        private string ClearScheduleBeforeInsertCurrentScheduleSql => "ClearScheduleBeforeInsertCurrentSchedule";
        private string ClearScheduleHistorySql => "ClearScheduleHistory";
        private string ClearScheduleTemplateBeforeInsertCurrentTemplateSql => "ClearScheduleTemplateBeforeInsertCurrentTemplate";
        private string InsertCurrentScheduleSql => "InsertCurrentSchedule";
        private string InsertPreviousScheduleToScheduleHistorySql => "InsertPreviousScheduleToScheduleHistory";
        private string InsertCurrentScheduleTemplateSql => "InsertCurrentScheduleTemplate";
        private string InsertOldScheduleToHistorySql => "InsertOldScheduleToHistory";
        private string UpdateEmployeeByIdSql = "UpdateEmployeeById";


        #region Clear
        /// <summary>
        /// Clear the current schedule before inserting the new schedule.
        /// </summary>
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
        /// <summary>
        /// Clear the schedule history if history is to big.
        /// </summary>
        public void ClearScheduleHistory()
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(ClearScheduleHistorySql, conn))
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
        /// <summary>
        /// Clear the current template before inserting the new template
        /// </summary>
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
        /// <summary>
        /// Insert the last schedule to the history table before inserting the new schedule.
        /// </summary>
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
        /// <summary>
        /// Insert the new current schedule.
        /// </summary>
        /// <param name="Schedules"></param>
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
                            //cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
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
        /// <summary>
        /// Insert the new current template.
        /// </summary>
        /// <param name="Templates"></param>
        public void InsertCurrentScheduleTemplate(List<Template> Templates)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    //MySqlTransaction trans = conn.BeginTransaction();
                    foreach (var item in Templates)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(InsertCurrentScheduleTemplateSql, conn))
                        {
                            //cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new MySqlParameter("@jobName", item.JobName));
                            cmd.Parameters.Add(new MySqlParameter("@departmentName", item.DepartmentName));
                            cmd.Parameters.Add(new MySqlParameter("@shift1", item.Shift1));
                            cmd.Parameters.Add(new MySqlParameter("@shift2", item.Shift2));
                            cmd.Parameters.Add(new MySqlParameter("@shift3", item.Shift3));
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
        /// <summary>
        /// Insert the last schedule to the history table before inserting new schedule.
        /// </summary>
        /// <param name="Schedules"></param>
        public void InsertOldScheduleToHistory(List<Schedule> Schedules)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    //MySqlTransaction trans = conn.BeginTransaction();
                    foreach (var item in Schedules)
                    {
                        using (MySqlCommand cmd = new MySqlCommand(InsertOldScheduleToHistorySql, conn))
                        {
                            //cmd.Transaction = trans;
                            cmd.CommandType = CommandType.StoredProcedure;
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
        #endregion Insert

        #region Update
        public void UpdateEmployeeById(Employee employee)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(UpdateEmployeeByIdSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("@clockNumber", employee.ClockNumber));
                        cmd.Parameters.Add(new MySqlParameter("@seniorityNumber", employee.SeniorityNumber));
                        cmd.Parameters.Add(new MySqlParameter("@shiftPref", employee.ShiftPreference));
                        cmd.Parameters.Add(new MySqlParameter("@empName", employee.EmployeeName));
                        cmd.Parameters.Add(new MySqlParameter("@jobname", employee.JobName));
                        cmd.Parameters.Add(new MySqlParameter("@senorityDate", employee.SeniorityDate));
                        cmd.Parameters.Add(new MySqlParameter("@prebuiltHours", employee.PrebuiltHours));
                        cmd.Parameters.Add(new MySqlParameter("@weekendOtHours", employee.WeekendOTHours));
                        cmd.Parameters.Add(new MySqlParameter("@totalHours", employee.TotalHours));
                        cmd.Parameters.Add(new MySqlParameter("@jobId", employee.JobId));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public void UpdateTemplateByJobId(Template template)
        {
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(UpdateEmployeeByIdSql, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("@jobName", template.JobName));
                        cmd.Parameters.Add(new MySqlParameter("@deptName", template.DepartmentName));
                        cmd.Parameters.Add(new MySqlParameter("@Shift1", template.Shift1));
                        cmd.Parameters.Add(new MySqlParameter("@Shift2", template.Shift2));
                        cmd.Parameters.Add(new MySqlParameter("@Shift3", template.Shift3));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        #endregion Update
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
