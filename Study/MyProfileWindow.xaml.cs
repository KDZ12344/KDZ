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
using System.Data.SqlClient;
using Study.Core;
using System.IO;
using System.Resources;
namespace Study
{
    /// <summary>
    /// Логика взаимодействия для MyProfileWindow.xaml
    /// </summary>
    public partial class MyProfileWindow : Window
    {
        User user1 = new User();

        public MyProfileWindow(User user)
        {
            InitializeComponent();
            
            var uriSource = new Uri(@"/Study;component/"+ user.AvatarAdress, UriKind.Relative);
            AvatarImage.Source = new BitmapImage(uriSource);
            
            NameTextBlock.Text = user.Name;
            VKTextBlock.Text = user.VKID;
            TGTextBlock.Text = user.TelegramID;
            LoginTextBlock.Text = user.Login;
            List<Interest> interestsNeedHelp = new List<Interest>();
            List<Interest> interestsCanHelp = new List<Interest>();
            List<string> NeedHelp = new List<string>();
            List<string> CanHelp = new List<string>();
            List<int> interestsIDNeedHelp = new List<int>();
            List<int> interestsIDCanHelp = new List<int>();
            List<User> friends = new List<User>();

            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "SELECT * FROM Interests WHERE UserId=\'" + user.UserId + "\';";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read()) // построчно считываем данные, SubjectsID
                {
                    if (reader.GetValue(2).ToString() == "1")
                    {
                        interestsIDCanHelp.Add(int.Parse(reader.GetValue(0).ToString()));
                    }
                    else
                    {
                        interestsIDNeedHelp.Add(int.Parse(reader.GetValue(0).ToString()));
                    }
                }
                reader.Close();
            }
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "";
                foreach (var interest in interestsIDCanHelp)
                {
                    query = "SELECT * FROM SubSubjects WHERE IdSubSubject=\'" + interest + "\';";
                    SqlCommand command = new SqlCommand(query, connection);
                    command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) // построчно считываем данные
                    {
                        Interest inter = new Interest();
                        inter.InterestId = int.Parse(reader.GetValue(2).ToString());
                        inter.InterestName = reader.GetValue(1).ToString();
                        inter.SubjectName = reader.GetValue(0).ToString();
                        interestsCanHelp.Add(inter);
                    }
                    reader.Close();
                    connection.Close();
                }

                foreach (var interest in interestsIDNeedHelp)
                {
                    query = "SELECT * FROM SubSubjects WHERE IdSubSubject=\'" + interest + "\';";
                    SqlCommand command = new SqlCommand(query, connection);
                    command = new SqlCommand(query, connection);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read()) // построчно считываем данные
                    {
                        Interest inter = new Interest();
                        inter.InterestId = int.Parse(reader.GetValue(2).ToString());
                        inter.InterestName = reader.GetValue(1).ToString();
                        inter.SubjectName = reader.GetValue(0).ToString();
                        interestsNeedHelp.Add(inter);
                    }
                    reader.Close();
                    connection.Close();
                }
                foreach (var item in interestsCanHelp)
                {
                    CanHelp.Add(item.InterestName.Trim() + " " + item.SubjectName);
                }
                foreach (var item in interestsNeedHelp)
                {
                    NeedHelp.Add(item.InterestName.Trim() + " " + item.SubjectName);
                }
                ListCanHelpWith.ItemsSource = CanHelp;
                ListNeedHelpWith.ItemsSource = NeedHelp;
                
            }
            user1 = user;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var redactionWindow = new RedactProfileWindow(user1);
            redactionWindow.ShowDialog();
        }
    }
}
