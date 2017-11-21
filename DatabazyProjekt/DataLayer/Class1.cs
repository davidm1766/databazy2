using Oracle.ManagedDataAccess.Client;
//using Oracle.DataAccess.Client;
using System;
using System.Data;

namespace DataLayer
{
    public class Class1
    {
        private string _connectionString = "Data Source=localhost;User Id=david;Password=Mimada176;";
        private OracleConnection conn;


        public Class1() {
            conn = new OracleConnection(_connectionString);
        }

        public void TestFunction()
        {
            string s;

            var cmd = new OracleCommand
            {
                Connection = conn,
                CommandText = "test_func(:parameter1)",
                CommandType = CommandType.StoredProcedure,
                BindByName = true
                
            };
            OracleParameter param = new OracleParameter();
            param.DbType = DbType.Int32;
            //param.ParameterName = "parameter1";
            param.Direction = ParameterDirection.Output;
            param.Size = 25;
            cmd.Parameters.Add(param);
            cmd.InitialLONGFetchSize = 1000;
            //param = cmd.Parameters.Add("parameter1", OracleDbType.Int32, ParameterDirection.Output);  // can assign the direction within the parameter declaration


            //cmd.Parameters.Add("par1", OracleDbType.Int32).Direction = ParameterDirection.Output;
            //  cmd.Parameters.Add("return_value", OracleDbType.Int32, ParameterDirection.ReturnValue);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                s = cmd.Parameters["par1"].Value.ToString();
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
}}
