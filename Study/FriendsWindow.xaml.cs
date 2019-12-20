using Study.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            FriendsBox.ItemsSource = User.Friends;
        }
        


        private void ShowFriend_Click(object sender, RoutedEventArgs e)
        {
            if (FriendsBox.SelectedItem == null)
            {
                MessageBox.Show("Select a friend");
                return;
            }
            var cf = new FriendProfileWindow(FriendsBox.SelectedItem as User);
            cf.Show();
            this.Close();

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
                MessageBox.Show("select a friend");
                return;
            }
            var friend = FriendsBox.SelectedItem as User;
            User.Friends.Remove(friend);
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "delete from Friend where 'Sender'='" + User.UserId + "' and 'Receiver'='" + friend.UserId + "'";// + " and 'Receiver' =" + User.UserId + " or 'Receiver' =" + friend.UserId;
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();

                // + add to /remove from Interests 

            }
            repository.Users[User.UserId] = User;
            repository.UpdateDatabase(User);
            this.Close();
            var frWin = new FriendsWindow(User);
            frWin.Show();
            

        }

        private void RequestFriend_Click(object sender, RoutedEventArgs e)
        {
            var lena = new RequestWindow(User);
            lena.ShowDialog();
        }

        private void ShowFriends_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var menu = new UserMenu(User);
            menu.Show();
        }

        private void FriendsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
