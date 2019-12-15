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

        public void Registration(string name, string login, string password, DateTime birthDate, string? vk, string? telegram)
        {
            int id = users.Count;
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

        }
    }
}