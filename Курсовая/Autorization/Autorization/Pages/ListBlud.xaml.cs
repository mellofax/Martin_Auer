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
    /// Логика взаимодействия для ListBlud.xaml
    /// </summary>
    public partial class ListBlud : Window
    {
        public ListBlud(string spisok)
        {
            InitializeComponent();
            List.Text = spisok;
        }
    }
}
