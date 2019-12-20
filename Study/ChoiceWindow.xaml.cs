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
using System.Timers;

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для ChoiceWindowxaml.xaml
    /// </summary>
    public partial class ChoiceWindow : Window
    {
        public User meUser { get; set; }
        public List<User> Buddies { get; set; }
        Repository repos = Factory.Instance.GetRepository();
        public delegate void MethodContainer();
        int i;
        User curBuddy = new User();
        public ChoiceWindow(User me)
        {
            meUser = me;
            List<User> buddies = repos.GetSuitableBuddies(me);
            Buddies = buddies;
            
            if (buddies.Count() > 0)
            {
                InitializeComponent();
                i = buddies.Count();
                
                UserControl1(buddies[0]);
                curBuddy = buddies[0];
                
    // сделать чтобы по нажатию next показывался след. юзер
            }
            else
            {               
                MessageBox.Show("There are no suitable users for you. Please, change your interests or wait.");
                var myprof = new RedactProfileWindow(me);
                myprof.Show();   
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var myProfile = new RedactProfileWindow(meUser);
            myProfile.Show();
        }

        int k = 1;
        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
            if (k < i)
            {
                UserControl1(Buddies[k]);
            }
            else

            {
                MessageBox.Show("That's all!");
                this.Close();
            }
            k += 1;            
        }

        private void UserControl1(User item)
        {
            NameTextBlock.Text = item.Name;
            MajorTextBlock.Text = item.Major;
            BioTextBlock.Text = item.Bio;
            //LoginTextBlock.Text = item.Login;
            //AgeTextBlock.Text = (Math.Round((DateTime.Now - item.BirthDate).TotalDays / 365)).ToString() + " years";
            CanHelpWithListBox.ItemsSource = item.CanHelpWithSubjects;
            var uriSource = new Uri(@"/Study;component/" + item.AvatarAdress, UriKind.Relative);
            avatarImage.Source = new BitmapImage(uriSource);
            curBuddy = item;
        }

        private void Button_Text_Click(object sender, RoutedEventArgs e)
        {
            repos.FriendRequest(meUser, curBuddy);
            MessageBox.Show("Your request has been sent!");
            Buddies.Remove(curBuddy);
            if (Buddies.Count() > 0)
            {
                UserControl1(Buddies[0]);
            }
            else
            {
                this.Close();
            }
            
        }

        private void CanHelpWithListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }

   
}
