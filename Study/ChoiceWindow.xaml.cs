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
using StudentGrades.Classes;
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
        //public Repository repos0 { get; set; }
        public delegate void MethodContainer();
        public event MethodContainer buttonNextClicked;
        private static Timer aTimer;
        private async void Waiting()
        {           
            await Task.Delay(200);          
        }
        public ChoiceWindow(User me)
        {
            meUser = me;
            Timer timer = new Timer();
            List<User> buddies = repos.GetSuitableBuddies(me);
            Buddies = buddies;
            if (buddies.Count() > 0)
            {
                InitializeComponent();
                foreach (var item in buddies)
                {
                    LoginTextBlock.Text = item.Login;
                    AgeTextBlock.Text = (Math.Round((DateTime.Now - item.BirthDate).TotalDays / 365)).ToString() + " years";
                    CanHelpWithListBox.ItemsSource = item.CanHelpWithSubjects;
                    var uriSource = new Uri(@"/Study;component/" + item.AvatarAdress, UriKind.Relative);
                    AvatarImage.Source = new BitmapImage(uriSource);   
                }
                MessageBox.Show("That's all!", "Want to see buddies once again?", MessageBoxButton.YesNo);
    // сделать чтобы по нажатию next показывался след. юзер
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
            
        
        private void Button_Next_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void Button_Text_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CanHelpWithListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
