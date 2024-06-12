using Microsoft.Data.Sqlite;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Cartridges.Windows
{
    public partial class Addition_Cartridges : Window
    {
        public Addition_Cartridges()
        {
            InitializeComponent();
        }

        private void Cartridges_Add(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Type.Text) ||
                string.IsNullOrWhiteSpace(Model.Text) ||
                string.IsNullOrWhiteSpace(Date.Text) ||
                string.IsNullOrWhiteSpace(Number.Text) ||
                string.IsNullOrWhiteSpace(State.Text) ||
                !int.TryParse(Number.Text, out int number) ||
                !int.TryParse(Date.Text, out int date))
            {
                MessageBox.Show("Заполните все поля и убедитесь, что номер введен корректно");
            }
            else
            {
                try
                {
                    using (var connection = new SqliteConnection("Data Source=db.db"))
                    {
                        connection.Open();

                        string query = "INSERT INTO Cartridges (Type, Model, Date, Number, State) VALUES (@Type, @Model, @Date, @Number, @State)";
                        using (var command = new SqliteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Type", Type.Text);
                            command.Parameters.AddWithValue("@Model", Model.Text);
                            command.Parameters.AddWithValue("@Date", date);
                            command.Parameters.AddWithValue("@Number", number);
                            command.Parameters.AddWithValue("@State", State.Text);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Данные успешно сохранены");

                    new Window_Admin().Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
                }
            }
        }
    }
}
