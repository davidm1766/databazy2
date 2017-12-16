using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class TypVozna
    {
        
        private int _idTypuVozna;
        public int IdTypuVozna
        {
            get => _idTypuVozna;
            set
            {
                if (value != _idTypuVozna)
                {
                    _idTypuVozna = value;
                    OnPropertyChanged("IdTypuVozna");
                }
            }
        }
        private string _nazovTypuVozna;

        public string NazovTypuVozna
        {
            get => _nazovTypuVozna;
            set
            {
                if (value != _nazovTypuVozna)
                {
                    _nazovTypuVozna = value;
                    OnPropertyChanged("NazovTypuVozna");
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
