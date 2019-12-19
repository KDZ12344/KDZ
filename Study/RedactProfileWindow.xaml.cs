using Microsoft.Win32;
using Study.Core;
using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
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
            UpdateWindow();
        }

        private void UpdateWindow()
        {

            if (User.AvatarAdress != null)
            {
                avatarImage.Source = new ImageSourceConverter().ConvertFromString(User.AvatarAdress) as ImageSource;
            }
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
            repository.UpdateDatabase(User);
            UpdateWindow();
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
            repository.UpdateDatabase(User);
            UpdateWindow();
        }

        private void ImageChange_Button(object sender, RoutedEventArgs e)
        {
            //avatarImage.Source = repository.ImageUploading(User);
            //avatarImage.Source = rep.ImageUploading(user1);
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                var user2 = new User();
                Uri openUri = new Uri(open.FileName);
                var toSave = DateTime.Now.ToString() + Path.GetExtension(open.FileName);
                var imagePath = Path.Combine("C:\"" + toSave);
                user2.AvatarAdress = open.FileName;
                avatarImage.Source = new BitmapImage(openUri);

            }
        }

        private void AddNeedHelpItem(object sender, RoutedEventArgs e)
        {
            var lenaStalin = new СhooseNewInterestWindow(User, 3);
            if (lenaStalin.ShowDialog() == true)
            {
                UpdateWindow();
            }
        }


       
        private void ChangePicClick(object sender, RoutedEventArgs e)
        {
            //var choosewin = new ChooseAvatar(User);
            //choosewin.Show();
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                Uri openUri = new Uri(open.FileName);
                var toSave = DateTime.Now.ToString() + Path.GetExtension(open.FileName);
                var imagePath = Path.Combine("C:\'" + toSave);
                User.AvatarAdress = imagePath;
                avatarImage.Source = new BitmapImage(openUri);
            }
            else
            {
                MessageBox.Show("Please, choose an image.");
            }
        }

        private void AddCanHelpItem(object sender, RoutedEventArgs e)
        {
            var anyaTrozkiy = new СhooseNewInterestWindow(User, 4);
            if (anyaTrozkiy.ShowDialog() == true)
            {
                UpdateWindow();
            }
        }
    }
}
