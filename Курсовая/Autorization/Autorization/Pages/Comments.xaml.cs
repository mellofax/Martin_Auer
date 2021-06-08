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
using System.Windows.Shapes;

namespace Autorization.Pages
{
    /// <summary>
    /// Логика взаимодействия для Comments.xaml
    /// </summary>
    public partial class Comments : Window
    {
        string user;
        public Comments(string name)
        {
            InitializeComponent();
            user = name;
            Text.Text = user;
        }
    }
}
