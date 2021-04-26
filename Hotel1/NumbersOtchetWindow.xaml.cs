using Hotel1.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Hotel1
{
    /// <summary>
    /// Логика взаимодействия для NumbersOtchetWindow.xaml
    /// </summary>
    public partial class NumbersOtchetWindow : Window
    {
        public NumbersOtchetWindow()
        {
            InitializeComponent();
        }
        public void SetNumbers(string selection)
        {
            List<Numbers> rooms = new List<Numbers>();
            DataTable dt = SqlDB.Select(selection);
            if (dt.Rows.Count <= 0)
            {
                MessageBox.Show("Таких номеров нет");
                Table.ItemsSource = rooms;
                return;
            }
            foreach (DataRow dr in dt.Rows)
            {
                rooms.Add(new Numbers()
                {
                    Number = Convert.ToInt32(dr["number"].ToString()),
                    Price = Convert.ToDouble(dr["price"].ToString())
                });
            }
            Table.ItemsSource = rooms;
        }

        private void Empty_Click(object sender, RoutedEventArgs e)
        {
            SetNumbers("SELECT * FROM Rooms WHERE NOT EXISTS (SELECT * FROM RoomCard WHERE Rooms.id=RoomCard.id_room)");
        }

        private void Closed_Click(object sender, RoutedEventArgs e)
        {
            SetNumbers("SELECT * FROM Rooms join RoomCard on Rooms.id=RoomCard.id_room");
        }
    }
}
