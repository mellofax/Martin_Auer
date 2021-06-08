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
    /// Логика взаимодействия для Comm.xaml
    /// </summary>
    public partial class Comm : UserControl
    {
        public Comm(string topic, string text, string dost, string nedo, string name, string data)
        {
            InitializeComponent();
            Tema.Text = topic;
            Text.Text = text;
            Dost.Text = dost;
            Nedo.Text = nedo;
            Info.Text = name + ", " + data;
        }
    }
}
