using Autorization.Class;
using Autorization.Custom;
using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Autorization.Pages
{
    public partial class Order : Window
    {
        int s = 1;
        string Name;
        public Order(string name)
        {
            InitializeComponent();
            Name = name;
            if (Korb.list.Count == 0)
            {
                Complete.IsEnabled = false;
                TextBlock text = new TextBlock();
                text.Text = "Вы ничего не выбрали!";
                text.FontSize = 25;
                menu.Children.Add(text);
            }
            else
            {
                for (int i = 0; i < Korb.list.Count; i++)
                {
                    Korzina kor = new Korzina(Convert.ToString(Korb.list[i]));
                    menu.Children.Add(kor);
                }
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (s > 0 && s < 9)
            {
                s += 1;
                Col.Text = Convert.ToString(s);
            }
        }

        private void Button_Minus(object sender, RoutedEventArgs e)
        {
            if (s > 1)
            {
                s -= 1;
                Col.Text = Convert.ToString(s);
            }
        }
        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            Message.Visibility = Visibility.Visible;
            Complete.IsEnabled = false;
            string spis = "";
            Data data = new Data();
            for (int i = 0; i < Korb.list.Count; i++)
            {
                spis += Convert.ToString(Korb.list[i]) + "\r\n";
            }
            MySqlCommand command = new MySqlCommand("INSERT INTO `orders` (`Name`, `Price`, `Place`, `List`, `PeopleCount`) VALUES (@Name, @Price, @Place, @List, @Count)", data.GetConnection());
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name;
            command.Parameters.Add("@Price", MySqlDbType.VarChar).Value = Convert.ToString(Korb.sum);
            command.Parameters.Add("@Place", MySqlDbType.VarChar).Value = Place.Text;
            command.Parameters.Add("@Count", MySqlDbType.VarChar).Value = Convert.ToString(s);
            command.Parameters.Add("@List", MySqlDbType.VarChar).Value = spis;
            Korb.list.Clear();
            Korb.sum = 0;
            data.OpenConnection();
            command.ExecuteNonQuery();
            data.CloseConnection();
        }

        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Message.Visibility = Visibility.Hidden;
        }
    }
}
