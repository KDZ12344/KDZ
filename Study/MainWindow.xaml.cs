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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Study.Core;
using System.IO;

namespace Study
{
    public partial class MainWindow : Window
    {
        public Repository repository { get; set; }
        public User user { get; set; }
        public List<User> buddies { get; set; }
        public MainWindow()
        {           
            //repository = new Repository();

            //repository.GetUsers(); // выводит только последнего юхзера
            //repository.GetSubjects();
            //repository.GetInterests();
            //int k = 0;
            InitializeComponent();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            repository = new Repository();
            user = repository.Authorization(LoginTextBox.Text, PasswordTextBox.Password);
            
            var choice = new ChoiceWindow(user, repository);
            choice.Show();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var reg = new RegistrationWindow(user, repository);
            reg.Show();
            
        }
    }
}
