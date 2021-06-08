using System.Windows;
using System;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using System.Data;
using Autorization.Pages;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Text;

namespace Autorization
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int reg = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        //===================События==в==меню================================
        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ToolBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void LogoContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        //===================================================================

        //====================Ошибки==в==тексте==============================
        private bool Simvols(string text)
        {
            bool test = false;
            string b = ",./!@#$%^&*()-+={}[]|~`<>";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                for (int k = 0; k < b.Length; k++)
                {
                    char d = b[k];
                    if (c == d)
                    {
                        test = true;
                    }
                }
            }
            return test;
        }
        private bool Email(string text)
        {
            string pattern = "[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}";
            Match isMatch = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
            return isMatch.Success;
        }
        //===================================================================


        //====================Хэширование====================================
        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        //===================================================================


        //====================Вывод==ошибок==================================
        private void Log_Click(object sender, MouseButtonEventArgs e)
        {
            LogMenu.Visibility = Visibility.Hidden;
        }
        private void ShowError(string s)
        {

            LogText.Text = "В процессе авторизации возникли ошибки";
            Log.Text = s;
            LogMenu.Visibility = Visibility.Visible;
        }
        private void ShowRegistration()
        {
            LogText.Text = "Регистрация прошла успешно!";
            Log.Text = "";
            LogMenu.Visibility = Visibility.Visible;
        }
        //==================================================================


        //====================Работы==с==БД=================================
        private bool ChechLogin(string login)
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `list` WHERE `Login` = @uL", data.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if(table.Rows.Count >=1)
            {
                ShowError("Такой логин занят!");
                return false;
            }
            return true;
        }
        private bool Find_User()
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `list` WHERE `Login` = @uL", data.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = tb1.Text;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count < 1)
            {
               ShowError("Пользователь с таким логином не найден");
               return false;
            }
            else
            {
               command = new MySqlCommand("SELECT * FROM `list` WHERE `Password` = @uL", data.GetConnection());
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = CalculateMD5Hash(tb2.Password);
                adapter.SelectCommand = command;
               adapter.Fill(table);
               if (table.Rows.Count < 2)
                  {
                      ShowError("Неверный пароль");
                      return false;
                  }
            }
            return true;
        }
        private bool Add_User(string login, string password, string email)
        {
            if (ChechLogin(login) && Email(email) && !Simvols(login) && !Simvols(password))
            {
                Data data = new Data();
                MySqlCommand command = new MySqlCommand("INSERT INTO `list` (`Login`, `Password`, `Email`) VALUES (@uL, @uP, @uM)", data.GetConnection());
                command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = password;
                command.Parameters.Add("@uM", MySqlDbType.VarChar).Value = email;
                data.OpenConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    data.CloseConnection();
                    return true;
                }
                else data.CloseConnection();
            }
            else ShowError("Некорректно заполнены формы");
            return false;
        }
        //==================================================================

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            bool admin = false;
            if (tb1.Text == "Admin")
            {
                admin = true;
            }
            else
                admin = false;

                LogMenu.Visibility = Visibility.Hidden;
                if (Find_User())
                {
                    User user = new User(tb1.Text, tb2.Password);
                    MenuNem menu = new MenuNem(user, admin);
                    menu.Show();
                    this.Close();
                }
        }
        private void Registration_Click(object sender, RoutedEventArgs e)
        {
                if (Add_User(tb1.Text, CalculateMD5Hash(tb2.Password), tb3.Text))
                {
                    tb3.Visibility = Visibility.Hidden;
                    Em.Foreground = null;
                    tb1.Text = tb2.Password = tb3.Text = "";
                    ButtonLogin.Click -= Registration_Click;
                    ButtonLogin.Click += Autorization_Click;
                    reg -= 1;
                    Registr.Text = "Создать учетную запись";
                ButtonLogin.Content = "Войти";
                ShowRegistration();
                }
        }

        private void RegistrationOrLogin(object sender, MouseButtonEventArgs e)
        {
            if (reg == 0)
            {
                tb3.Visibility = Visibility.Visible;
                Em.Foreground = Brushes.Black;
                Registr.Text = "Авторизоваться";
                ButtonLogin.Content = "Зарегестрироваться";
                tb1.Text = tb2.Password = tb3.Text = "";
                ButtonLogin.Click -= Autorization_Click;
                ButtonLogin.Click += Registration_Click;
                reg += 1;
            }
            else
            {
                tb3.Visibility = Visibility.Hidden;
                Registr.Text = "Создать учетную запись";
                ButtonLogin.Content = "Войти";
                Em.Foreground = null;
                tb1.Text = tb2.Password = tb3.Text = "";
                ButtonLogin.Click -= Registration_Click;
                ButtonLogin.Click += Autorization_Click;
                reg -= 1;
            }
        }
        private void ChangePas(object sender, RoutedEventArgs e)
        {
            if (tb2.Password == "")
                TextPassword.Visibility = Visibility.Visible;
            else TextPassword.Visibility = Visibility.Hidden;
        }
    }
}
