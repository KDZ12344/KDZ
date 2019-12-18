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

        public int Flag { get; set; }
        private Repository repository = Factory.Instance.GetRepository();
        public СhooseNewInterestWindow(User user, int flag)
        {
            InitializeComponent();
            User = user;
            Flag = flag;
            if (flag == 1) // 1 - delete needInterest, 3 - add new needsubject
                comboBoxInterests.ItemsSource = User.NeedSubjects;
            else if (flag == 2) // 2 - delete CanHelp subject, 4 - add new canHelpsubject
                comboBoxInterests.ItemsSource = User.CanHelpWithSubjects;
            else
                comboBoxInterests.ItemsSource = repository.Interests;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxInterests.SelectedItem == null)
            {
                MessageBox.Show("Choose Subject!!!");
                return;
            }
            var subject = comboBoxInterests.SelectedItem as Interest;
            if (Flag == 1)
                User.NeedSubjects.Remove(subject);

            else if (Flag == 2)
                User.CanHelpWithSubjects.Remove(subject);


            else if (Flag == 3)
                User.NeedSubjects.Add(subject);
            else if (Flag == 4)
                User.CanHelpWithSubjects.Add(subject);

            repository.Users[User.UserId] = User;
            repository.UpdateDatabase(User);
            DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void comboBoxCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
