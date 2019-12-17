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
    /// Логика взаимодействия для ChoiceWindowxaml.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        public User meUser { get; set; }
        public List<User> Buddies { get; set; }
        //public Repository repos0 { get; set; }
        public ChoiceWindow(User me, Repository repos)
        {
            meUser = me;

            List<User> buddies = repos.GetSuitableBuddies(me);
            Buddies = buddies;
            if (buddies.Count() > 0)
            {
                 
                LoginTextBlock.Text = buddies[0].Login.ToString();
                AgeTextBlock.Text = ((DateTime.Now - buddies[0].BirthDate).TotalDays / 365).ToString();
                CanHelpWithListBox.ItemsSource = buddies[0].CanHelpWithSubjects;
                InitializeComponent();
            }
            else
            {               
                MessageBox.Show("There are no suitable users for you. Please, change your interests or wait.");
                var myprof = new MyProfileWindow(me);
                myprof.Show();
                
            }
           


        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var myProfile = new MyProfileWindow(meUser);
            myProfile.Show();

        }

        private void Button_Next(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Text_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
