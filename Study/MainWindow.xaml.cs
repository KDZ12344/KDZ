﻿using System;
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
using System.Data.SqlClient;
using Study.Core;
using System.IO;

namespace Study
{
    public partial class MainWindow : Window
    {
        Repository repository = Factory.Instance.GetRepository();
        public User user { get; set; }
        public List<User> buddies { get; set; }
        public MainWindow()
        {  
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            user = repository.Authorization(LoginTextBox.Text, PasswordTextBox.Password);
            if (user!=null)
            {
                this.Hide();
                var choice = new UserMenu(user);
                choice.Show();
            }
            else
            {
                MessageBox.Show("There is no user with such login & password");
            }
        }
            
            
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var reg = new RegistrationWindow();
            
                reg.Show();
                this.Hide();
            

        }
    }
}
