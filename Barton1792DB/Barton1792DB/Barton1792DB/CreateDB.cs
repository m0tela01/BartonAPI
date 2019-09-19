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
        private static MySqlConnection conn;
        private static string BSConnectionString { get { return "server=107.180.51.29;uid=sazerac_user;pwd=sazerac2019;database=sazerac"; } }

        private static string DataFolder { get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\DataFiles\\"; } }
        private static string SeniorityFile { get { return DataFolder + "UL Master Seniority List.xlsx"; } }
        private static string WeekDaySchedule { get { return DataFolder + "6 DAY SCHEDULE WEEK OF 9-2-19.xlsm"; } }
        #endregion Props


        public static void ConnectToDB()
        {
            try
            {
                conn = new MySqlConnection();
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
            Context cWeekDayData = Context.from_excel(WeekDaySchedule);
            Context cExcelData = Context.from_excel(SeniorityFile);
            Context cEmp = cWeekDayData[new string[] { "Fburba", "SHIFT PREFERENCE", "EMPLOYEE NAME" }];
            cEmp.columnrename("Fburba", "SEN");

            //cEmp = cEmp[cEmp["ShiftPreference"].isnull(), cEmp["ShiftPreference"] = 0];
            List<int> rowsToRemove = new List<int>();
            for (int i = 0; i < cEmp.Rows; i++)
            {
                List<object> asdf = new List<object>();
                for (int j = 0; j < 3; j++)
                {
                    asdf.Add(cEmp[i, j]);
                }
                if (asdf[0] == "" && asdf[1] == "" && asdf[2] == "")
                {
                    rowsToRemove.Add(i);
                }
            }
            cEmp.drop_row(rowsToRemove.ToArray());
            cEmp[cEmp["SHIFT PREFERENCE"] == "", "SHIFT PREFERENCE"] = 0;

            Context EmployeeTable = Context.zip("SEN", cEmp, cExcelData);
            EmployeeTable.to_csv(DataFolder + "EmpTable1.csv");

            Context EmpTable = Context.from_csv(DataFolder + "EmpTable1.csv");
            EmpTable = EmpTable[ColumnCondition.Not, "SEN_", "UnNamesfeature_4_", "DEPT"];
            EmpTable.columnrename("SEN", "empid");
            EmpTable.columnrename("SHIFT PREFERENCE_", "shiftpref");
            EmpTable.columnrename("EMPLOYEE NAME_", "empname");
            EmpTable.columnrename("RATED JOB_","job");

            List<string> jobs =  EmpTable["job"].Distinct().ToList();
            EmpTable.addfeature("jobid");
            for (int i = 0; i < EmpTable.Rows; i++)
            {
                for (int j = 0; j < jobs.Count(); j++)
                {
                    if(EmpTable[i,3].ToString() == jobs[j])
                    {
                        EmpTable[i, 4] = j + 1;
                    }
                }
            }


            rowsToRemove = new List<int>();
            for (int i = 0; i < EmpTable.Rows; i++)
            {
                List<object> asdf = new List<object>();
                for (int j = 0; j < 5; j++)
                {
                    asdf.Add(EmpTable[i, j]);
                }
                if (asdf[2] == "" && asdf[3] == "" && asdf[4] == "")
                {
                    rowsToRemove.Add(i);
                }
            }
            EmpTable.drop_row(rowsToRemove.ToArray());

            EmpTable.to_csv(DataFolder + "EmpTable2.csv");

            util.print(EmpTable);
            Context.from_sql_query(conn, "");
        }
        public static void CallProcedure(string Procedure)
        {

        }
        public static void CallProcedure(string Procedure, Context cTable)
        {
            CallProcedure(Procedure);
        }
    }
}
