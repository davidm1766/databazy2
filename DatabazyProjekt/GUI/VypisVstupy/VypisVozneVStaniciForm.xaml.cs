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
	/// Interaction logic for VypisVozneVStaniciForm.xaml
	/// </summary>
	public partial class VypisVozneVStaniciForm : Window
	{
		public ObservableCollection<Vlastnik> Vlastnici { get; set; }
		public ObservableCollection<TypVozna> TypyVoznov { get; set; }
		public ObservableCollection<Stanica> Stanice { get; set; }

		public DateTime Od { get; set; }
		public DateTime Do { get; set; }
		public Vlastnik Vlastnik { get; set; }
		public TypVozna TypVozna { get; set; }
		public Stanica Stanica { get; set; }

		private Action<DataSet> _vypisAction;
		public VypisVozneVStaniciForm(Action<DataSet> vypisAction, ObservableCollection<Vlastnik> vlastnici, 
			ObservableCollection<TypVozna> typyVoznov, ObservableCollection<Stanica> stanice)
		{
			_vypisAction = vypisAction;
			Vlastnici = vlastnici;
			TypyVoznov = typyVoznov;
			Stanice = stanice;
			InitializeComponent();
			DataContext = this;
		}

		private void OKButton_OnClick(object sender, RoutedEventArgs e)
		{
			DataSet dataSet = CoreApp.Instance.VypisVozneVStanici(Stanica?.Nazov, Od, Do, Vlastnik?.NazovVlastnika, TypVozna?.NazovTypuVozna);
			_vypisAction(dataSet);
			DialogResult = true;
			Close();
		}

		private void ZrusButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
