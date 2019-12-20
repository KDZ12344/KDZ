using System;
using System.Collections.Generic;
using System.IO;
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

using Microsoft.Win32;
using Study.Core;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public User user1 { get; set; }
        public User user2 { get; set; }
        Repository rep = Factory.Instance.GetRepository();
        public RegistrationWindow()
        {
            InitializeComponent();
            user1 = new User();
            
        }

       
        
        int j = 0;
        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            j = 1;
            user2 = new User();

            //avatarImage.Source = rep.ImageUploading(user1);
            //avatarImage.Source = new BitmapImage(new Uri(user1.AvatarAdress, UriKind.Relative));
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                File.Copy(open.FileName, $"user{rep.Users.Count + 1}{Path.GetExtension(open.FileName)}");
                Uri openUri = new Uri(open.FileName);
                user2.AvatarAdress = open.FileName;
                avatarImage.Source = new BitmapImage(openUri);

               
            }
        }
        
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            if (LoginTextBox.Text.Length > 6 && NameTextBox.Text.Length > 0 && PasswordBox.Password.Length > 0
                && BioTextBox.Text.Length > 0 && VKTextBox.Text.Length > 0 && TGTextBox.Text.Length > 0)
            {

                user1 = rep.Registration(NameTextBox.Text, LoginTextBox.Text, PasswordBox.Password, DateTime.Parse(BirthDateTextBox.Text), VKTextBox.Text, TGTextBox.Text, "none", BioTextBox.Text, MajorTextBox.Text);
                var chooseAvatar = new ChooseAvatar(user1);
                chooseAvatar.Show();
                if (user2 != null)
                {
                    rep.SavingToDatabase(user1.Login, user1.TelegramID, user1.VKID, user1.Name, user1.Password, user1.BirthDate, user1.DateAdded.Value, user2.AvatarAdress, user1.Bio, user1.Major);
                    user1.AvatarAdress = user2.AvatarAdress;
                    var userMenu = new UserMenu(user1);
                    userMenu.Show();
                    this.Close();
                }
                else
                {
                    rep.SavingToDatabase(user1.Login, user1.TelegramID, user1.VKID, user1.Name, user1.Password, user1.BirthDate, user1.DateAdded.Value, "none", user1.Bio, user1.Major);

                }


            }
            else
            {
                if (LoginTextBox.Text.Length <= 6)
                    MessageBox.Show("Login's length should be more than 6 symbols.");
                if (NameTextBox.Text.Length == 0)
                    MessageBox.Show("Name's length should be more than 0 symbols.");
                if (!DateTime.TryParse(BirthDateTextBox.Text, out DateTime birthdate1))
                    MessageBox.Show("Birthdate should be in format 2000-1-1");
                else
                {
                    MessageBox.Show("All fields should be filled!!!");
                }
            }

            
            
            
        }
    }
}
