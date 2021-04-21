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
    /// Логика взаимодействия для PersonsLiveWindow.xaml
    /// </summary>
    public partial class PersonsLiveWindow : Window
    {
        public PersonsLiveWindow()
        {
            InitializeComponent();
            SetPersons();
        }
        public void SetPersons()
        {
            List<Persons> rooms = new List<Persons>();
            DataTable dt = SqlDB.Select("SELECT full_name, passport_number, telephone_number, number, price, food, [services], amount FROM Rooms join RoomCard on Rooms.id = RoomCard.id_room join ClientCard on ClientCard.id = id_client_card; ");
            foreach (DataRow dr in dt.Rows)
            {
                rooms.Add(new Persons()
                {
                    Full_Name = dr["full_name"].ToString(),
                    PassportNumber = dr["passport_number"].ToString(),
                    TelephoneNumber = dr["telephone_number"].ToString(),
                    Food = CheckFoodAndServices(dr["food"].ToString()),
                    Services = CheckFoodAndServices(dr["services"].ToString()),
                    Amount = Convert.ToInt32(dr["amount"]),
                    Number = Convert.ToInt32(dr["number"].ToString()),
                    Price = Convert.ToDouble(dr["price"].ToString())
                });
            }
            Table.ItemsSource = rooms;
        }
        public string CheckFoodAndServices(string value)
        {
            return value == "1" ? "Да" : "Нет";
        }
    }
}
