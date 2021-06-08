using Microsoft.Win32;
using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Autorization.Custom
{
    /// <summary>
    /// Логика взаимодействия для AddBludo.xaml
    /// </summary>
    public partial class AddBludo : UserControl
    {
        WrapPanel ListBookWrapPanel;
        private string toPhoto;
        private string type;

        public AddBludo(WrapPanel Panel)
        {
            InitializeComponent();
            ListBookWrapPanel = Panel;
        }

        private void Add(object sender, MouseButtonEventArgs e)
        {
            Bludo.Background = Brushes.White;
            Plus.Visibility = Visibility.Hidden;
            Exit.Visibility = ButtonAdd.Visibility = Name.Visibility = Gramm.Visibility = Summa.Visibility = Rub.Visibility = Type.Visibility = Photo.Visibility = Visibility.Visible;
            AddBludo addBludo = new AddBludo(ListBookWrapPanel);
            ListBookWrapPanel.Children.Add(addBludo);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "C:\\Users\\Nikita\\Desktop\\Курсовая\\Блюда";
                openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

                if (openFileDialog1.ShowDialog() == true)
                {
                    toPhoto = openFileDialog1.FileName;
                    Source.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(toPhoto);
                    Photo.Visibility = Visibility.Hidden;
                }
            }
            catch
            {
                MessageBox.Show("Invalid data");
            }
        }

        private void AddInfo(object sender, MouseButtonEventArgs e)
        {
            ButtonAdd.Visibility = Visibility.Hidden;
            switch(Type.Text)
            {
                case "Закуски":
                    {
                        type = "Snack";
                        break;
                    }
                case "Супы":
                    {
                        type = "Suppen";
                        break;
                    }
                case "Напитки":
                    {
                        type = "Napoi";
                        break;
                    }
                case "Салаты":
                    {
                        type = "Salate";
                        break;
                    }
            }
            Data data = new Data();
            MySqlCommand command = new MySqlCommand("INSERT INTO `menu` (`Source`, `Name`, `Gramm`, `Price`, `Type`) VALUES (@Source, @Name, @Gramm, @Price, @Type)", data.GetConnection());
            command.Parameters.Add("@Source", MySqlDbType.VarChar).Value = toPhoto;
            command.Parameters.Add("@Name", MySqlDbType.VarChar).Value = Name.Text;
            command.Parameters.Add("@Gramm", MySqlDbType.VarChar).Value = Gramm.Text;
            command.Parameters.Add("@Price", MySqlDbType.VarChar).Value = Convert.ToInt16(Summa.Text);
            command.Parameters.Add("@Type", MySqlDbType.VarChar).Value = type;
            data.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Успешно добавлено");
                data.CloseConnection();
            }
            else data.CloseConnection();
        }
    }
}
