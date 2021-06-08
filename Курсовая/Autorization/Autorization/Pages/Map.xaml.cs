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

namespace Autorization.Pages
{
    /// <summary>
    /// Логика взаимодействия для Map.xaml
    /// </summary>
    public partial class Map : Page
    {
        private int click1 = 0;
        private int click2 = 0;
        private int click3 = 0;
        public Map(ResourceDictionary resource)
        {
            InitializeComponent();
            this.Resources.Clear();
            this.Resources = resource;
        }

        private void Coffee_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (click1 == 0)
            {
                text2.Foreground = Brushes.Gray;
                text5.Foreground = Brushes.Gray;
                text7.Foreground = Brushes.Gray;
                text12.Foreground = Brushes.Gray;
                text13.Foreground = Brushes.Gray;
                text18.Foreground = Brushes.Gray;
                text21.Foreground = Brushes.Gray;
                click1 += 1;
            }
            else
            {
                text2.Foreground = Brushes.Black;
                text5.Foreground = Brushes.Black;
                text7.Foreground = Brushes.Black;
                text12.Foreground = Brushes.Black;
                text13.Foreground = Brushes.Black;
                text18.Foreground = Brushes.Black;
                text21.Foreground = Brushes.Black;
                click1 -= 1;
            }
        }

        private void Blun_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(click2 == 0)
            {
                text3.Foreground = Brushes.Gray;
                text15.Foreground = Brushes.Gray;
                text24.Foreground = Brushes.Gray;
                text25.Foreground = Brushes.Gray;
                click2 += 1;
            }
            else
            {
                text3.Foreground = Brushes.Black;
                text15.Foreground = Brushes.Black;
                text24.Foreground = Brushes.Black;
                text25.Foreground = Brushes.Black;
                click2 -= 1;
            }

        }

        private void SO_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (click3 == 0)
            {
                text9.Foreground = Brushes.Gray;
                text10.Foreground = Brushes.Gray;
                click3 += 1;
            }
            else
            {
                text9.Foreground = Brushes.Black;
                text10.Foreground = Brushes.Black;
                click3 -= 1;
            }
        }
    }
}
