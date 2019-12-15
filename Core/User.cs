using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.Core
{ 

   
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public string VKID { get; set; }
        public string TelegramID { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DateAdded { get; set; }        
        public int IndexOfAvatarImage { get; set; }
        public List<Interest> NeedSubjects { get; set; }
        public List<Interest> CanHelpWithSubjects { get; set; }

        public List<User> Friends { get; set; }

        //public User(string name, string login, int id, string password, DateTime birthDate, DateTime dateAdded, string? vk, string? telegram)
        //{
            
        //    Login = login;
        //    UserId = id;
        //    Password = password;
        //    BirthDate = birthDate;
        //    DateAdded = dateAdded;
        //    VKID = vk;
        //    TelegramID = telegram;
        //    Name = name;
        //}
        
    }
}
