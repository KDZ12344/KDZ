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
        public RegistrationWindow(User user0)
        {
            InitializeComponent();
            user1 = new User();
            user1 = user0;
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            int k = 1;
            if (DateTime.TryParse(BirthDateTextBox.Text, out DateTime birthdate))
            {
                k = 0;
            }           
            if (LoginTextBox.Text.Length > 6 && k == 0)
            {
                k = 1;
                user1 = rep.Registration(NameTextBox.Text, LoginTextBox.Text, PasswordBox.Password, DateTime.Parse(BirthDateTextBox.Text), VKTextBox.Text, TGTextBox.Text, "none");
                var chooseAvatar = new ChooseAvatar(user1);
                chooseAvatar.Show();
                
            }
            else
            {
                if (LoginTextBox.Text.Length <= 6)              
                    MessageBox.Show("Login's length should be more than 6 symbols.");               
                if (NameTextBox.Text.Length == 0)
                    MessageBox.Show("Name's length should be more than 0 symbols.");                              
                if (!DateTime.TryParse(BirthDateTextBox.Text, out DateTime birthdate1))          
                    MessageBox.Show("Birthdate should be in format 2000-1-1");                
            }
            
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            
            //avatarImage.Source = rep.ImageUploading(user1);
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                user2 = new User();
                Uri openUri = new Uri(open.FileName);
                var toSave = DateTime.Now.ToString() + Path.GetExtension(open.FileName);
                var imagePath = Path.Combine("C:\"" + toSave);
                user2.AvatarAdress = open.FileName;
                avatarImage.Source = new BitmapImage(openUri);
               
            }
        }
        int k = 0;
        private void SaveChanges(object sender, RoutedEventArgs e)
        {
            
                rep.SavingToDatabase(user1.Login, user1.TelegramID, user1.VKID, user1.Name, user1.Password, user1.BirthDate, user1.DateAdded.Value, user2.AvatarAdress);
                var userMenu = new UserMenu(user1);
                userMenu.Show();
                this.Close();
            
            
        }
    }
}
