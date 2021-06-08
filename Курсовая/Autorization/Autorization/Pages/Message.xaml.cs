using Autorization.Class;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using System.Windows.Shapes;

namespace Autorization.Pages
{
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : Window
    {
        MailAddress fromMailAddress = new MailAddress("barmansuperman4@gmail.com", "Admin");
        public Message()
        {
            InitializeComponent();
        }

        private List<object> getEmail()
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT `Email` FROM `list`", data.GetConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            List<object> list = new List<object>();
            foreach (DataRow r in table.Rows)
            {
                foreach (var cell in r.ItemArray)
                    list.Add(cell);
            }
            return list;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            List<object> spis = getEmail();
            for (int i = 0; i < spis.Count; i++)
            {
                MailMessage mailMessage = new MailMessage(fromMailAddress, new MailAddress(Convert.ToString(spis[i])));
                mailMessage.Subject = "Martin AUER";
                mailMessage.Body = Text.Text;
                Korb.SendMessage(fromMailAddress, new MailAddress(Convert.ToString(spis[i])), mailMessage);
            }
            MessageBox.Show("Пользователи осведомлены!");
        }
    }
}
