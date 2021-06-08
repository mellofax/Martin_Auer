using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Autorization.Custom
{
    public partial class AddComm : UserControl
    {
        string name;
        WrapPanel ListBookWrapPanel;
        public AddComm(string name, WrapPanel panel)
        {
            InitializeComponent();
            this.name = name;
            ListBookWrapPanel = panel;
        }

        private void Addcom(object sender, MouseButtonEventArgs e)
        {
            Data data = new Data();
            MySqlCommand command = new MySqlCommand("INSERT INTO `comments` (`Topic`, `Text`, `Advantages`, `Disadvantages`, `Name`, `Date`) VALUES (@Tema, @Text, @Dost, @Nedo, @Name ,CURRENT_DATE ())", data.GetConnection());
            command.Parameters.Add("@Tema", MySqlDbType.VarChar).Value = Tema.Text;
            command.Parameters.Add("@Text", MySqlDbType.VarChar).Value = Text.Text;
            command.Parameters.Add("@Dost", MySqlDbType.VarChar).Value = Dost.Text;
            command.Parameters.Add("@Nedo", MySqlDbType.VarChar).Value = Nedo.Text;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = name;
            data.OpenConnection();
            command.ExecuteNonQuery();
            data.CloseConnection();
            ListBookWrapPanel.Children.Clear();
            TextBlock text = new TextBlock();
            text.Text = "Спасибо за отзыв!";
            text.FontSize = 50;
            text.Padding = new Thickness(220,150,10,10);
            ListBookWrapPanel.Children.Add(text);
        }
    }
}
