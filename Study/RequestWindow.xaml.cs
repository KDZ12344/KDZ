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
    /// Логика взаимодействия для RequestWindow.xaml
    /// </summary>
    public partial class RequestWindow : Window
    {
        public Repository repository = Factory.Instance.GetRepository();
        public User User { get; set; }
        public List<Request> userRequests = new List<Request>() { };
        public RequestWindow(User user)
        {
            InitializeComponent();
            User = user;
            UpdateWindow();
        }

        private void UpdateWindow()
        {
            userRequests = repository.Requests.FindAll(requset => requset.Receiver == User);
            if (userRequests.Count == 0 || userRequests[0] == null)
                userRequests.Add(new Request { Sender = new User { Name = "YouHaveNoRequests", Login = "0000", Password = "0000", UserId = 0 } });
            RequestBox.ItemsSource = userRequests; // it could not work if we dont add constructor of User class ???
        }
        private void ShowPerson_Click(object sender, RoutedEventArgs e)
        {
            var anyaItem = RequestBox.SelectedItem as User;
            if (anyaItem == null)
            {
                MessageBox.Show("Choose a request");
                return;
            }
            var anya = new FriendProfileWindow(anyaItem);
            anya.ShowDialog();
        }
        private void DeleteRequest(User anyaItem)
        {
            if (anyaItem == null)
            {
                MessageBox.Show("Choose a request");
                return;
            }
            userRequests.Remove(userRequests.First(item => item.Receiver == anyaItem));
            UpdateWindow();
            return;
        }
        private void deleteRequestClick(object sender, RoutedEventArgs e)
        {
            var anyaItem = RequestBox.SelectedItem as User;
            DeleteRequest(anyaItem);


        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            return;
        }

        private void RequestFriend_Click(object sender, RoutedEventArgs e)
        {
            
            User request = RequestBox.SelectedItem as User;
            User.Friends.Add(request);
            foreach (var item in repository.Requests)
            {
                if (item.Receiver == User && item.Sender == request)
                {
                    item.Status = false;
                }
            }
            //repository.Users[User.UserId] = User;
            //repository.UpdateDatabase(User);
            DeleteRequest(request);
            DialogResult = true;
        }
    }
}
