
using System;
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
            _volanieFunkcii.VlozNovyVozen(novyVozen.VlastnikVozna.IdVlastnika,
                                          novyVozen.TypVozna.IdTypuVozna,
                                          novyVozen.AktualnaPoloha.IdPoloha
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

        public void PresunVozen(int idVozna, int idKolajZ, int idKolajNa)
        {
            _volanieFunkcii.PresunVozen(idVozna, idKolajZ, idKolajNa);
        }
    }
}
