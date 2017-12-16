using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Zamestnanec
    {
        private string _meno;
        public string Meno
        {
            get => _meno;
            set
            {
                if (value != _meno)
                {
                    _meno = value;
                    OnPropertyChanged("Meno");
                }
            }
        }


        private string _priezvisko;
        public string Priezvisko
        {
            get => _priezvisko;
            set
            {
                if (value != _priezvisko)
                {
                    _priezvisko = value;
                    OnPropertyChanged("Priezvisko");
                }
            }
        }

        private BitmapImage _fotka;
        public BitmapImage Fotka
        {
            get => _fotka;
            set
            {
                if (value != _fotka)
                {
                    _fotka = value;
                    OnPropertyChanged("Fotka");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
