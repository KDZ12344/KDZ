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
using Study.Core;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public User user1 { get; set; }
        Repository rep = Factory.Instance.GetRepository();
        public RegistrationWindow(User user0)
        {
            InitializeComponent();
            user1 = user0;
        }

        private void LoginTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            
            int k = 1;
            if (DateTime.TryParse(BirthDateTextBox.Text, out DateTime birthdate))
            {
                k = 0;
            }           
            if (LoginTextBox.Text.Length > 6 && PasswordTextBox.Password == PasswordTextBox_Copy1.Password && NameTextBox.Text.Length > 0 && k == 0)
            {
                
                user1 = rep.Registration(NameTextBox.Text, LoginTextBox.Text, PasswordTextBox.Password, DateTime.Parse(BirthDateTextBox.Text), VkIdTextBox.Text, TgIdTextBox.Text);
                var chooseAvatar = new ChooseAvatar(user1);
                chooseAvatar.Show();
                
            }
            else
            {
                if (LoginTextBox.Text.Length <= 6)              
                    MessageBox.Show("Login's length should be more than 6 symbols.");               
                if (NameTextBox.Text.Length == 0)
                    MessageBox.Show("Name's length should be more than 0 symbols.");                
                if (PasswordTextBox.Password != PasswordTextBox_Copy1.Password)                
                    MessageBox.Show("Passwords don't match.");              
                if (!DateTime.TryParse(BirthDateTextBox.Text, out DateTime birthdate1))          
                    MessageBox.Show("Birthdate should be in format 2000-1-1");                
            }
            
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
