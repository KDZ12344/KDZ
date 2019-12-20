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

            if (User.AvatarAdress.Trim() != "none" && User.AvatarAdress.Trim() != "")
            {
                avatarImage.Source = new BitmapImage(new Uri(User.AvatarAdress));
                //avatarImage.Source = new ImageSourceConverter().ConvertFromString(User.AvatarAdress) as ImageSource;
            }
            LoginTextBox.Text = User.Login;
            NameTextBox.Text = User.Name;
            VKTextBox.Text = User.VKID;
            TGTextBox.Text = User.TelegramID;
            BirthDateTextBox.Text = User.BirthDate.ToString();
            ListNeedHelpWith.ItemsSource = User.NeedSubjects;
            ListCanHelpWith.ItemsSource = User.CanHelpWithSubjects;
            BioTextBox.Text = User.Bio;

            MajorTextBox.Text = User.Major;
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
            else if (BioTextBox.Text.Length > 100)
            {
                MessageBox.Show("There is a limit of a 100 symbols.");
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
                User.Bio = BioTextBox.Text;
                User.Major = MajorTextBox.Text;

                var indexU = repository.Users.IndexOf(User);
                repository.Users[indexU] = User;
                repository.UpdateDatabase(User);
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
            this.Close();
            var red_win = new RedactProfileWindow(User);
            red_win.Show();
            repository.UpdateDatabase(User);    
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
            this.Close();
            var red_win = new RedactProfileWindow(User);
            red_win.Show();
            repository.UpdateDatabase(User);
            //UpdateWindow();
        }

        //private void ImageChange_Button(object sender, RoutedEventArgs e)
        //{
        //    //avatarImage.Source = repository.ImageUploading(User);
        //    //avatarImage.Source = rep.ImageUploading(user1);
        //    OpenFileDialog open = new OpenFileDialog();
        //    if (open.ShowDialog() == true)
        //    {
        //        var user2 = new User();
        //        Uri openUri = new Uri(open.FileName);
        //        var toSave = DateTime.Now.ToString() + Path.GetExtension(open.FileName);
        //        var imagePath = Path.Combine("C:\"" + toSave);
        //        user2.AvatarAdress = open.FileName;
        //        avatarImage.Source = new BitmapImage(openUri);

        //    }
        //}

        private void AddNeedHelpItem(object sender, RoutedEventArgs e)
        {
            var lenaStalin = new СhooseNewInterestWindow(User, 3);
            lenaStalin.Show();
            this.Close();
            
        }


       
        private void ChangePicClick(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                File.Copy(open.FileName, $"user{repository.Users.Count + 1}{Path.GetExtension(open.FileName)}");
                Uri openUri = new Uri(open.FileName);
                User.AvatarAdress = open.FileName;
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
            anyaTrozkiy.Show();
            this.Close();
        }

        private void NameTextBlock_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
