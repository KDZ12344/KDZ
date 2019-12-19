using Study.Core;
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

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для UserMenue.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        public User User { get; set; }
        public UserMenu(User user)
        {
            InitializeComponent();
            User = user;
            HelloTextBlock.Text = $"Hello, {User.Login}!";
        }

        private void PersonalSettings_Click(object sender, RoutedEventArgs e)
        {
            var s1 = new RedactProfileWindow(User);
            s1.ShowDialog();
        }


        private void ShowChoice_Click(object sender, RoutedEventArgs e)
        {
            var s2 = new ChoiceWindow(User);
            s2.ShowDialog();
        }

        private void ShowFriends_Click(object sender, RoutedEventArgs e)
        {
            var s3 = new FriendsWindow(User);
            s3.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
