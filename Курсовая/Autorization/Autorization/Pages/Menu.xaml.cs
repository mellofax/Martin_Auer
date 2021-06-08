using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using Autorization.Custom;
using Autorization.Class;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Autorization.Pages
{
    public partial class MenuNem : Window
    {
        bool root;
        string name;
        int click = 0;
        public MenuNem(User user, bool adminroot)
        {
            InitializeComponent();
            Name.Text = "Добро пожаловать, " +  user.Login + "!";
            this.Resources.Clear();
            root = adminroot;
            this.name = user.Login;
            if(adminroot)
            {
                Otzivs.Margin = new Thickness(606, 0, 227, 0);
                ZakazUser.Visibility = Visibility.Hidden;
                ZakazAdmin.Visibility = Visibility.Visible;
                Result.Visibility = Visibility.Hidden;
                Messages.Visibility = Visibility.Visible;
            }
        }
        //====================Главная=страница==================================
        private void TextContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Ostv.Visibility = Visibility.Hidden;
            Skroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            MenuBludandCommets.Visibility = Visibility.Hidden;
            OsnPage.Visibility = Visibility.Visible;
            Skroll.ScrollToHome();
            Menu.Content = null;
        }
        private void MinButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ExitButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Skroll.ScrollToHome();
        }
        private void Panel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void MenuBlud(object sender, MouseButtonEventArgs e)
        {
            if (Glav.Visibility == Visibility.Hidden)
                Glav.Visibility = Visibility.Visible;
            else
                Glav.Visibility = Visibility.Hidden;
        }
        //======================================================================

        //==================Другие=страницы=====================================
        private void News_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Skroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            MenuBludandCommets.Visibility = Ostv.Visibility = OsnPage.Visibility = Ostv.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
            Menu.Content = new News();
            Skroll.ScrollToHome();
        }
        private void Work_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Skroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            MenuBludandCommets.Visibility = Ostv.Visibility = Glav.Visibility = OsnPage.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
            Menu.Content = new Work();
            Skroll.ScrollToHome();
        }
        private void Story_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Skroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            MenuBludandCommets.Visibility = Ostv.Visibility = Glav.Visibility = OsnPage.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
            Menu.Content = new Story(this.Resources);
            Skroll.ScrollToHome();
        }
        private void Map_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Skroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            MenuBludandCommets.Visibility = Ostv.Visibility = OsnPage.Visibility = OsnPage.Visibility = Visibility.Hidden;
            Menu.Visibility = Visibility.Visible;
            Menu.Content = new Map(this.Resources);
            Skroll.ScrollToHome();
        }
        //======================================================================

        //================Доп=панель============================================
        private void Unlogin(object sender, MouseButtonEventArgs e)
        {
            Glav.Visibility = Visibility.Hidden;
            Skroll.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            Korb.list.Clear();
            Korb.sum = 0;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void ShowKorzina(object sender, MouseButtonEventArgs e)
        {
            History.Visibility = Visibility.Hidden;
            if (Post.Visibility == Visibility.Hidden)
                Post.Visibility = Visibility.Visible;
            else
                Post.Visibility = Visibility.Hidden;
            Korz.Children.Clear();
            if (Korb.list.Count == 0)
            {
                TextBlock text = new TextBlock();
                text.Text = "Вы ничего не выбрали!";
                text.FontSize = 25;
                Korz.Children.Add(text);
            }
            for (int i = 0; i < Korb.list.Count; i++)
            {
                Korzina kor = new Korzina(Convert.ToString(Korb.list[i]));
                Korz.Children.Add(kor);
            }
            suma.Text = Convert.ToString(Korb.sum) + " Руб.";
        }
        private void OrderFood(object sender, MouseButtonEventArgs e)
        {
            Post.Visibility = Visibility.Hidden;
            Order order = new Order(name);
            order.Topmost = true;
            order.Show();
        }
        private void Messages_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Message message = new Message();
            message.Show();
        }
        //======================================================================

        //======================Корзина=====================================
        private void ClearKorzina(object sender, MouseButtonEventArgs e)
        {
            Korb.list.Clear();
            Korb.sum = 0;
            Post.Visibility = Visibility.Hidden;
        }
        //==================================================================

        //======================Работа=с=бд=================================
        private List<object> getinfoMenu(string type)
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `menu` WHERE `Type` = @uL", data.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = type;
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
        private List<object> getinfoComments()
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `comments`", data.GetConnection());
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
        private List<object> getinfoOrders()
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @uL", data.GetConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = name;
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
        private List<object> getinfoHistory()
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `orders`", data.GetConnection());
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
        //==================================================================

        //=====================Меню=поиска==================================
        private void findSv()
        {
            Find find = new Find(ListBookWrapPanel, root);
            FindMenu.Children.Clear();
            FindMenu.Children.Add(find);
            Bot.Padding = new Thickness(0, 50, 0, 0);
        }
        //==================================================================


        //====================Фильтрация=по=типу=блюда======================
        private void Snack(object sender, MouseButtonEventArgs e)
        {
            Ostv.Visibility = Visibility.Hidden;
            MenuVisability();
            findSv();
            Bot.Padding = new Thickness(0, 50, 0, 0);
            if (root)
            {
                List<object> list = getinfoMenu("Snack");
                for (int i = 0; i < list.Count;)
                {
                    BludoAdmin bludo = new BludoAdmin(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
                AddBludo addBludo = new AddBludo(ListBookWrapPanel);
                ListBookWrapPanel.Children.Add(addBludo);
            }
            else
            {
                List<object> list = getinfoMenu("Snack");
                for (int i = 0; i < list.Count;)
                {
                    Bludo bludo = new Bludo(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
            }
        }
        private void Salate(object sender, MouseButtonEventArgs e)
        {
            Ostv.Visibility = Visibility.Hidden;
            MenuVisability();
            findSv();
            if (root)
            {
                List<object> list = getinfoMenu("Salate");
                for (int i = 0; i < list.Count;)
                {
                    BludoAdmin bludo = new BludoAdmin(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
                AddBludo addBludo = new AddBludo(ListBookWrapPanel);
                ListBookWrapPanel.Children.Add(addBludo);
            }
            else
            {
                List<object> list = getinfoMenu("Salate");
                for (int i = 0; i < list.Count;)
                {
                    Bludo bludo = new Bludo(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
            }
        }
        private void Suppen(object sender, MouseButtonEventArgs e)
        {
            Ostv.Visibility = Visibility.Hidden;
            MenuVisability();
            findSv();
            if (root)
            {
                List<object> list = getinfoMenu("Suppen");
                for (int i = 0; i < list.Count;)
                {
                    BludoAdmin bludo = new BludoAdmin(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
                AddBludo addBludo = new AddBludo(ListBookWrapPanel);
                ListBookWrapPanel.Children.Add(addBludo);
            }
            else
            {
                List<object> list = getinfoMenu("Suppen");
                for (int i = 0; i < list.Count;)
                {
                    Bludo bludo = new Bludo(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
            }
        }
        private void Napoi(object sender, MouseButtonEventArgs e)
        {
            Ostv.Visibility = Visibility.Hidden;
            MenuVisability();
            findSv();
            if (root)
            {
                List<object> list = getinfoMenu("Napoi");
                for (int i = 0; i < list.Count;)
                {
                    BludoAdmin bludo = new BludoAdmin(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
                AddBludo addBludo = new AddBludo(ListBookWrapPanel);
                ListBookWrapPanel.Children.Add(addBludo);
            }
            else
            {
                List<object> list = getinfoMenu("Napoi");
                for (int i = 0; i < list.Count;)
                {
                    Bludo bludo = new Bludo(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
            }
        }
        //==================================================================

        //======================Отзывы======================================
        private void Comments(object sender, MouseButtonEventArgs e)
        {
            Ostv.Visibility = Visibility.Visible;
            FindMenu.Children.Clear();
            Bot.Padding = new Thickness(0, 0, 0, 0);
            MenuVisability();
            List<object> list = getinfoComments();
            for (int i = 0; i < list.Count;)
            {
                Comm comments = new Comm(Convert.ToString(list[i]), Convert.ToString(list[i+1]), Convert.ToString(list[i+2]), Convert.ToString(list[i+3]), Convert.ToString(list[i+4]), Convert.ToString(list[i+5]));
                ListBookWrapPanel.Children.Add(comments);
                i += 6;
            }
        }
        private void AddOtziv(object sender, MouseButtonEventArgs e)
        {
            MenuVisability();
            Ostv.Visibility = Visibility.Hidden;
            AddComm addComm = new AddComm(name, ListBookWrapPanel);
            ListBookWrapPanel.Children.Add(addComm);
        }
        //==================================================================

        //========================Списки=заказов============================
        private void ZakazUserMenu(object sender, MouseButtonEventArgs e)
        {
            Histor.Children.Clear();
            Post.Visibility = Visibility.Hidden;
            if (History.Visibility == Visibility.Hidden)
                History.Visibility = Visibility.Visible;
            else
                History.Visibility = Visibility.Hidden;
            List<object> list = getinfoOrders();
            if(list.Count == 0)
            {
                TextBlock text = new TextBlock();
                text.Text = "История пустая!";
                text.FontSize = 30;
                text.Padding = new Thickness(55, 20, 50, 50);
                Histor.Children.Add(text);
            }
            for(int i = 0; i < list.Count; i++)
            {
                ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i+1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                Histor.Children.Add(zakazInfo);
                i += 5;
            }
        }
        private void ZakazAdminMenu(object sender, MouseButtonEventArgs e)
        {
            HistoryOfU.Children.Clear();
            if (HistoryUsers.Visibility == Visibility.Hidden)
                HistoryUsers.Visibility = Visibility.Visible;
            else
                HistoryUsers.Visibility = Visibility.Hidden;

            List<object> list = getinfoHistory();
            if (list.Count == 0)
            {
                TextBlock text = new TextBlock();
                text.Text = "История пустая!";
                text.FontSize = 30;
                text.Padding = new Thickness(55, 20, 50, 50);
                Histor.Children.Add(text);
            }
            for (int i = 0; i < list.Count; i++)
            {
                UsersZakaz UsersZakaz = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                HistoryOfU.Children.Add(UsersZakaz);
                i += 5;
            }
        }
        //==================================================================

        private void MenuVisability()
        {
            ListBookWrapPanel.Children.Clear();
            Glav.Visibility = OsnPage.Visibility = Menu.Visibility = Visibility.Hidden;
            Skroll.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            MenuBludandCommets.Visibility = Visibility.Visible;
        }

        private void Sort_By_Address(object sender, MouseButtonEventArgs e)
        {
            Histor.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `Place` DESC", data.GetConnection());
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    Histor.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `Place` ASC", data.GetConnection());
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    Histor.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
        private void Sort_By_List(object sender, MouseButtonEventArgs e)
        {
            Histor.Children.Clear();
            if (click == 0)
            {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `List` DESC", data.GetConnection());
            command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            List<object> list = new List<object>();
            foreach (DataRow r in table.Rows)
            {
                foreach (var cell in r.ItemArray)
                    list.Add(cell);
            }
            if (list.Count == 0)
            {
                TextBlock text = new TextBlock();
                text.Text = "История пустая!";
                text.FontSize = 30;
                text.Padding = new Thickness(55, 20, 50, 50);
                Histor.Children.Add(text);
            }
            for (int i = 0; i < list.Count; i++)
            {
                ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                Histor.Children.Add(zakazInfo);
                i += 5;
            }
            click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `List` ASC", data.GetConnection());
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    Histor.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
        private void Sort_By_Price(object sender, MouseButtonEventArgs e)
        {
            Histor.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `Price` DESC", data.GetConnection());
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    Histor.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `Price` ASC", data.GetConnection());
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    Histor.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
        private void Sort_By_Status(object sender, MouseButtonEventArgs e)
        {
            Histor.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `Status` DESC", data.GetConnection());
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    Histor.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` WHERE `Name` = @N  ORDER BY `Status` ASC", data.GetConnection());
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value = name;
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    ZakazInfo zakazInfo = new ZakazInfo(Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    Histor.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }

        private void Admin_Sort_By_Name(object sender, MouseButtonEventArgs e)
        {
            HistoryOfU.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Name` DESC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Name` ASC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
        private void Admin_Sort_By_Address(object sender, MouseButtonEventArgs e)
        {
            HistoryOfU.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Place` DESC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Place` ASC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
        private void Admin_Sort_By_List(object sender, MouseButtonEventArgs e)
        {
            HistoryOfU.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `List` DESC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `List` ASC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
        private void Admin_Sort_By_Price(object sender, MouseButtonEventArgs e)
        {
            HistoryOfU.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Price` DESC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Price` ASC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
        private void Admin_Sort_By_Status(object sender, MouseButtonEventArgs e)
        {
            HistoryOfU.Children.Clear();
            if (click == 0)
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Status` DESC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]) ,Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click += 1;
            }
            else
            {
                DataTable table = new DataTable();
                Data data = new Data();
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `orders` ORDER BY `Status` ASC", data.GetConnection());
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<object> list = new List<object>();
                foreach (DataRow r in table.Rows)
                {
                    foreach (var cell in r.ItemArray)
                        list.Add(cell);
                }
                if (list.Count == 0)
                {
                    TextBlock text = new TextBlock();
                    text.Text = "История пустая!";
                    text.FontSize = 30;
                    text.Padding = new Thickness(55, 20, 50, 50);
                    Histor.Children.Add(text);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    UsersZakaz zakazInfo = new UsersZakaz(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]), Convert.ToString(list[i + 4]));
                    HistoryOfU.Children.Add(zakazInfo);
                    i += 5;
                }
                click -= 1;
            }
        }
    }
}
