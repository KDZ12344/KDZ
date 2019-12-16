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
using System.Resources;
namespace Study
{
    /// <summary>
    /// Логика взаимодействия для ChooseAvatar.xaml
    /// </summary>
    public partial class ChooseAvatar : Window
    {
        public User User { get; set; }
        
        public ChooseAvatar(User user)
        {
            InitializeComponent();
            
            User = user;
        }
        List<string> avNames = new List<string>
        {
            "Avatars/Avatar1.png",
            "Avatars/Avatar2.png",
            "Avatars/Avatar3.png",
            "Avatars/Avatar4.png",
            "Avatars/Avatar5.png",
            "Avatars/Avatar6.png",
            "Avatars/Avatar7.png",
            "Avatars/Avatar8.png",
            "Avatars/Avatar9.png"
        };
        private void SelectingAvatar(List<string> avNames, int number)
        {
            MessageBox.Show("Are you sure?", "Choose avatar", MessageBoxButton.YesNoCancel);
            User.AvatarAdress = avNames[number - 1];
            var myprofile = new MyProfileWindow(User);
            myprofile.Show();
        }

        private void Avatar1CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 1);
        }

        private void Avatar5CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 5);
        }

        private void Avatar4CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 4);
        }

        private void Avatar6CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 6);
        }

        private void Avatar3CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 3);
        }

        private void Avatar8CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 8);
        }

        private void Avatar2CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 2);
        }


        private void Avatar9CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 9);
        }

        private void Avatar7CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectingAvatar(avNames, 7);
        }
    }
}
