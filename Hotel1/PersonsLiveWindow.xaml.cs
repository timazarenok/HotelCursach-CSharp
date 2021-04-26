using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Excel = Microsoft.Office.Interop.Excel;

namespace Hotel1
{
    /// <summary>
    /// Логика взаимодействия для PersonsLiveWindow.xaml
    /// </summary>
    public partial class PersonsLiveWindow : Window
    {
        public List<Persons> rooms = new List<Persons>();
        public PersonsLiveWindow()
        {
            InitializeComponent();
            SetPersons();
        }
        public void SetPersons()
        {
            rooms = new List<Persons>();
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
        private void Excel_Click(object sender, EventArgs e)
        {
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "Полное имя";
            ExcelApp.Cells[1, 2] = "Пасспортный номер";
            ExcelApp.Cells[1, 3] = "Тел. номер";
            ExcelApp.Cells[1, 4] = "Еда";
            ExcelApp.Cells[1, 5] = "Доп. услуги";
            ExcelApp.Cells[1, 6] = "Количество";
            ExcelApp.Cells[1, 7] = "Номер";

            var list = Table.Items.OfType<Persons>().ToList();

            for (int j = 0; j < list.Count; j++)
            {
                ExcelApp.Cells[j + 2, 1] = list[j].Full_Name;
                ExcelApp.Cells[j + 2, 2] = list[j].PassportNumber;
                ExcelApp.Cells[j + 2, 3] = list[j].TelephoneNumber;
                ExcelApp.Cells[j + 2, 4] = list[j].Food;
                ExcelApp.Cells[j + 2, 5] = list[j].Services;
                ExcelApp.Cells[j + 2, 6] = list[j].Amount;
                ExcelApp.Cells[j + 2, 7] = list[j].Number;
            }
            ExcelApp.Visible = true;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((TextBox)sender).Text;
            Table.ItemsSource = rooms.FindAll(item => item.Full_Name.Contains(text));
        }
    }
}
