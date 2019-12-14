using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{ 

   
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string VKTG { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime DateAdded { get; set; }
        public List<Interest> Interests { get; set; }
        public List<User> Friends { get; set; }

        public User()
        {
        }
        
    }
}
