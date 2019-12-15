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

        List<User> users = new List<User>();

        public void Registration(string name, string login, string password, DateTime birthDate, string vk, string telegram)
        {
            int id = users.Count + 1;
            User user = new User
            {
                Name = name,
                Login = login,
                UserId = id,
                Password = password,
                BirthDate = birthDate,
                DateAdded = DateTime.Now,
                VKID = vk,
                TelegramID = telegram

            };
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
                    + "\',\'" + user.DateAdded + "\');";
                cmd.Connection = connection;

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
           }
        }
    }
}