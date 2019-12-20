using Study.Core;
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

namespace Study
{
    /// <summary>
    /// Логика взаимодействия для СhooseNewInterestWindow.xaml
    /// </summary>
    public partial class СhooseNewInterestWindow : Window
    {
        public User User { get; set; }
        public int IndexUser { get; set; }

        public int Flag { get; set; }
        private Repository repository = Factory.Instance.GetRepository();
        public СhooseNewInterestWindow(User user, int flag)
        {
            InitializeComponent();
            User = user;
            IndexUser = repository.Users.IndexOf(User);
            Flag = flag;
            if (flag == 1) // 1 - delete needInterest, 3 - add new needsubject
                comboBoxInterests.ItemsSource = User.NeedSubjects;
            else if (flag == 2) // 2 - delete CanHelp subject, 4 - add new canHelpsubject
                comboBoxInterests.ItemsSource = User.CanHelpWithSubjects;
            else 
            {
                Interest[] interests = repository.Interests.ToArray();
                Interest[] canhelpsubj = User.CanHelpWithSubjects.ToArray();
                Interest[] needsubj = User.NeedSubjects.ToArray();
                var new_list = interests.Except(canhelpsubj);
                new_list = new_list.Except(needsubj);              
                comboBoxInterests.ItemsSource = new_list;
            }
           
                
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxInterests.SelectedItem == null)
            {
                MessageBox.Show("Choose a Subject.");
                return;
            }
            var subject = comboBoxInterests.SelectedItem as Interest;
            if (Flag == 1)
            {
                User.NeedSubjects.Remove(subject);
                repository.Users[IndexUser].NeedSubjects.Remove(subject);
                repository.UpdateDatabase(User);
            }

            else if (Flag == 2)
            {
                User.CanHelpWithSubjects.Remove(subject);
                repository.Users[IndexUser].CanHelpWithSubjects.Remove(subject);
                repository.UpdateDatabase(User);
            }


            else if (Flag == 3)
            {

                User.NeedSubjects.Add(subject);
                repository.Users[IndexUser].NeedSubjects.Add(subject);
                repository.UpdateDatabase(User);
            }
            else if (Flag == 4)
            {
                User.CanHelpWithSubjects.Add(subject);
                repository.Users[IndexUser].CanHelpWithSubjects.Add(subject);
                repository.UpdateDatabase(User);
            }



            var new_red = new RedactProfileWindow(User);
            new_red.Show();
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            var new_red = new RedactProfileWindow(User);
            new_red.Show();
            this.Close();
        }

        private void comboBoxCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
