using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Barton1792DB.DAO
{
    public class Writers
    {
        private string BSConnectionString = CreateDB.BSConnectionString;

        private string InsertSql { get { return ""; } }
        private string UpdateSql { get { return ""; } }
        private string DeleteSql { get { return ""; } }

        /// <summary>
        /// Insert, Delete, Update
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

    }
}
