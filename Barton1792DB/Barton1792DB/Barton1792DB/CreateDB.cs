using System;
using System.Collections.Generic;
using DVAC;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Linq;

namespace Barton1792DB
{
    public static class CreateDB
    {
        #region Props
        private static MySqlConnection conn = new MySqlConnection();
        public static string BSConnectionString { get { return "server=107.180.51.29;uid=sazerac_user;pwd=sazerac2019;database=sazerac"; } }
        private static string DataFolder { get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\DataFiles\\"; } }
        private static string SeniorityFile { get { return DataFolder + "UL Master Seniority List.xlsx"; } }
        private static string WeekDaySchedule { get { return DataFolder + "6 DAY SCHEDULE WEEK OF 9-2-19.xlsm"; } }
        private static string MasterList { get { return DataFolder + "Copy of MASTER SENIORITY LISTING.xlsx"; } }
        private static FileInfo sqlFile = new FileInfo(DataFolder + "barton1792CreateTables.sql");
        #endregion Props

        public static void ConnectToDB()
        {
            try
            {
                conn.ConnectionString = BSConnectionString;
                conn.Open();
                util.print("connected to BS Database\n");
            }
            catch (MySqlException ex)
            {
                util.print(ex.Message);
            }
        }
        public static void CreateAndCleanEmployeeTable()
        {
            Context cMaster = Context.from_excel(MasterList, 2, 1, 1, 1, 15, true); 
            conn.ConnectionString = BSConnectionString;

            cMaster.columnrename("Seniority Number", "senioritynumber");
            cMaster.columnrename("Clock Number", "clocknumber");
            cMaster.columnrename("Shift Preference", "shiftpref");
            cMaster.columnrename("EMPLOYEE NAME", "empname");
            cMaster.columnrename("DATE", "senioritydate");
            cMaster.columnrename("Prebuilt Hours", "prebuilthours");
            cMaster.columnrename("Weekend OT hours", "weekendothours");
            cMaster.columnrename("Total hours", "totalhours");

            List<string> jobs = cMaster["Rated Job"].Distinct().ToList();
            cMaster.addfeature("jobid");
            for (int i = 0; i < cMaster.Rows; i++)
            {
                for (int j = 0; j < jobs.Count(); j++)
                {
                    if (cMaster[i, 4].ToString() == jobs[j])
                    {
                        cMaster[i, 10] = j + 1;
                    }
                }
            }

            Context EmployeeTableToDB = cMaster[new string[] {
                "clocknumber",
                "senioritynumber",
                "shiftpref",
                "empname",
                "senioritydate",
                "prebuilthours",
                "weekendothours",
                "totalhours",
                "jobid" }];

            EmployeeTableToDB.to_csv(DataFolder + "EmployeeTableToDB.csv");
            Context.Import_csv(conn, DataFolder + "EmployeeTableToDB.csv", tablename: "employee", showlog: true);
            util.print("Imported: " + "cMaster");
        }

        public static void CreateAndCleanEmployeeTableOld()
        {
            Context cWeekDayData = Context.from_excel(WeekDaySchedule, 1, 1, 1, 6, 10, true);
            Context cSenList = Context.from_excel(SeniorityFile, 1, 1, 1, 1, 4, true);
            conn.ConnectionString = BSConnectionString;

            //Clean initial data
            cWeekDayData.columnrename("Fburba", "SEN");
            cWeekDayData = cWeekDayData.clean_dropna("employee name");
            cWeekDayData[cWeekDayData["SHIFT PREFERENCE"] == "", "SHIFT PREFERENCE"] = 0;
            cWeekDayData.Name = "cWeekDayData";
            cSenList.Name = "cSenList";

            Context EmployeeTable = Context.zip("SEN", cWeekDayData, cSenList);
            EmployeeTable.Name = "EmployeeTable";
            List<string> jobs = EmployeeTable["RATED JOB_cWeekDayData"].Distinct().ToList();
            EmployeeTable.addfeature("jobid");
            for (int i = 0; i < EmployeeTable.Rows; i++)
            {
                for (int j = 0; j < jobs.Count(); j++)
                {
                    if (EmployeeTable[i, 4].ToString() == jobs[j])
                    {
                        EmployeeTable[i, 10] = j + 1;
                    }
                }
            }

            List<string> depts = EmployeeTable["DEPT_cWeekDayData"].Distinct().ToList();
            EmployeeTable.addfeature("deptid");
            for (int i = 0; i < EmployeeTable.Rows; i++)
            {
                for (int j = 0; j < depts.Count(); j++)
                {
                    if (EmployeeTable[i, 5].ToString() == depts[j])
                    {
                        EmployeeTable[i, 11] = j + 1;
                    }
                }

            }

            EmployeeTable.columnrename("sen", "empid");
            EmployeeTable.columnrename("SHIFT PREFERENCE_cWeekDayData", "shiftpref");
            EmployeeTable.columnrename("EMPLOYEE NAME_cWeekDayData", "empname");
            EmployeeTable.columnrename("RATED JOB_cWeekDayData", "job");

            Context EmployeeTableToDB = EmployeeTable[new string[] { "empid", "shiftpref", "empname", "job", "jobid" }];
            //EmployeeTableToDB["empname"] = EmployeeTableToDB[new string[] { "empname" }].clean_regex("empname", ",", "; ")["empname"];
            EmployeeTableToDB = EmployeeTableToDB.clean_dropna(new string[] { "empname", "job" });
            EmployeeTableToDB.to_csv(DataFolder + "EmployeeTableToDB.csv");
            Context.Import_csv(conn, DataFolder + "EmployeeTableToDB.csv", tablename: "employee", showlog: true);
            util.print("Imported: " + EmployeeTable.Name);
        }
    }
}