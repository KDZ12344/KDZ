using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using Study;
namespace Study.Core
{
    public class Repository
    {
        public List<User> Users { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Request> Requests { get; set; }

        List<User> users = new List<User>();
        List<User> friends = new List<User>();
        List<Request> requests = new List<Request>();

        public void SavingToDatabase(string login, string telegram, string vk, string name, string password, DateTime birthDate, DateTime dateAdded)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SET IDENTITY_INSERT users ON ";
                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into users(login, telegramid, vkid, name, password, birthdate, datewhenadded)" +
                    " values(\'" + login + "\',\'" + telegram + "\',\'" + vk + "\',\'" + name + "\',\'" + password + "\',\'" + birthDate
                    + "\',\'" + dateAdded + "\');";
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public User Registration(string name, string login, string password, DateTime birthDate, string vk, string telegram)
        {
            int id = users.Count + 1;
            User user = new User
            {
                Name = name,
                Login = login,
                UserId = id + 1,
                Password = password,
                BirthDate = birthDate,
                DateAdded = DateTime.Now,
                VKID = vk,
                TelegramID = telegram
                
                
            };
            users.Add(user);
            SavingToDatabase(login, telegram, vk, name, password, birthDate, DateTime.Now);
            return user;

        }

        public void FriendRequest(User sender, User receiver)
        {
            var request = new Request
            {
                Sender = sender,
                Receiver = receiver
            };
            requests.Add(request);
        } 
        
    }
}