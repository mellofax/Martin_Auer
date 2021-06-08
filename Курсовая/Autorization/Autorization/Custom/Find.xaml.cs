using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace Autorization.Custom
{
    /// <summary>
    /// Логика взаимодействия для Find.xaml
    /// </summary>
    public partial class Find : UserControl
    {
        WrapPanel ListBookWrapPanel;
        bool root;
        public Find(WrapPanel panel, bool prava)
        {
            root = prava;
            ListBookWrapPanel = panel;
            InitializeComponent();
        }

        private void FindText_TextChanged(object sender, TextChangedEventArgs e)
        {
            DataTable table = new DataTable();
            Data data = new Data();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `menu` WHERE `Name` LIKE @M", data.GetConnection());
            command.Parameters.Add("@M", MySqlDbType.VarChar).Value = "%" + FindText.Text + "%";
            adapter.SelectCommand = command;
            adapter.Fill(table);
            List<object> list = new List<object>();
            foreach (DataRow r in table.Rows)
            {
                foreach (var cell in r.ItemArray)
                    list.Add(cell);
            }
            if(table.Rows.Count == 0)
            {
                ListBookWrapPanel.Children.Clear();
                TextBlock text = new TextBlock();
                text.Text = "Ничего не найдено!";
                text.FontSize = 50;
                text.Padding = new Thickness(220, 130, 10, 10);
                ListBookWrapPanel.Children.Add(text);
            }
            else if(root)
            {
                ListBookWrapPanel.Children.Clear();
                for (int i = 0; i < list.Count;)
                {
                    BludoAdmin bludo = new BludoAdmin(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
            }
            else
            {
                ListBookWrapPanel.Children.Clear();
                for (int i = 0; i < list.Count;)
                {
                    Bludo bludo = new Bludo(Convert.ToString(list[i]), Convert.ToString(list[i + 1]), Convert.ToString(list[i + 2]), Convert.ToString(list[i + 3]));
                    ListBookWrapPanel.Children.Add(bludo);
                    i += 5;
                }
            }
        }
    }
}
