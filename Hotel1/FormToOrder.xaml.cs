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
    /// Логика взаимодействия для FormToOrder.xaml
    /// </summary>
    public partial class FormToOrder : Window
    {
        public FormToOrder()
        {
            InitializeComponent();
            InputPlaceholders();
            GetRoomsNumber();
        }

        public void GetRoomsNumber()
        {
            Rooms.ItemsSource = SqlDB.GetDataOneAttribute("SELECT * FROM Rooms WHERE NOT EXISTS " +
             "(SELECT * FROM RoomCard WHERE Rooms.id=RoomCard.id_room);", "number");
        }

        public void InputPlaceholders()
        {
            List<TextBox> collection = Form.Children.OfType<TextBox>().ToList();
            foreach(TextBox txt in collection)
            {
                txt.Text = $"Введите {txt.Name}";
                txt.GotFocus += RemoveText;
                txt.LostFocus += AddText;
            }
        }

        public void RemoveText(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text.Contains("Введите"))
            {
                txt.Text = "";
            }
        }

        public void AddText(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(txt.Text))
                txt.Text = $"Введите {txt.Name}";
        }
        public bool CheckDates()
        {
            if(dateIn.SelectedDate != null && dateOut.SelectedDate != null)
            {
                if((dateOut.SelectedDate - dateIn.SelectedDate).Value.TotalDays > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        private bool CheckRommAmount(int room_id, int amount)
        {
            DataTable dt = SqlDB.Select($"select * from Rooms where id={room_id}");
            int value = Convert.ToInt32(dt.Rows[0]["amount"]);
            if(value < amount)
            {
                MessageBox.Show("Количество проживающих не соответствует вместимости номера");
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            if(SqlDB.Command($"insert into ClientCard values ('{Полное_имя.Text}', '{Пасопортный_номер.Text}', '{Телефонный_номер.Text}')"))
            {
                message += "Клиент добавлен \n";
                int client_id = SqlDB.GetId($"SELECT TOP 1 id FROM ClientCard ORDER BY id DESC");
                int food = Convert.ToInt32(Food.IsChecked);
                int services = Convert.ToInt32(Services.IsChecked);
                int amount = Convert.ToInt32(Количество_проживающих.Text);
                int room_id = SqlDB.GetId($"select id from Rooms where number = {Convert.ToInt32(Rooms.SelectedItem)}");
                if (SqlDB.Command($"insert into RoomCard values ({room_id}, {client_id}, {food}, {services}, {amount})") && CheckRommAmount(room_id, amount))
                {
                    message += "Комната оформлена \n";
                    int room_card_id = SqlDB.GetId($"SELECT TOP 1 id FROM RoomCard ORDER BY id DESC");
                    if (SqlDB.Command($"insert into Dates values({room_card_id}, '{dateIn.SelectedDate.Value.ToString("yyyy-MM-dd")}', '{dateOut.SelectedDate.Value.ToString("yyyy-MM-dd")}')") && CheckDates())
                    {
                        MessageBox.Show(message);
                    }
                    else
                    {
                        MessageBox.Show("Что-то не так с датами");
                    }
                }
                else
                {
                    MessageBox.Show("Что-то не так с комнатой");
                }
            }
            else
            {
                MessageBox.Show("Что-то пошло не так с добавлением клиента");
            }
        }
    }
}
