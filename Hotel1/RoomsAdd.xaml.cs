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
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Hotel1
{
    /// <summary>
    /// Логика взаимодействия для RoomsAdd.xaml
    /// </summary>
    public partial class RoomsAdd : Window
    {
        public RoomsAdd()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(SqlDB.Command($"insert into Rooms values({Convert.ToInt32(Number.Text)}, {Amount.Text}, {Convert.ToDouble(Price.Text)})"))
            {
                MessageBox.Show("Номер добавлен");
            }
            else
            {
                MessageBox.Show("Проверьте правильность введенных данных");
            }
        }
    }
}
