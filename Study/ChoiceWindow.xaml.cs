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
        public List<User> buddies { get; set; }
        //public Repository repos0 { get; set; }
        public ChoiceWindow(User me, Repository repos)
        {
            meUser = me;
            buddies = repos.GetSuitableBuddies(me);
            LoginTextBlock.Text = buddies[0].Login;
            AgeTextBlock.Text = ((DateTime.Now - buddies[0].BirthDate).TotalDays / 365).ToString();
            CanHelpWithListBox.ItemsSource = buddies[0].CanHelpWithSubjects;
            InitializeComponent();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var myProfile = new MyProfileWindow(meUser);
            myProfile.Show();

        }
    }
}
