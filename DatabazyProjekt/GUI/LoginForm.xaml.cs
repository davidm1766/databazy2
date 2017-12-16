using System;
using System.Collections.Generic;
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

namespace GUI
{
    


    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        
        public LoginForm()
        {
            InitializeComponent();
            Button_Click(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var meno = TXTMeno.Text;
                var heslo = PasswordBox.Password;
                MainWindow mw = new MainWindow(meno,heslo);
                this.Hide();
                mw.Show();                
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
