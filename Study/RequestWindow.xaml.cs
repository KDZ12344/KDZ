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
            IncomingListBox.ItemsSource = userIncomingRequests;
            OutcomingListBox.ItemsSource = userOutcomingRequests;
        }
        private void ShowPerson_Click(object sender, RoutedEventArgs e)
        {
            if (OutcomingTabItem.IsSelected == true)
            {
                var reqUser = OutcomingListBox.SelectedItem as Request;
                User us0 = repository.GetUserById(reqUser.Receiver.UserId);
                var anya = new FriendProfileWindow(us0);
                anya.ShowDialog();
                this.Close();
            }
            if (IncomingTabItem.IsSelected == true)
            {
                var reqUser = IncomingListBox.SelectedItem as Request;
                User us0 = repository.GetUserById(reqUser.Sender.UserId);
                var anya = new FriendProfileWindow(us0);
                anya.ShowDialog();
                this.Close();
            }



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
        //private void deleteRequestClick(object sender, RoutedEventArgs e)
        //{
        //    var anyaItem = RequestBox.SelectedItem as User;
        //    DeleteRequest(anyaItem);


        //}

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            return;
        }

        private void RequestFriend_Click(object sender, RoutedEventArgs e)
        {
            User us = new User();
            Request req0 = IncomingListBox.SelectedItem as Request;
            if (OutcomingListBox.SelectedItem != null && OutcomingTabItem.IsSelected==true)
            {
                MessageBox.Show("You should wait until this user accept your request.");
            }
            else
            {
                us = repository.GetUserById(req0.Sender.UserId);
                User.Friends.Add(us);
                repository.Requests.Remove(IncomingListBox.SelectedItem as Request);
                userIncomingRequests.Remove(IncomingListBox.SelectedItem as Request);
                this.Close();
                var reqWin = new RequestWindow(User);
                reqWin.Show();
            }
            
        }

        private void deleteRequestClick(object sender, RoutedEventArgs e)
        {

        }

        //private void RequestFriend_Click(object sender, RoutedEventArgs e)
        //{

        //    User request = RequestBox.SelectedItem as User;
        //    User.Friends.Add(request);
        //    foreach (var item in repository.Requests)
        //    {
        //        //if (item.Receiver == User && item.Sender == request)
        //        //{
        //        //    item.Status = false;
        //        //}
        //    }
        //repository.Users[User.UserId] = User;
        //repository.UpdateDatabase(User);
        //    DeleteRequest(request);
        //    DialogResult = true;
        //}
    }
}
