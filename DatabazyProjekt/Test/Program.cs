using DataLayer;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 c = new Class1();
            c.TestFunction();

            Console.ReadKey();
        }
    }
    public class Class1
    {
        private string _connectionString = "Data Source=localhost;User Id=meno;Password=*******;";
        private OracleConnection conn;


        public Class1()
        {
            conn = new OracleConnection(_connectionString);
        }

        public void TestFunction()
        {
            string s;

            var cmd = new OracleCommand()
            {
                Connection = conn,
                CommandText = "test_func1",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };

            cmd.Parameters.Add("ddd", OracleDbType.Int32, ParameterDirection.Input).Value = 123;
            cmd.Parameters.Add("dd1", OracleDbType.Int32, ParameterDirection.Input).Value = 1;
            cmd.Parameters.Add("parameter1", OracleDbType.Int32).Direction = ParameterDirection.ReturnValue;
           
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                s = cmd.Parameters["parameter1"].Value.ToString();
                s += cmd.Parameters["dd1"].Value.ToString();
            }
            finally
            {
                conn.Close();
            }

            Console.WriteLine(s);
        }




        public void TryConnect()
        {

            conn.Open();
            Console.WriteLine("Connected to Oracle" + conn.ServerVersion);
            // Close and Dispose OracleConnection object  
            conn.Close();
            conn.Dispose();
            // Console.WriteLine("Connected to Oracle" + conn.ServerVersion);

        }
    }
}
