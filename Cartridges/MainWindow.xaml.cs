using Cartridges.Windows;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cartridges
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Guest_Click(object sender, RoutedEventArgs e)
        {
            Window_Guest x = new Window_Guest();
            x.Show();
            Close();
        }

        private void Editor_Click(object sender, RoutedEventArgs e)
        {
            Editor_Password x = new Editor_Password();  
            x.Show();
            Close();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            Admin_Password x = new Admin_Password();
            x.Show();
            Close();
        }
    }
}