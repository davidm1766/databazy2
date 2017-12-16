﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Types;

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
                                  int idAktualPoloha)
        {


            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_insert_vozen",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };

            cmd.Parameters.Add("pa_id_vlastnik", OracleDbType.Int32, ParameterDirection.Input).Value = idVlastnik;
            cmd.Parameters.Add("pa_id_typ_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idTypVozna;
            cmd.Parameters.Add("pa_id_aktual_poloha", OracleDbType.Int32, ParameterDirection.Input).Value = idAktualPoloha;
            cmd.Parameters.Add("pa_id", OracleDbType.Int32, ParameterDirection.Output);

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

        public void ZaradVOzenDoVlaku(int idVozna, int idVlaku)
        {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_zarad_vozen_do_vlaku",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };

            cmd.Parameters.Add("pa_id_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idVozna;
            cmd.Parameters.Add("pa_id_vlaku", OracleDbType.Int32, ParameterDirection.Input).Value = idVlaku;

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
        
        /// <summary>
        ///     Vyradenie vozna z prevadzky
        /// </summary>
        /// <param name="idVozna"></param>
        public void VyradVozen(int idVozna)
        {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_insert_vozen",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };

            cmd.Parameters.Add("pa_id_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idVozna;

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

	    public void DajPolohuVozna(int idVozna, out double zemDlzka, out double zemSirka)
	    {
			var cmd = new OracleCommand
			{
				Connection = _connection,
				CommandText = "daj_polohu_vozna",
				CommandType = CommandType.StoredProcedure,
				BindByName = true
			};

		    cmd.Parameters.Add("pa_id_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idVozna;
		    cmd.Parameters.Add("pa_zem_sirka", OracleDbType.Decimal, ParameterDirection.Output);
		    cmd.Parameters.Add("pa_zem_dlzka", OracleDbType.Decimal, ParameterDirection.Output);

		    try
		    {
				_connection.Open();   							    
			    cmd.ExecuteNonQuery();
			    zemDlzka = (double) (OracleDecimal) cmd.Parameters["pa_zem_dlzka"].Value;
			    zemSirka = (double) (OracleDecimal) cmd.Parameters["pa_zem_sirka"].Value;				
		    }
		    finally
		    {
			    _connection.Close();
		    }
		}		    
        
    }
}