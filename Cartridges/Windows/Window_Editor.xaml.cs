using Microsoft.Data.Sqlite;
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
    public partial class Window_Editor : Window
    {
        public Window_Editor()
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

                DataGrid_Editor.ItemsSource = cartridges;
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            Cartridge selectedItem = (Cartridge)DataGrid_Editor.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите Картридж");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "DELETE FROM Cartridges WHERE id = @id";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedItem.id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Сотрудник успешно удален");

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении картриджа: " + ex.Message);
                }
            }
        }

        private void DeleteCartridges()
        {
            try
            {
                List<Cartridge> cartridges = new List<Cartridge>();

                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    string query = "SELECT Type, Model, Number, Date, State FROM Cartridges";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cartridges.Add(new Cartridge
                            {
                                Type = reader.GetString(0),
                                Model = reader.GetString(1),
                                Number = reader.GetString(2),
                                Date = reader.GetString(3),
                                State = reader.GetString(4)
                            });
                        }
                    }
                }

                DataGrid_Editor.ItemsSource = cartridges;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке сотрудников: " + ex.Message);
            }
        }
    }
}

