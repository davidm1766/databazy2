using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Vlastnik
    {
        private int _idVlastnika;
        public int IdVlastnika
        {
            get => _idVlastnika;
            set
            {
                if (value != _idVlastnika)
                {
                    _idVlastnika = value;
                    OnPropertyChanged("IdVlastnika");
                }
            }
        }
        private string _nazovVlastnika;
        public string NazovVlastnika
        {
            get => _nazovVlastnika;
            set
            {
                if (value != _nazovVlastnika)
                {
                    _nazovVlastnika = value;
                    OnPropertyChanged("NazovVlastnika");
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
