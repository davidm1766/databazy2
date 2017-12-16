using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Poloha
    {
        private int _idPoloha;
        public int IdPoloha
        {
            get => _idPoloha;
            set
            {
                if (value != _idPoloha)
                {
                    _idPoloha = value;
                    OnPropertyChanged("IdPoloha");
                }
            }
        }


        private double _aktualnaPolohaLongitude;
        public double AktualnaPolohaLongitude
        {
            get => _aktualnaPolohaLongitude;
            set
            {
                if (value != _aktualnaPolohaLongitude)
                {
                    _aktualnaPolohaLongitude = value;
                    OnPropertyChanged("AktualnaPolohaLongitude");
                }
            }
        }

        private string _polohaSpolu;
        public string PolohaSpolu
        {
            get => $"{AktualnaPolohaLatitude} - {AktualnaPolohaLongitude}";
            set
            {
                if (value != _polohaSpolu)
                {
                    _polohaSpolu = value;
                    OnPropertyChanged("PolohaSpolu");
                }
            }
        }


        private double _aktualnaPolohaLatitude;
        public double AktualnaPolohaLatitude
        {
            get => _aktualnaPolohaLatitude;
            set
            {
                if (value != _aktualnaPolohaLatitude)
                {
                    _aktualnaPolohaLatitude = value;
                    OnPropertyChanged("AktualnaPolohaLatitude");
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
