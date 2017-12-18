
using System;
using System.Data;
using Core.Models;
using DataLayer;
using System.Windows.Media.Imaging;
using System.IO;
using System.Collections.ObjectModel;

namespace Core
{
    public class CoreApp
    {
        /// <summary>
        ///     Singleton
        /// </summary>
        private static CoreApp _instance;
        public static CoreApp Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("Najprv musis zavolat staticku metodu Register()");
                }
                return _instance;
            }
        }

        private VolanieFunkcii _volanieFunkcii;
        private string ORACLE_PRIECINOK = @"C:\TEMP\databazy\";
        private CoreApp(string meno, string heslo) {
            _volanieFunkcii = new VolanieFunkcii(meno, heslo);
        }


        public void PridajNovyVozen(Vozen novyVozen) {
            int poloha =_volanieFunkcii.DajIdPolohy(novyVozen.AktualnaPoloha.AktualnaPolohaLongitude,novyVozen.AktualnaPoloha.AktualnaPolohaLatitude);
            _volanieFunkcii.VlozNovyVozen(novyVozen.VlastnikVozna.IdVlastnika,
                                          novyVozen.TypVozna.IdTypuVozna,
                                          poloha
                                          );
        }

        public static void Register(string meno, string heslo)
        {
            _instance = new CoreApp(meno,heslo);
        }

        public void VyradVozen(int idVozna)
        {
            _volanieFunkcii.VyradVozen(idVozna);
        }

        public void ZaradVozenDoVlaku(int idVozna, int idVlaku)
        {
            _volanieFunkcii.ZaradVozenDoVlaku(idVozna, idVlaku);
        }

        public void VyradVozenZVlaku(int idVozna)
        {
            _volanieFunkcii.VyradVozenZVlaku(idVozna);
        }

		// vypisy
	    public DataSet VypisAktualnuPolohuVoznov(string nazovVlastnika, string nazovTypu)
	    {
		    return _volanieFunkcii.DajPolohuVoznov(nazovVlastnika, nazovTypu);
	    }

	    public DataSet VypisAktualnuPolohuVozna(int idVozna)
	    {
		    return _volanieFunkcii.DajPolohuVozna(idVozna);
	    }

	    public DataSet VypisVozneVStanici(string nazovStanice, DateTime casOd, DateTime casDo, string nazovVlastnika, string nazovTypuVozna)
	    {
		    return _volanieFunkcii.DajVozneVStanici(nazovStanice, casOd, casDo, nazovVlastnika, nazovTypuVozna);
	    }

	    public DataSet VypisVozneVoVlaku(int idVlaku, string nazovTypuVlaku, string nazovTypuVozna, string nazovVlastnika, DateTime casOd, DateTime casDo)
	    {
		    return _volanieFunkcii.DajVozneVoVlaku(idVlaku, nazovTypuVlaku, nazovTypuVozna, nazovVlastnika, casOd, casDo);
	    }
		// vypisy koniec

		public void PresunVozen(int idVozna, int idKolajZ, int idKolajNa)
        {
            _volanieFunkcii.PresunVozen(idVozna, idKolajZ, idKolajNa);
        }

        public ObservableCollection<Vlastnik> DajVsetkychVlastnikov()
        {
            ObservableCollection<Vlastnik> ret = new ObservableCollection<Vlastnik>();
            foreach (var vlastnik in _volanieFunkcii.DajVsetkychVlastnikov()) {
                ret.Add(new Vlastnik() { IdVlastnika = vlastnik.Item1, NazovVlastnika = vlastnik.Item2 });
            }
            return ret;
        }

        public ObservableCollection<TypVozna> DajVsetkyTypyVoznov()
        {

            ObservableCollection<TypVozna> ret = new ObservableCollection<TypVozna>();
            foreach (var typVozna in _volanieFunkcii.DajVsetkyTypyVoznov())
            {
                ret.Add(new TypVozna() { IdTypuVozna = typVozna.Item1, NazovTypuVozna = typVozna.Item2 });
            }
            return ret;
        }

	    public ObservableCollection<Stanica> DajVsetkyStanice()
	    {
		    ObservableCollection<Stanica> ret = new ObservableCollection<Stanica>();
		    foreach (var stanica in _volanieFunkcii.DajVsetkyStanice())
		    {
				ret.Add(new Stanica() { IdStanice = stanica.Item1, Nazov = stanica.Item2 });
			}
		    return ret;
	    }

	    public ObservableCollection<TypVlaku> DajVsetkyTypyVlakov()
	    {
			ObservableCollection<TypVlaku> ret = new ObservableCollection<TypVlaku>();
		    foreach (var typVlaku in _volanieFunkcii.DajVsetkyTypyVlakov())
		    {
			    ret.Add(new TypVlaku() { IdTypuVlaku = typVlaku.Item1, NazovTypuVlaku = typVlaku.Item2 });
		    }
		    return ret;
		}

		public void PostavVozenNaKolaj(int idVozna, int idKolajNa)
        {
            _volanieFunkcii.PridajVozenNaKolaj(idVozna, idKolajNa);
        }

        public Zamestnanec NajdiZamestnanca(int idZamestnanca)
        {
            var tupl =  _volanieFunkcii.NajdiZamestnanca(idZamestnanca);
            if (tupl == null) {
                return null;
            }

            Zamestnanec zam = new Zamestnanec();
            zam.Meno = tupl.Item1;
            zam.Priezvisko = tupl.Item2;
            zam.Fotka = ConvertBytesToImage(tupl.Item3);
            return zam;
        }

       

        private BitmapImage ConvertBytesToImage(byte[] bytes) {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }

        public void PridajNovehoZamestnanca(Zamestnanec zamestnanecNew)
        {
            if (String.IsNullOrWhiteSpace(zamestnanecNew.CestaKuFotke) ||
               String.IsNullOrWhiteSpace(zamestnanecNew.Meno) ||
               String.IsNullOrWhiteSpace(zamestnanecNew.Priezvisko))
            {
                throw new ArgumentException("Nie su vyplnene všetky potrebné údaje.");
            }
            //musim skopirovat image do adresare kde ma oracle pristup
            var nazovSuboru = zamestnanecNew.CestaKuFotke.Split('\\')[zamestnanecNew.CestaKuFotke.Split('\\').Length - 1];

            string sourceFilePath = zamestnanecNew.CestaKuFotke;
            string targetFilePath = ORACLE_PRIECINOK + nazovSuboru;

            //ak nie je orcl dir spraveny tak ho vytvorim
            if (!Directory.Exists(ORACLE_PRIECINOK))
            {
                Directory.CreateDirectory(ORACLE_PRIECINOK);
            }

            File.Copy(sourceFilePath, targetFilePath, true);

            _volanieFunkcii.VlozZamestnanca(zamestnanecNew.Meno, zamestnanecNew.Priezvisko, nazovSuboru);
        }

      
        public Vozen NajdiNajblizsiVolnyVozen(Vlastnik vlastnik, TypVozna typvozna, double latit, double longi)
        {
            Vozen v = new Vozen();

            var ret = _volanieFunkcii.NajdiNajblizsiVolnyVozen(vlastnik.IdVlastnika, typvozna.IdTypuVozna, latit, longi);

            v.IDVozna = ret.Item1;
            v.TypVozna = typvozna;
            v.VlastnikVozna = vlastnik;
            v.AktualnaPoloha = new Poloha() {AktualnaPolohaLatitude=ret.Item2,AktualnaPolohaLongitude=ret.Item3 };
            return v;
        }
    }
}
