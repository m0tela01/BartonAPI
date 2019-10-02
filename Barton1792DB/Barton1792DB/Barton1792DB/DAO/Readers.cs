using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using Barton1792DB.DBO;
using MySql.Data.MySqlClient;

namespace Barton1792DB.DAO
{
    public class Readers
    {
        private string BSConnectionString = CreateDB.BSConnectionString;
        private string GetEmployeesSql { get { return "SELECT * FROM  `sazerac`.`employee`"; } }
        private string DataFolder { get { return Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\DataFiles\\"; } }

        public List<Employee> GetEmployees()
        {
            List<Employee> Employees = new List<Employee>();
            using (MySqlConnection conn = new MySqlConnection(BSConnectionString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(GetEmployeesSql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        Employees.Add(new Employee()
                        {
                            EmployeeId = int.Parse(rdr["empid"].ToString()),
                            ShiftPreference = int.Parse(rdr["shiftpref"].ToString()),
                            EmployeeName = rdr["empname"].ToString(),
                            Job = rdr["job"].ToString(),
                            JobId = int.Parse(rdr["jobid"].ToString())

                        });
                    }
                    rdr.Close();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return Employees;
        }

    }
}
