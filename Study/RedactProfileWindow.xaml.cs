using Study.Core;
using System;
using System.Windows;
using System.Windows.Media.Imaging;

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
            var uriSource = new Uri(@"/Study;component/" + User.AvatarAdress, UriKind.Relative);
            avatarImage.Source = new BitmapImage(uriSource);
            LoginTextBox.Text = User.Login;
            NameTextBox.Text = User.Name;
            VKTextBox.Text = User.VKID;
            TGTextBox.Text = User.TelegramID;
            BirthDateTextBox.Text = User.BirthDate.ToString();
            ListNeedHelpWith.ItemsSource = User.NeedSubjects;
            // repository.GetNeededSubjectsForUser(user);
            ListCanHelpWith.ItemsSource = User.CanHelpWithSubjects;
            //repository.GetCanHelpWithSubjectsForUser(user);

        }

        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(NameTextBox.Text)))
            {
                MessageBox.Show("Login's length should be more than 6 symbols.");
                return;
            }
            else if (NameTextBox.Text.Length == 0)
            {
                MessageBox.Show("Name's length should be more than 0 symbols.");
                return;
            }
            else if (!DateTime.TryParse(BirthDateTextBox.Text, out DateTime birthdate1))
            {
                MessageBox.Show("Birthdate should be in format 2000-1-1");
                return;
            }
            else if (NameTextBox.Text.Length > 0 && LoginTextBox.Text.Length > 6)
            {
                User.Login = LoginTextBox.Text;
                User.Name = NameTextBox.Text;
                User.VKID = VKTextBox.Text;
                User.TelegramID = TGTextBox.Text;
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
            var selectedCanSubject = ListCanHelpWith.SelectedItem as Interest;
            if (selectedCanSubject == null)
            {
                MessageBox.Show("Select a CanHelpSubject from the list");
                return;
            }
            User.NeedSubjects.Remove(selectedCanSubject);
            repository.ChangeUserProfile(User);
        }

        private void DeleteNeedHelpItem(object sender, RoutedEventArgs e)
        {
            var selectedNeedSubject = ListNeedHelpWith.SelectedItem as Interest;
            if (selectedNeedSubject == null)
            {
                MessageBox.Show("Select a NeedSubject from the list");
                return;
            }
            User.NeedSubjects.Remove(selectedNeedSubject);
            repository.ChangeUserProfile(User);
        }

        private void ImageChange_Button(object sender, RoutedEventArgs e)
        {
            avatarImage.Source = repository.ImageUploading(User);
        }

        private void AddNeedHelpItem(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
