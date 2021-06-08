using Autorization.Pages;
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
    /// Логика взаимодействия для ZakazInfo.xaml
    /// </summary>
    public partial class ZakazInfo : UserControl
    {
        string listu;
        public ZakazInfo(string price, string place, string status, string list)
        {
            InitializeComponent();
            switch (status)
            {
                case "1":
                    status = "В ожидании";
                    break;
                case "2":
                    status = "Принят";
                    break;
                case "3":
                    status = "Отказано";
                    break;
            }
            listu = list;
            Status.Text = status;
            Price.Text = price + "руб";
            Place.Text = place;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListBlud listb = new ListBlud(listu);
            listb.Show();
            listb.Topmost = true;
        }
    }
}
