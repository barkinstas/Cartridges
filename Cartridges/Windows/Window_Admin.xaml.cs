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
    public partial class Window_Admin : Window
    {
        public Window_Admin()
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

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();


                string sqlExpression = "SELECT * FROM Users";
                SqliteCommand command = new SqliteCommand(sqlExpression, connection);

                List<User> users = new List<User>();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Role = reader.GetString(2),
                            Password = reader.GetString(3),

                        };
                        users.Add(user);
                    }
                }

                DataGrid_Users.ItemsSource = users;
            }
        }

        private void User_add(object sender, RoutedEventArgs e)
        {
            Addition_Admin x = new Addition_Admin();
            x.Show();
            Close();
        }

        private void Delete_User(object sender, RoutedEventArgs e)
        {
            User selectedItem = (User)DataGrid_Users.SelectedItem;

            if (selectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "DELETE FROM Users WHERE id = @id";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@id", selectedItem.id);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Сотрудник успешно удален");

                    LoadEmployees();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении сотрудника: " + ex.Message);
                }
            }
        }

        private void LoadEmployees()
        {
            try
            {
                List<User> users = new List<User>();

                using (var connection = new SqliteConnection("Data Source=db.db"))
                {
                    connection.Open();

                    string query = "SELECT id, Name, Role, Password FROM Users";
                    using (var command = new SqliteCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Role = reader.GetString(2),
                                Password = reader.GetString(3)
                            });
                        }
                    }
                }

                DataGrid_Users.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке сотрудников: " + ex.Message);
            }
        }

        private void Cartridges_Add(object sender, RoutedEventArgs e)
        {
            Addition_Cartridges x = new Addition_Cartridges();
            x.Show();
            Close();
        }

        private void Delete_Cartridges(object sender, RoutedEventArgs e)
        {
            Cartridge selectedItem = (Cartridge)DataGrid_Cartridges.SelectedItem;

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

                    LoadEmployees();
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

                DataGrid_Cartridges.ItemsSource = cartridges;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при загрузке сотрудников: " + ex.Message);
            }
        }
    }
}

    public class User
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
