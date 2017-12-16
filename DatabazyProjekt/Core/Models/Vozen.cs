using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Vozen
    {
        private Vlastnik _vlastnikVozna;
        public Vlastnik VlastnikVozna
        {
            get => _vlastnikVozna;
            set
            {
                if (value != _vlastnikVozna)
                {
                    _vlastnikVozna = value;
                    OnPropertyChanged("VlastnikVozna");
                }
            }
        }

        private TypVozna _typvozna;
        public TypVozna TypVozna
        {
            get => _typvozna;
            set
            {
                if (value != _typvozna)
                {
                    _typvozna = value;
                    OnPropertyChanged("TypVozna");
                }
            }
        }

        private Poloha _aktualnaPoloha;
        public Poloha AktualnaPoloha
        {
            get => _aktualnaPoloha;
            set
            {
                if (value != _aktualnaPoloha)
                {
                    _aktualnaPoloha = value;
                    OnPropertyChanged("AktualnaPoloha");
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
