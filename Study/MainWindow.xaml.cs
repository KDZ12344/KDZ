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
using Core;
namespace Study
{
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
                string queryString = "SELECT * FROM Users WHERE Login=\'" + LoginTextBox.Text + "\' and Password=\'" + PasswordTextBox.Password + "\';";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                
                SqlDataReader reader = command.ExecuteReader();
                string t = reader.Read().ToString();
                
                if (t == "False")
                {
                    MessageBox.Show("Oops! There's no user with such login&password.");               
                }
                else
                {

                    User user = new User();                                       
                    Object[] values = new Object[reader.FieldCount];
                    user.UserId = int.Parse(reader.GetValue(7).ToString());
                    user.Login = reader.GetValue(0).ToString();
                    user.VKTG = reader.GetValue(2).ToString();                   
                    user.Name = reader.GetValue(4).ToString();
                    user.Password = reader.GetValue(5).ToString();
                    
                    var myprofile = new MyProfileWindow(user);
                    myprofile.ShowDialog();
                   
                }

            }

        }
    }
}
