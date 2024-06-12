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

namespace Cartridges.Windows
{
    public partial class Admin_Password : Window
    {
        private const string CorrectPassword = "Admin01";
        public Admin_Password()
        {
            InitializeComponent();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Password.Text == CorrectPassword)
            {
                Window_Admin windowAdmin = new Window_Admin();
                windowAdmin.Show();
                this.Close();
            }
            else
            {

            }
        }
    }
}
