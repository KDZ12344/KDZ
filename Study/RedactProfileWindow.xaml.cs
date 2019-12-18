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
    /// Логика взаимодействия для RedactProfileWindow.xaml
    /// </summary>
    public partial class RedactProfileWindow : Window
    {
        public User User { get; set; }
        Repository repository = Factory.Instance.GetRepository();
        public RedactProfileWindow(User user)
        {
            InitializeComponent();
            User = user;
            UpdateWindow();

        }
        private void UpdateWindow()
        {
            
            var uriSource = new Uri(@"/Study;component/" + User.AvatarAdress, UriKind.Relative);
            AvatarImage.Source = new BitmapImage(uriSource);
            LoginTextBlock.Text = User.Login;
            NameTextBlock.Text = User.Name;
            VKTextBlock.Text = User.VKID;
            TGTextBlock.Text = User.TelegramID;
            BirthDateTextBox.Text = User.BirthDate.ToString();
            ListNeedHelpWith.ItemsSource = repository.GetNeededSubjectsForUser(User);
            ListCanHelpWith.ItemsSource = repository.GetCanHelpWithSubjectsForUser(User);
        }
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(NameTextBlock.Text)))
            {
                MessageBox.Show("Login's length should be more than 6 symbols.");
                return;
            }
            else if (NameTextBlock.Text.Length == 0)
            {
                MessageBox.Show("Name's length should be more than 0 symbols.");
                return;
            }
            else if (!DateTime.TryParse(BirthDateTextBox.Text, out DateTime birthdate1))
            {
                MessageBox.Show("Birthdate should be in format 2000-1-1");
                return;
            }
            else if (NameTextBlock.Text.Length > 0 && LoginTextBlock.Text.Length > 6)
            {
                User.Login = LoginTextBlock.Text;
                User.Name = NameTextBlock.Text;
                User.VKID = VKTextBlock.Text;
                User.TelegramID = TGTextBlock.Text;
                User.BirthDate = DateTime.Parse(BirthDateTextBox.Text);
                if (MessageBox.Show("Do you want to change avatar picture?", "Confirm changes", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var chooseAvatar = new ChooseAvatar(User);
                    chooseAvatar.Show();
                }
                repository.ChangeUserProfile(User);
            }
        
        }
        private void DeleteCanHelpItem(object sender, RoutedEventArgs e)
        {
            var s7 = new СhooseNewInterestWindow(User, 2);
            if (s7.ShowDialog() == true)
                UpdateWindow();
        }

        private void DeleteNeedHelpItem(object sender, RoutedEventArgs e)
        {
            var s6 = new СhooseNewInterestWindow(User, 1);
            if (s6.ShowDialog() == true)
                UpdateWindow();





        }

        private void AddCanHelpItem(object sender, RoutedEventArgs e)
        {
            var s4 = new СhooseNewInterestWindow(User, 4);
            if (s4.ShowDialog() == true)
                UpdateWindow();

        }

        private void AddNeedHelpItem(object sender, RoutedEventArgs e)
        {
            var s5 = new СhooseNewInterestWindow(User, 3);
            if (s5.ShowDialog() == true)
                UpdateWindow();
        }

        
    }
}
