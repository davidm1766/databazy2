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
using System.Data;

namespace GUI.VypisVstupy
{
	/// <summary>
	/// Interaction logic for VypisInformacieOVlakoch.xaml
	/// </summary>
	public partial class VypisInformacieOVlakoch : Window
	{
		public ObservableCollection<TypVlaku> TypyVlakov { get; set; }
		public TypVlaku TypVlaku { get; set; }
		public string IdVlaku { get; set; }

		private Action<DataSet> _vypisAction;

		public VypisInformacieOVlakoch(Action<DataSet> vypisAction, ObservableCollection<TypVlaku> typyVlakov)
		{
			TypyVlakov = typyVlakov;
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

			DataSet dataSet = CoreApp.Instance.VypisInformacieOVlakoch(idVlaku, TypVlaku?.NazovTypuVlaku);

			_vypisAction(dataSet);
			DialogResult = true;
			Close();
		}

		private void ZrusitButton_OnClick(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
