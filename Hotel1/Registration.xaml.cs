﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        const string connectionString = @"Server=DANIKDRANIK\TEW_SQLEXPRESS;Database=Hotel;Trusted_Connection=True;";
        public Registration()
        {
            InitializeComponent();
        }
        public bool RegexLogin(string login)
        {
            return new Regex("[A-Za-z0-9]{4,15}").IsMatch(login);
        }
        public bool RegexPassword(string password)
        {
            return new Regex("[A-Za-z0-9]{8,20}").IsMatch(password);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable find = SqlDB.Select($"select * from [Users] where login='{LoginBox.Text}' and password='{Password.Password}'");
            if (find.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
            }
            else
            {
                if (RegexLogin(LoginBox.Text))
                {
                    if (RegexPassword(Password.Password))
                    {
                        if (RepeatPassword.Password.Equals(Password.Password))
                        {
                            try
                            {
                                SqlDB.Command($"insert into [Users] values('{LoginBox.Text}', '{Password.Password}')");
                                MessageBox.Show("Успешно создан");
                                MainWindow mw = new MainWindow();
                                mw.Show();
                                Close();
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show(error.Message);
                            }
                        }
                        else MessageBox.Show("Пароли не совпадают");
                    }
                    else MessageBox.Show("Пароль обязан быть 8-20 символовв");
                }
                else MessageBox.Show("Логин обязан быть 4-15 символов");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            Close();
        }
    }
}
