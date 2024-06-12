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
    public partial class Editor_Password : Window
    {
        private const string CorrectPassword = "Redactor02";
        public Editor_Password()
        {
            InitializeComponent();
        }

        private void Editor_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Password.Text == CorrectPassword)
            {
                Window_Editor windowEditor = new Window_Editor();
                windowEditor.Show();
                this.Close();
            }
            else
            {
              
            }
        }
    }
}
