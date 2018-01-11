using System;
using System.Data;
using Npgsql;

namespace PostgreSQL
{
    class Program
    {
        private static string cnxDetails = "Server=localhost;Database=dbNameHere;User Id=usernameHere;Password=passwordHere;";
        
        static void Main(string[] args)
        {
            // nice to start a new sample connect to a DB which is gonna be PostgreSQL :( I don't want to do this since
            // I so bored and ehh but here we go.

            string SQL = @"select * from testDB";

            Console.WriteLine("Hello world: " + getAll(SQL).Rows.Count);

            Console.ReadKey();

        }

        static DataTable getAll(string _SQLCommand)
        {
            DataTable dTable = new DataTable();
            using (NpgsqlConnection cnx = new NpgsqlConnection(cnxDetails))
            {
                cnx.Open();
                
                NpgsqlDataAdapter dAdapter = new NpgsqlDataAdapter(_SQLCommand, cnx);
                dAdapter.Fill(dTable);

                if (dAdapter != null)
                    dAdapter.Dispose();
            }
            return dTable;
        }

        public int runSQLquery(string _SQLCommand)
        {
            int results = 0;

            using (NpgsqlConnection cnx = new NpgsqlConnection(cnxDetails))
            {
                cnx.Open();
                
                NpgsqlCommand cmd = new NpgsqlCommand(_SQLCommand, cnx);
                cmd.CommandType = CommandType.Text;

                results = cmd.ExecuteNonQuery();
                cmd.Dispose();
                cnx.Dispose();
            }
            return results;
        }
    }
}