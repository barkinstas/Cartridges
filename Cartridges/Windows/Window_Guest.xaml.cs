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
using Microsoft.Data.Sqlite;

namespace Cartridges.Windows
{
    public partial class Window_Guest : Window
    {
        public Window_Guest()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow x = new MainWindow();    
            x.Show();
            Close();
        }

        private void LoadData()
        {
            string connectionString = "DataSource=db.db";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            { 
                    connection.Open();
                    

                    string sqlExpression = "SELECT * FROM Cartridges";
                    SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                    List<Cartridge> cartridges = new List<Cartridge>();

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Cartridge cartridge = new Cartridge
                            {
                                id = reader.GetInt32(0),
                                Type = reader.GetString(1),
                                Model = reader.GetString(2),
                                Number = reader.GetString(3),
                                Date = reader.GetString(4),
                                State = reader.GetString(5)

                            };
                            cartridges.Add(cartridge);
                        }
                    }

                    DataGrid_Cartridges.ItemsSource = cartridges;
            }
        }
    }

    public class Cartridge
    {
        public int id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string Number { get; set; }
        public string Date { get; set; }
        public string State { get; set; }
    }
}




