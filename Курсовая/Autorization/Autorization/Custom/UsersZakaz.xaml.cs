using Autorization.Class;
using Autorization.Pages;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autorization.Custom
{
    /// <summary>
    /// Логика взаимодействия для UsersZakaz.xaml
    /// </summary>
    public partial class UsersZakaz : UserControl
    {
        MailAddress fromMailAddress = new MailAddress("barmansuperman4@gmail.com","Admin");
        string listu;
        public UsersZakaz(string name, string price, string place, string status, string list)
        {
            InitializeComponent();
            switch (status)
            {
                case "1":
                    Accept.Visibility = Visibility.Visible;
                    Decline.Visibility = Visibility.Visible;
                    break;
                case "2":
                    status = "Принят";
                    break;
                case "3":
                    status = "Отказано";
                    break;
            }
            Name.Text = name;
            listu = list;
            Status.Text = status;
            Price.Text = price + "руб";
            Place.Text = place;
        }

        private string getEmail()
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `Email` FROM `list` WHERE `Login` = @Name", data.GetConnection());
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            List<object> list = new List<object>();
            foreach (DataRow r in table.Rows)
            {
                foreach (var cell in r.ItemArray)
                    list.Add(cell);
            }
            return Convert.ToString(list[0]);
        }
        private void changestatus(string status)
        {
            Data data = new Data();
            MySqlCommand command = new MySqlCommand("UPDATE `orders` SET `Status` = @Status WHERE `Name` = @Name", data.GetConnection());
            command.Parameters.Add("@Status", MySqlDbType.VarChar).Value = status;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name.Text;
            data.OpenConnection();
            command.ExecuteNonQuery();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBlud listb = new ListBlud(listu);
            listb.Show();
            listb.Topmost = true;
        }

        private void Button_Accept(object sender, RoutedEventArgs e)
        {
            Accept.Visibility = Visibility.Hidden;
            Decline.Visibility = Visibility.Hidden;
            Status.Text = "Принят";
            MailAddress toAddress = new MailAddress(getEmail(), Name.Text);
            MailMessage mailMessage = new MailMessage(fromMailAddress, toAddress);
            mailMessage.Subject = "Martin AUER";
            mailMessage.Body = "Здравствуйте, спешим сообщить, что ваш заказ принят!";
            Korb.SendMessage(fromMailAddress, toAddress, mailMessage);
            changestatus("2");
        }

        private void Button_Decline(object sender, RoutedEventArgs e)
        {
            Accept.Visibility = Visibility.Hidden;
            Decline.Visibility = Visibility.Hidden;
            Status.Text = "Отказано";
            MailAddress toAddress = new MailAddress(getEmail(), Name.Text);
            MailMessage mailMessage = new MailMessage(fromMailAddress, toAddress);
            mailMessage.Subject = "Martin AUER";
            mailMessage.Body = "Здравствуйте, спешим сообщить, что на ваш заказ был получен отрицательный ответ :(";
            Korb.SendMessage(fromMailAddress, toAddress, mailMessage);
            changestatus("3");
        }
    }
}
