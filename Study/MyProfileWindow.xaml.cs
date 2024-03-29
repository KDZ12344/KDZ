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
using System.Windows.Shapes;
using System.Data.SqlClient;
using Study.Core;
using System.IO;
using System.Resources;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : Window
    {
        public User User { get; set; }
        Repository rep = Factory.Instance.GetRepository();

        public MyProfileWindow(User user)
        {
            
            InitializeComponent();
            User = user;
            if (user.AvatarAdress != null)
            {//}
                AvatarImage.Source = new ImageSourceConverter().ConvertFromString(User.AvatarAdress) as ImageSource;
                int u = 0;
            }
            

            NameTextBlock.Text =User.Name;
            VKTextBlock.Text = User.VKID;
            TGTextBlock.Text = User.TelegramID;
            LoginTextBlock.Text = User.Login;


            List<User> friends = new List<User>();
            ListCanHelpWith.ItemsSource = user.CanHelpWithSubjects;
            ListNeedHelpWith.ItemsSource = user.NeedSubjects;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {  
            var redactionWindow = new RedactProfileWindow(User);
            redactionWindow.ShowDialog();
        }

        private void ListCanHelpWith_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListNeedHelpWith_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
