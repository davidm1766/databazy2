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
	/// Interaction logic for VypisVozneVoVlakuForm.xaml
	/// </summary>
	public partial class VypisVozneVoVlakuForm : Window
	{
		public ObservableCollection<TypVlaku> TypyVlakov { get; set; }
		public ObservableCollection<TypVozna> TypyVoznov { get; set; }
		public ObservableCollection<Vlastnik> Vlastnici { get; set; }

		public TypVlaku TypVlaku { get; set; }
		public TypVozna TypVozna { get; set; }
		public Vlastnik VlastnikVozna { get; set; }

		public string IdVlaku { get; set; }
		public DateTime CasOd { get; set; }
		public DateTime CasDo { get; set; }

		private Action<DataSet> _vypisAction;
		
		public VypisVozneVoVlakuForm(Action<DataSet> vypisAction, ObservableCollection<TypVlaku> typyVlakov, 
			ObservableCollection<TypVozna> typyVoznov, ObservableCollection<Vlastnik> vlastnici)
		{
			TypyVlakov = typyVlakov;
			TypyVoznov = typyVoznov;
			Vlastnici = vlastnici;
			_vypisAction = vypisAction;
			InitializeComponent();
			DataContext = this;
		}

		private void OKButton_OnClick(object sender, RoutedEventArgs e)
		{
			int idVlaku = -1;

			if (!string.IsNullOrWhiteSpace(IdVlaku) && !int.TryParse(IdVlaku, out idVlaku))
			{
				MessageBox.Show("Id vlaku musí byť celé číslo.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}								

			DataSet dataSet = CoreApp.Instance.VypisVozneVoVlaku(idVlaku, TypVlaku?.NazovTypuVlaku, 
				TypVozna?.NazovTypuVozna, VlastnikVozna?.NazovVlastnika, CasOd, CasDo);
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
