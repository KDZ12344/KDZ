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
namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
    
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string queryString = "SELECT login FROM Users WHERE Login=\'" + LoginTextBox.Text + "\' and Password=\'" + PasswordTextBox.Password + "\';";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader == "")
                {
                    MessageBox.Show("Oops! There's no user with such login&password.");
                    queryString = "SELECT * FROM Users WHERE Login=\'" + LoginTextBox.Text + "\' and Password=\'" + PasswordTextBox.Password + "\';";
                    command = new SqlCommand(queryString, connection);
                    connection.Open();
                    reader = command.ExecuteReader();
                }
                reader.Close();

            }
            var choic = new ChoiceWindow();
            choic.ShowDialog();

        }
    }
}
