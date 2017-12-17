using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Core;
using Core.Models;

namespace GUI.VypisVstupy
{
	/// <summary>
	/// Interaction logic for VypisAktualnuPolohuVoznov.xaml
	/// </summary>
	public partial class VypisInformacieOVoznochForm : Window
	{
		public ObservableCollection<Vlastnik> Vlastnici { get; set; }
		public ObservableCollection<TypVozna> TypyVoznov { get; set; }

		public string IdVozna { get; set; }
		public Vlastnik VlastnikVozna { get; set; }
		public TypVozna TypVozna { get; set; }

		private Action<DataSet> _vypisAction; 

		public VypisInformacieOVoznochForm(Action<DataSet> vypisAction, Collection<Vlastnik> vlastnici, Collection<TypVozna> typyVozna)
		{
			Vlastnici = new ObservableCollection<Vlastnik>(vlastnici);
			TypyVoznov = new ObservableCollection<TypVozna>(typyVozna);
			_vypisAction = vypisAction;
			InitializeComponent();
			DataContext = this;
		}

		private void OKButton_OnClick(object sender, RoutedEventArgs e)
		{			
			int idVozna = -1;

			if (!string.IsNullOrWhiteSpace(IdVozna) && !int.TryParse(IdVozna, out idVozna))
			{
				MessageBox.Show("Id vozna musí byť celé číslo.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			DataSet dataSet =
				CoreApp.Instance.VypisInformacieOVoznoch(idVozna, VlastnikVozna?.NazovVlastnika, TypVozna?.NazovTypuVozna);	

			_vypisAction(dataSet);
			DialogResult = true;
			Close();
		}

		private void CancelButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
