using System;
using System.Collections.Generic;
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

namespace GUI.VypisVstupy
{
	/// <summary>
	/// Interaction logic for VypisHistoriuVyskytuVoznaForm.xaml
	/// </summary>
	public partial class VypisHistoriuVyskytuVoznaForm : Window
	{
		public string IdVozna { get; set; }	
		private Action<DataSet> _vypisAction;

		public VypisHistoriuVyskytuVoznaForm(Action<DataSet> vypisAction)
		{
			_vypisAction = vypisAction;
			InitializeComponent();
			DataContext = this;
		}

		private void OKButton_OnClick(object sender, RoutedEventArgs e)
		{
			if (int.TryParse(IdVozna, out var idVozna))
			{
				DataSet dataSet = CoreApp.Instance.VypisHistoriuVyskytuVozna(idVozna);
				_vypisAction(dataSet);
				DialogResult = true;
				Close();
			}
			else
			{
				MessageBox.Show("Je potrebne zadať validne id vozna (cele cislo).", "Error", MessageBoxButton.OK,
					MessageBoxImage.Error);				
			}
		}

		private void ZrusButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
