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
    /// Логика взаимодействия для FriendProfileWindow.xaml
    /// </summary>
    public partial class FriendProfileWindow : Window
    {
        private Repository repository = Factory.Instance.GetRepository();
        public User User { get; set; }
        public FriendProfileWindow(User user)
        {
            User = user;
            InitializeComponent();
            UpdateWindow();
        }
        private void UpdateWindow()
        {
            var uriSource = new Uri(@"/Study;component/" + User.AvatarAdress, UriKind.Relative);
            avatarImage.Source = new BitmapImage(uriSource);

            NameTextBlock.Text = User.Name;
            VKTextBlock.Text = User.VKID;
            TGTextBlock.Text = User.TelegramID;
            LoginTextBlock.Text = User.Login;
            BirthDateTextBlock.Text = User.BirthDate.ToShortDateString();
            MajorTextBlock.Text = User.Major;
            BioTextBlock.Text = User.Bio;

            ListCanHelpWith.ItemsSource = User.CanHelpWithSubjects;
            ListNeedHelpWith.ItemsSource = User.NeedSubjects;
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var friendWin = new FriendProfileWindow(User);
            friendWin.Show();
            this.Close();
        }

        private void ListCanHelpWith_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListNeedHelpWith_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
