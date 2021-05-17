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

namespace Hotel1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddRoomButton_Click(object sender, RoutedEventArgs e)
        {
            RoomsAdd add = new RoomsAdd();
            add.Show();
        }

        private void OrderClient_Click(object sender, RoutedEventArgs e)
        {
            FormToOrder form = new FormToOrder();
            form.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Show();
            Close();
        }

        private void NumbersOtchet_Click(object sender, RoutedEventArgs e)
        {
            NumbersOtchetWindow numbers = new NumbersOtchetWindow();
            numbers.Show();
        }

        private void PersonsLive_Click(object sender, RoutedEventArgs e)
        {
            PersonsLiveWindow liveWindow = new PersonsLiveWindow();
            liveWindow.Show();
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            InfoWindow window = new InfoWindow();
            window.Show();
        }
    }
}
