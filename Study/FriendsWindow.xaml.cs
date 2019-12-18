using StudentGrades.Classes;
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
    /// Логика взаимодействия для FriendsWindow.xaml
    /// </summary>
    public partial class FriendsWindow : Window
    {
        private Repository repository = Factory.Instance.GetRepository();
        public User User { get; set; }
        public FriendsWindow(User user)
        {
            InitializeComponent();
            User = user;
            UpdateWindow();
        }
        private void UpdateWindow()
        {
            FriendsBox.ItemsSource = User.Friends;
        }


        private void ShowFriend_Click(object sender, RoutedEventArgs e)
        {
            if (FriendsBox.SelectedItem == null) {
                MessageBox.Show("Select friend!!!");
                return;
                    }
            var cf = new FriendProfileWindow(User);
            if (cf.ShowDialog() == true)
                UpdateWindow();

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            return;
        }

        private void deleteFriend_Click(object sender, RoutedEventArgs e)
        {
            if (FriendsBox.SelectedItem == null)
            {
                MessageBox.Show("select friend");
                return;
            }
            var friend = FriendsBox.SelectedItem as User;
            User.Friends.Remove(friend);
            repository.Users[User.UserId] = User;
            repository.UpdateDatabase(User);
            UpdateWindow();

        }
    }
}
