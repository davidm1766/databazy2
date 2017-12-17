using Oracle.ManagedDataAccess.Client;
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
        private int _varchar2Size = 100;

        public VolanieFunkcii(string meno, string heslo) {
            _connectionString = $"Data Source=localhost;User Id={meno};Password={heslo};";
            _connection = new OracleConnection(_connectionString);
            
            //vyskusam ci otvorim pripojenie
            _connection.Open();
            _connection.Close();
        }


        public int DajIdPolohy(double longitude, double latitude)
        {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_daj_id_polohu",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };

            cmd.Parameters.Add("pa_id_longitude", OracleDbType.Decimal, ParameterDirection.Input).Value = (decimal)longitude;
            cmd.Parameters.Add("pa_id_latitude", OracleDbType.Decimal, ParameterDirection.Input).Value = (decimal)latitude;
            cmd.Parameters.Add("pa_id", OracleDbType.Int32, ParameterDirection.Output);

            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();
                return (int)(OracleDecimal)cmd.Parameters["pa_id"].Value;
            }
            finally
            {
                _connection.Close();
            }

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

        public List<Tuple<int, string>> DajVsetkyTypyVoznov()
        {
            List<Tuple<int, string>> ret = new List<Tuple<int, string>>();
            OracleDataReader oraReader = null;
            try
            {
                _connection.Open();

                OracleCommand oraCommand = new OracleCommand("SELECT id_typu,typ_nazov FROM typ_vozna", _connection);

                oraReader = oraCommand.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        var idcko = oraReader.GetInt32(0);
                        var nazov = oraReader.GetString(1);
                        ret.Add(new Tuple<int, string>(idcko, nazov));
                    }
                }

            }
            finally
            {
                oraReader.Close();
                _connection.Close();
            }
            return ret;
        }

        public List<Tuple<int,string>> DajVsetkychVlastnikov()
        {
            List<Tuple<int, string>> ret  = new List<Tuple<int, string>>();
            OracleDataReader oraReader = null;
            try
            {
                _connection.Open();

                OracleCommand oraCommand = new OracleCommand("SELECT id_vlastnik,nazov FROM vlastnik",_connection);
                
                oraReader = oraCommand.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        var idcko = oraReader.GetInt32(0);
                        var nazov = oraReader.GetString(1);
                        ret.Add(new Tuple<int, string>(idcko, nazov));
                    }
                }
                
            }
            finally
            {
                oraReader.Close();
                _connection.Close();
            }
            return ret;
        }

	    public List<Tuple<int, string>> DajVsetkyStanice()
	    {
			List<Tuple<int, string>> ret = new List<Tuple<int, string>>();
		    OracleDataReader oraReader = null;
		    try
		    {
			    _connection.Open();

			    OracleCommand oraCommand = new OracleCommand("SELECT id_stanica,nazov FROM stanica", _connection);

			    oraReader = oraCommand.ExecuteReader();

			    if (oraReader.HasRows)
			    {
				    while (oraReader.Read())
				    {
					    var idcko = oraReader.GetInt32(0);
					    var nazov = oraReader.GetString(1);
					    ret.Add(new Tuple<int, string>(idcko, nazov));
				    }
			    }

		    }
		    finally
		    {
			    oraReader?.Close();
			    _connection.Close();
		    }
		    return ret;
		}

	    public List<Tuple<int, string>> DajVsetkyTypyVlakov()
	    {
			List<Tuple<int, string>> ret = new List<Tuple<int, string>>();
		    OracleDataReader oraReader = null;
		    try
		    {
			    _connection.Open();

			    OracleCommand oraCommand = new OracleCommand("SELECT id_druhu,nazov_druhu FROM druh_vlaku", _connection);

			    oraReader = oraCommand.ExecuteReader();

			    if (oraReader.HasRows)
			    {
				    while (oraReader.Read())
				    {
					    var idcko = oraReader.GetInt32(0);
					    var nazov = oraReader.GetString(1);
					    ret.Add(new Tuple<int, string>(idcko, nazov));
				    }
			    }

		    }
		    finally
		    {
			    oraReader?.Close();
			    _connection.Close();
		    }
		    return ret;
		}

		public void VlozZamestnanca(string meno,
                                    string priezvisko,
                                    string cestaKuFotke)
        {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_uloz_pracovnika",
                CommandType = CommandType.StoredProcedure,
                BindByName = true
            };

            cmd.Parameters.Add("pa_meno", OracleDbType.Varchar2, ParameterDirection.Input).Value = meno;
            cmd.Parameters.Add("pa_priezvisko", OracleDbType.Varchar2, ParameterDirection.Input).Value = priezvisko;
            cmd.Parameters.Add("pa_src_file_path", OracleDbType.Varchar2, ParameterDirection.Input).Value = cestaKuFotke;

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
        ///     ID
        ///     lati
        ///     longi
        /// </summary>
        /// <param name="idVlastnika"></param>
        /// <param name="idTypuVozna"></param>
        /// <param name="latit"></param>
        /// <param name="longi"></param>
        /// <returns></returns>
        public Tuple<int, double, double> NajdiNajblizsiVolnyVozen(int idVlastnika, int idTypuVozna, double latit, double longi)
        {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_najdi_najblizsi_vozen",
                CommandType = CommandType.StoredProcedure,
                BindByName = true
            };

            cmd.Parameters.Add("pa_id_vlastnika", OracleDbType.Int32, ParameterDirection.Input).Value = idVlastnika;
            cmd.Parameters.Add("pa_id_typu_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idTypuVozna;
            cmd.Parameters.Add("pa_latitude", OracleDbType.Decimal, ParameterDirection.Input).Value = (decimal)latit;
            cmd.Parameters.Add("pa_longitude", OracleDbType.Decimal, ParameterDirection.Input).Value = (decimal)longi;

            cmd.Parameters.Add("out_id", OracleDbType.Int32, ParameterDirection.Output);
            cmd.Parameters.Add("out_longitude", OracleDbType.Decimal, ParameterDirection.Output);
            cmd.Parameters.Add("out_latitude", OracleDbType.Decimal, ParameterDirection.Output);
            
            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();
                var id = (int)(OracleDecimal)cmd.Parameters["out_id"].Value;
                var la = (decimal)(OracleDecimal)cmd.Parameters["out_latitude"].Value;
                var lo = (decimal)(OracleDecimal)cmd.Parameters["out_longitude"].Value;
                return new Tuple<int, double, double>(id, (double)la, (double)lo);
            }
            finally
            {
                _connection.Close();
            }
        }

        public Tuple<string,string,byte[]> NajdiZamestnanca(int idZamestnanca)
        {
            Tuple<string, string, byte[]> ret = null;
            OracleDataReader oraReader = null;
            try
            {
                _connection.Open();

                OracleCommand oraCommand = new OracleCommand("SELECT meno,priezvisko,fotka FROM pracovnik WHERE id_pracovnika = :pracId", _connection);
                oraCommand.Parameters.Add(new OracleParameter("pracId", idZamestnanca));

                
                oraReader = oraCommand.ExecuteReader();

                if (oraReader.HasRows)
                {
                    while (oraReader.Read())
                    {
                        var meno = oraReader.GetString(0);
                        var prizvisko = oraReader.GetString(1);
                        byte[] fotka = (oraReader.GetOracleBlob(2)).Value;
                        ret = new Tuple<string, string, byte[]>(meno,prizvisko,fotka);
                        return ret;
                    }
                }
                return ret;
            }
            finally
            {
                oraReader.Close();
                _connection.Close();                
            }

        }



        public void PresunVozen(int idVozna, int idKolajZ, int idKolajNa)
        {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_presun_vozen_z_kolaje",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };

            cmd.Parameters.Add("pa_id_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idVozna;
            cmd.Parameters.Add("pa_id_kolaj_z", OracleDbType.Int32, ParameterDirection.Input).Value = idKolajZ;
            cmd.Parameters.Add("pa_id_kolaj_na", OracleDbType.Int32, ParameterDirection.Input).Value = idKolajNa;
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

        public void VyradVozenZVlaku(int idVozna)
        {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_vyrad_vozen_z_vlaku",
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

        public void PridajVozenNaKolaj(int idVozna, int idKolajNa) {
            var cmd = new OracleCommand
            {
                Connection = _connection,
                CommandText = "gui_pridat_vozen_na_kolaj",
                CommandType = CommandType.StoredProcedure,
                BindByName = true

            };

            cmd.Parameters.Add("pa_id_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idVozna;
            cmd.Parameters.Add("pa_id_kolaj_na", OracleDbType.Int32, ParameterDirection.Input).Value = idKolajNa;
            
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

        public void ZaradVozenDoVlaku(int idVozna, int idVlaku)
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
            cmd.Parameters.Add("pa_error", OracleDbType.Varchar2, ParameterDirection.Output).Size = _varchar2Size;

            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();

                var error = cmd.Parameters["pa_error"].Value.ToString();
                if (!String.IsNullOrWhiteSpace(error)) {
                    throw new ArgumentException(error);
                }
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
                CommandText = "gui_vyrad_vozen_z_prevadzky",
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

		// vypisy

	    public DataSet DajInformacieOVoznoch(int idVozna, string nazovVlastnika, string nazovTypuVozna, string nazovStanice, bool? vyradene)
	    {
			var cmd = new OracleCommand
			{
				Connection = _connection,
				CommandText = "vypis_informacie_o_voznoch",
				CommandType = CommandType.StoredProcedure,
				BindByName = true
			};

		    if (idVozna > 0)
		    {
			    cmd.Parameters.Add("pa_id_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idVozna;
			}

		    if (vyradene != null)
		    {
			    cmd.Parameters.Add("pa_odstaveny", OracleDbType.Char, ParameterDirection.Input).Value = (vyradene == true ? 'y' : 'n');
			}
		    
			cmd.Parameters.Add("pa_nazov_vlastnika", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovVlastnika;
		    cmd.Parameters.Add("pa_nazov_typu_vozna", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovTypuVozna;
		    cmd.Parameters.Add("pa_nazov_stanice", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovStanice;
			cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;

		    return DajDataSet(cmd);
	    }

	    public DataSet DajInformacieOVlakoch(int idVlaku, string typVlaku)
	    {
		    var cmd = new OracleCommand
		    {
			    Connection = _connection,
			    CommandText = "vypis_informacie_o_vlakoch",
			    CommandType = CommandType.StoredProcedure,
			    BindByName = true
		    };

		    if (idVlaku > 0)
		    {
			    cmd.Parameters.Add("pa_id_vlaku", OracleDbType.Int32, ParameterDirection.Input).Value = idVlaku;
		    }		    

		    cmd.Parameters.Add("pa_nazov_typu", OracleDbType.Varchar2, ParameterDirection.Input).Value = typVlaku;		    
		    cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;

		    return DajDataSet(cmd);
	    }

		public DataSet DajVozneVStanici(string nazovStanice, DateTime casOd, DateTime casDo, string nazovVlastnika, string nazovTypuVozna)
		{
			var cmd = new OracleCommand()
			{
				Connection = _connection,
				CommandText = "vypis_vozne_v_stanici",
				CommandType = CommandType.StoredProcedure,
				BindByName = true

			};
			
			
			cmd.Parameters.Add("pa_nazov_stanice", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovStanice;
			cmd.Parameters.Add("pa_nazov_vlastnika", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovVlastnika;
			cmd.Parameters.Add("pa_nazov_typu_vozna", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovTypuVozna;

			if (casOd != DateTime.MinValue)
			{
				cmd.Parameters.Add("pa_cas_od", OracleDbType.Date, ParameterDirection.Input).Value = casOd;
			}

			if (casDo != DateTime.MinValue)
			{
				cmd.Parameters.Add("pa_cas_do", OracleDbType.Date, ParameterDirection.Input).Value = casDo;
			}
					
			cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;

			return DajDataSet(cmd);
		}

	    public DataSet DajVozneVoVlaku(int idVlaku, string nazovTypuVlaku, string nazovTypuVozna, string nazovVlastnika, DateTime casOd, DateTime casDo)
	    {
			var cmd = new OracleCommand()
		    {
			    Connection = _connection,
			    CommandText = "vypis_vozne_vo_vlaku",
			    CommandType = CommandType.StoredProcedure,
			    BindByName = true

		    };

		    if (idVlaku > 0)
		    {
			    cmd.Parameters.Add("pa_id_vlaku", OracleDbType.Int32, ParameterDirection.Input).Value = idVlaku;
			}
		    
		    cmd.Parameters.Add("pa_nazov_typu_vlaku", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovTypuVlaku;
		    cmd.Parameters.Add("pa_nazov_typu_vozna", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovTypuVozna;
			cmd.Parameters.Add("pa_nazov_vlastnika", OracleDbType.Varchar2, ParameterDirection.Input).Value = nazovVlastnika;
		    

			if (casOd != DateTime.MinValue)
		    {
			    cmd.Parameters.Add("pa_cas_od", OracleDbType.Date, ParameterDirection.Input).Value = casOd;
		    }

		    if (casDo != DateTime.MinValue)
		    {
			    cmd.Parameters.Add("pa_cas_do", OracleDbType.Date, ParameterDirection.Input).Value = casDo;
		    }

		    cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;

		    return DajDataSet(cmd);
		}

	    public DataSet DajHistoriuVyskytuVozna(int idVozna)
	    {
		    var cmd = new OracleCommand()
		    {
			    Connection = _connection,
			    CommandText = "vypis_historiu_vyskytu_vozna",
			    CommandType = CommandType.StoredProcedure,
			    BindByName = true

		    };
		    
		    cmd.Parameters.Add("pa_id_vozna", OracleDbType.Int32, ParameterDirection.Input).Value = idVozna;		   		    
		    cmd.Parameters.Add("result", OracleDbType.RefCursor).Direction = ParameterDirection.ReturnValue;

		    return DajDataSet(cmd);
	    }

		private DataSet DajDataSet(OracleCommand cmd)
	    {
			try
			{
				_connection.Open();
				var dataSet = new DataSet();
				var oracleDataAdapter = new OracleDataAdapter(cmd);
				oracleDataAdapter.Fill(dataSet);
				return dataSet;
			}
			finally
			{
				_connection.Close();
			}
		}

		// vypisy koniec

	}
}
