using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class VolanieFunkcii
    {
        private string _connectionString;
        private OracleConnection _connection;


        public VolanieFunkcii(string meno, string heslo) {
            _connectionString = $"Data Source=localhost;User Id={meno};Password={heslo};";
            _connection = new OracleConnection(_connectionString);
            
            //vyskusam ci otvorim pripojenie
            _connection.Open();
            _connection.Close();
        }

        /// <summary>
        ///     pridanie vozňa do systému a určenie jeho polohy
        /// </summary>
        public void VlozNovyVozen(int idVlastnik,
                                  int idTypVozna,
                                  int idAktualVlak,
                                  int idAktualPoloha,
                                  int idAktualKolaj,
                                  DateTime aktualVlakOd,
                                  DateTime aktualPolohaOd,
                                  DateTime aktualKolajOd) {

           
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "insert_vozen",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };
            
            cmd.Parameters.Add("pa_id_vlastnik", OracleDbType.Int32, ParameterDirection.Input).Value = idVlastnik;
            cmd.Parameters.Add("pa_id_typ_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idTypVozna;
            cmd.Parameters.Add("pa_id_aktual_vlak", OracleDbType.Int32, ParameterDirection.Input).Value = idAktualVlak;
            cmd.Parameters.Add("pa_id_aktual_poloha", OracleDbType.Int32, ParameterDirection.Input).Value = idAktualPoloha;
            cmd.Parameters.Add("pa_id_aktual_kolaj", OracleDbType.Int32, ParameterDirection.Input).Value = idAktualKolaj;

            cmd.Parameters.Add("pa_aktual_vlak_od", OracleDbType.Date, ParameterDirection.Input).Value = aktualVlakOd;
            cmd.Parameters.Add("pa_aktual_poloha_od", OracleDbType.Date, ParameterDirection.Input).Value = aktualPolohaOd;
            cmd.Parameters.Add("pa_aktual_kolaj_od", OracleDbType.Date, ParameterDirection.Input).Value = aktualKolajOd;


            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }

            
        }


    }
}
