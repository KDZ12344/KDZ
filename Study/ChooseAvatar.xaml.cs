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
using Study.Core;
using System.Data.SqlClient;
namespace Study
{
    /// <summary>
    /// Логика взаимодействия для ChooseAvatar.xaml
    /// </summary>
    public partial class ChooseAvatar : Window
    {
        User user1 = new User();
        public ChooseAvatar(User user)
        {
            InitializeComponent();
            user1 = user;
        }

        private void Avatar1CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar1.png";
            user1.AvatarAdress = avName;
            
            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar5CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar2.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar4CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar7.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar6CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar6.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar3CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar4.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar8CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar3.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar2CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar9.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar9CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar8.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }

        private void Avatar7CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            string avName = "Avatars/Avatar5.png";
            user1.AvatarAdress = avName;

            var myprofile = new MyProfileWindow(user1);
            myprofile.Show();
        }
    }
}
