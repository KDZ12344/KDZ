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
        public List<Request> userIncomingRequests = new List<Request>();
        public List<Request> userOutcomingRequests = new List<Request>();

        public RequestWindow(User user)
        {
            InitializeComponent();
            User = user;
            UpdateWindow();
            GetIncomingRequests();
            GetOutcomingRequests();
        }

        private void GetIncomingRequests()
        {
            foreach (var request in repository.Requests)
            {
                if (request.Receiver.UserId == User.UserId)
                {
                    userIncomingRequests.Add(request);
                }
            }
        }

        private void GetOutcomingRequests()
        {
            foreach (var request in repository.Requests)
            {
                if (request.Sender.UserId == User.UserId)
                {
                    userOutcomingRequests.Add(request);
                }
            }
        }

        private void UpdateWindow()
        { 
            //userRequests = repository.Requests.FindAll(requset => requset.Receiver == User);
           // if (userIncomingRequests.Count == 0 || userRequests[0] == null)
            //    userRequests.Add(new Request { Sender = new User { Name = "YouHaveNoRequests", Login = "0000", Password = "0000", UserId = 0 } });
            IncomingTabItem.Content = userIncomingRequests;
            OutcomingTabItem.Content = userOutcomingRequests;
            // it could not work if we dont add constructor of User class ???
        }
        private void ShowPerson_Click(object sender, RoutedEventArgs e)
        {
            var reqUser = TabRequests.SelectedItem as User;
            if (reqUser == null)
            {
                MessageBox.Show("Choose a request");
                return;
            }
            var anya = new FriendProfileWindow(reqUser);
            anya.ShowDialog();
        }
        //private void DeleteRequest(User anyaItem)
        //{
        //    if (anyaItem == null)
        //    {
        //        MessageBox.Show("Choose a request");
        //        return;
        //    }
        //    Tab.Remove(userRequests.First(item => item.Receiver == anyaItem));
        //    UpdateWindow();
        //    return;
        //}
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
                //if (item.Receiver == User && item.Sender == request)
                //{
                //    item.Status = false;
                //}
            }
            //repository.Users[User.UserId] = User;
            //repository.UpdateDatabase(User);
            DeleteRequest(request);
            DialogResult = true;
        }
    }
}
