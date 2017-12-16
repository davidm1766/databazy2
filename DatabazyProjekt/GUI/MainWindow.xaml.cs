using Core;
using Core.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// <summary>
        ///     Aktualne pridavany vozen
        /// </summary>
        public Vozen Vozen { get; set; }

        /// <summary>
        ///     Vsetci vlastnici z tabulky vlastnik
        /// </summary>
        public ObservableCollection<Vlastnik> Vlastnici { get; set; }

        /// <summary>
        ///     Vsetky typu voznov z tabulky typ_vozna
        /// </summary>
        public ObservableCollection<TypVozna> TypyVoznov { get; set; }

        /// <summary>
        ///     Všetky mozne polohy
        /// </summary>
        public ObservableCollection<Poloha> Polohy { get; set; }

        public MainWindow(string meno, string heslo)
        {
            InitializeComponent();
            CoreApp.Register(meno,heslo);
            TypyVoznov = VytvorFakeoveTypyVozna();
            Vlastnici = VytvorFakeovychVlastnikov();
            Polohy = VytvorFakeovePolohy();
            Vozen = new Vozen();
            DataContext = this;
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //pridanie noveho vozna
            CoreApp.Instance.PridajNovyVozen(Vozen);

        }




        public ObservableCollection<TypVozna> VytvorFakeoveTypyVozna()
        {
            ObservableCollection<TypVozna> ret = new ObservableCollection<TypVozna>();
            ret.Add(new TypVozna() { IdTypuVozna= 1, NazovTypuVozna = "Starý vozeň"});
            ret.Add(new TypVozna() { IdTypuVozna = 2, NazovTypuVozna = "Prepravný" });
            ret.Add(new TypVozna() { IdTypuVozna = 3, NazovTypuVozna = "Na uhlie" });
            ret.Add(new TypVozna() { IdTypuVozna = 4, NazovTypuVozna = "Na mlieko" });
            return ret;
        }


        public ObservableCollection<Vlastnik> VytvorFakeovychVlastnikov()
        {
            ObservableCollection<Vlastnik> ret = new ObservableCollection<Vlastnik>();
            ret.Add(new Vlastnik() { IdVlastnika = 1, NazovVlastnika = "ZSSK" });
            ret.Add(new Vlastnik() { IdVlastnika = 2, NazovVlastnika = "Regio Jet" });
            ret.Add(new Vlastnik() { IdVlastnika = 3, NazovVlastnika = "LEO Express" });
            ret.Add(new Vlastnik() { IdVlastnika = 4, NazovVlastnika = "České drahy" });
            return ret;
        }


        public ObservableCollection<Poloha> VytvorFakeovePolohy()
        {
            ObservableCollection<Poloha> ret = new ObservableCollection<Poloha>();
            ret.Add(new Poloha() { IdPoloha = 1, AktualnaPolohaLatitude = 20.1, AktualnaPolohaLongitude = 20.2 });
            ret.Add(new Poloha() { IdPoloha = 2, AktualnaPolohaLatitude = 12.1, AktualnaPolohaLongitude = 12.2 });
            ret.Add(new Poloha() { IdPoloha = 3, AktualnaPolohaLatitude = 10.1, AktualnaPolohaLongitude = 10.2 });
            ret.Add(new Poloha() { IdPoloha = 4, AktualnaPolohaLatitude = 5.1, AktualnaPolohaLongitude = 5.2 });
            return ret;
        }
    }
}
