using Autorization.Class;
using Autorization.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Autorization.Custom
{
    /// <summary>
    /// Логика взаимодействия для Bludo.xaml
    /// </summary>
    public partial class Bludo : UserControl
    {
        int s = 1;
        public Bludo(string source, string name, string gramm, string price)
        {
            InitializeComponent();
            Source.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(source);
            Name.Text = name;
            Gramm.Text = gramm;
            Summa.Text = price;
        }
        public double getSum()
        {
            return s * Convert.ToDouble(Summa.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (s > 0 && s < 9)
            {
                s += 1;
                Col.Text = Convert.ToString(s);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (s > 1)
            {
                s -= 1;
                Col.Text = Convert.ToString(s);
            }
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Korb.sum += Convert.ToDouble(Summa.Text) * s;
            Zakaz korzina = new Zakaz(Name.Text, Convert.ToDouble(Summa.Text), s);
            Korb.list.Add(korzina);
        }
    }
}
