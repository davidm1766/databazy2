
using System;
using System.Data;
using Core.Models;
using DataLayer;

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

        public void PresunVozen(int idVozna, int idKolajZ, int idKolajNa)
        {
            _volanieFunkcii.PresunVozen(idVozna, idKolajZ, idKolajNa);
        }

        public void PostavVozenNaKolaj(int idVozna, int idKolajNa)
        {
            _volanieFunkcii.PridajVozenNaKolaj(idVozna, idKolajNa);
        }

        public Zamestnanec NajdiZamestnanca(int idZamestnanca)
        {
            _volanieFunkcii.NajdiZamestnanca(idZamestnanca);
            throw new Exception("neimplementovane");
        }
    }
}
