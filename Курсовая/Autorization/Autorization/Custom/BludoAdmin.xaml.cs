using MySql.Data.MySqlClient;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Autorization.Custom
{
    /// <summary>
    /// Логика взаимодействия для BludoAdmin.xaml
    /// </summary>
    public partial class BludoAdmin : UserControl
    {
        private string res;
        public BludoAdmin(string source, string name, string gramm, string price)
        {
            InitializeComponent();
            Source.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(source);
            Name.Text = name;
            Gramm.Text = gramm;
            Summa.Text = price;
            res = source;
        }

        private void ChangeMenu(object sender, MouseButtonEventArgs e)
        {
            Data data = new Data();
            MySqlCommand command = new MySqlCommand("UPDATE `menu` SET `Name` = @NewName, `Gramm` = @NewGramm, `Price` = @NewPrice WHERE `menu`.`Source` = @Source", data.GetConnection());
            command.Parameters.Add("@NewName", MySqlDbType.VarChar).Value = Name.Text;
            command.Parameters.Add("@NewGramm", MySqlDbType.VarChar).Value = Gramm.Text;
            command.Parameters.Add("@NewPrice", MySqlDbType.VarChar).Value = Convert.ToInt16(Summa.Text);
            command.Parameters.Add("@Source", MySqlDbType.VarChar).Value = res;
            data.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Успешно изменено");
                data.CloseConnection();
            }
            else data.CloseConnection();
        }

        private void Delete(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            Data data = new Data();
            MySqlCommand command = new MySqlCommand("DELETE FROM `menu` WHERE `Name` = @Name", data.GetConnection());
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name.Text;
            data.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Успешно удалено");
                data.CloseConnection();
            }
            else data.CloseConnection();
        }
    }
}
