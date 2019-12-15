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
    /// Логика взаимодействия для RedactProfileWindow.xaml
    /// </summary>
    public partial class RedactProfileWindow : Window
    {
        public RedactProfileWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string.IsNullOrWhiteSpace(NameTextBlock.Text)))
            {
                MessageBox.Show("У вас должно быть имя!!!");
            }
            else if (!(string.IsNullOrWhiteSpace(NameTextBlock.Text)))
            {
                
            }
            else if (!(string.IsNullOrWhiteSpace(LoginTextBlock.Text)))
            {

            }
            else if (!(string.IsNullOrWhiteSpace(VKTextBlock.Text)))
            {

            }
        }
        private bool TryDeleteSelectedGrade(out Interest interest, ListBox listBox)
        {
            interest = listBox.SelectedItem as Interest;
            if (interest == null)
            {
                MessageBox.Show("Select an item from the table");
                return false;
            }
            return true;
        }

        private void DeleteCanHelpItem(object sender, RoutedEventArgs e)
        {
            if (!TryDeleteSelectedGrade(out var interest, ListCanHelpWith)) return;

            ListCanHelpWith.Items.Remove(interest);
        }

        private void DeleteNeedHelpItem(object sender, RoutedEventArgs e)
        {
            if (!TryDeleteSelectedGrade(out var interest, ListNeedHelpWith)) return;

            ListNeedHelpWith.Items.Remove(interest);

        }

        private void AddCanHelpItem(object sender, RoutedEventArgs e)
        {

        }

        private void AddNeedHelpItem(object sender, RoutedEventArgs e)
        {

        }
    }
}
