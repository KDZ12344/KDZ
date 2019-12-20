using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using Study;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.IO;

namespace Study.Core
{
    public class Repository
    {
        public List<User> Users { get; set; }
        public User user00 { get; set; }
        public List<Interest> Interests { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Request> Requests { get; set; }

        List<User> users = new List<User>();
        List<Subject> subjects = new List<Subject>();
        List<Interest> interests = new List<Interest>();
        List<User> suitablebuddies = new List<User>();
        List<User> friends = new List<User>();
        List<Request> requests = new List<Request>();
        public Repository()
        {
            
            GetSubjects();
            GetInterests();
            GetUsers();
            GetFriends();
            GetRequests();
            Users = users;
            Interests = interests;
            Subjects = subjects;
            Requests = requests;
        }

        private void GetRequests()
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {               
                string queryString = "SELECT * FROM Requests";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Request req = new Request
                    {
                        Sender = GetUserById(int.Parse(reader.GetValue(0).ToString())),
                        Receiver = GetUserById(int.Parse(reader.GetValue(1).ToString()))
                    };
                    requests.Add(req);
                }
            }
        }

        public void GetUsers() 
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                users.Clear();
                int Id = 0;
                string queryString = "SELECT * FROM Users";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                   
                    User user = new User
                    {
                        Name = reader.GetValue(4).ToString().Trim(),
                        Login = reader.GetValue(0).ToString().Trim(),
                        UserId = int.Parse(reader.GetValue(8).ToString()),
                        Password = reader.GetValue(5).ToString().Trim(),
                        BirthDate = DateTime.Parse(reader.GetValue(6).ToString()),
                        DateAdded = DateTime.Parse(reader.GetValue(7).ToString()),
                        VKID = reader.GetValue(3).ToString().Trim(),
                        TelegramID = reader.GetValue(2).ToString().Trim(),
                        AvatarAdress = reader.GetValue(1).ToString(), 
                        Bio = reader.GetValue(9).ToString().Trim(),
                        Major = reader.GetValue(10).ToString().Trim()
                    };
                    user00 = user;
                    Id = user.UserId;
                    users.Add(user);

                }
                reader.Close();
                for (int i = 1; i < users.Count()+1; i++)
                {
                    string queryString1 = "SELECT * FROM Users join Interests on Users.Userid = Interests.Userid where Users.UserId="+i;
                    SqlCommand command1 = new SqlCommand(queryString1, connection);
                    SqlDataReader reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        foreach (var subsubject in interests)
                        {
                            int u = int.Parse(reader1.GetValue(11).ToString());
                            string uu = reader1.GetValue(13).ToString();
                            if (subsubject.InterestId == int.Parse(reader1.GetValue(11).ToString()) && reader1.GetValue(13).ToString() == "True")
                            {
                                users[i-1].CanHelpWithSubjects.Add(subsubject);
                            }
                            if (subsubject.InterestId == int.Parse(reader1.GetValue(11).ToString()) && reader1.GetValue(13).ToString() == "False")
                            {
                                users[i-1].NeedSubjects.Add(subsubject);
                            }
                        }

                    }
                    reader1.Close();
                }
                
                
            }
        }

        public void RemoveInterestFromDb(Interest interest, User user)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "delete from Interests where SubSubjectId =\'" + interest.InterestId + "'and " + "UserId=\'" + user.UserId + "'";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();

                // + add to /remove from Interests 

            }
        }

        public void AddInterestToDb(Interest interest, User user, int Rel)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "insert into Interests values(\'" + interest.InterestId +"'," + user.UserId + ","+Rel+")";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();

                // + add to /remove from Interests 

            }
        }
        public void UpdateDatabase(User user)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "update Users set Login =\'" + user.Login + "', " + "Avatar=\'" + user.AvatarAdress + "', " + " TelegramID=\'" + user.TelegramID + "', " + " VKID=\'" + user.VKID + "', " + " Name=\'" + user.Name + "', " +
                   "Password=\'" + user.Password + "', " + " BirthDate=" + user.BirthDate.ToShortDateString().Replace(".", "-") + ", " + " Bio=\'" + user.Bio + "', " + " Major=\'" + user.Major + "' where UserID=" + user.UserId;
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();

                // + add to /remove from Interests 

            }
        }

        public void GetSubjects() // ����� ���� ������� ������ ���� ������������ ���������
        {
            subjects.Clear();
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string queryString = "SELECT * FROM MainSubjects";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Subject sub = new Subject
                    {
                        Name = reader.GetValue(0).ToString()
                    };
                    subjects.Add(sub);
                }
            }
        }
        public void GetInterests() // ����� ���� ������� ������ ���� ������������ ������������
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string queryString = "SELECT * FROM SubSubjects";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Interest interest = new Interest
                    {
                        InterestId = int.Parse(reader.GetValue(2).ToString()),
                        InterestName = reader.GetValue(1).ToString(),                        
                    };
                    foreach (var item in subjects)
                    {
                        if (item.Name == reader.GetValue(0).ToString())
                        {
                            interest.Subject = item;
                        }
                    }                   
                    interests.Add(interest);
                }
            }
        }
        public void SavingToDatabase(string login, string telegram, string vk, string name, string password, DateTime birthDate, DateTime dateAdded, string avatar, string bio, string major)
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

                string tt = birthDate.ToShortDateString().Replace('.', '-');
                string query = "insert into users(login, telegramid, vkid, name, password, birthdate, datewhenadded, avatar, bio, major) values(\'" + login + "\',\'" + telegram + "\',\'" + vk + "\',\'" + name + "\',\'" + password + "\',\'" + birthDate + "\',\'" +
                     dateAdded + "\',\'" + avatar + "\',\'" + bio + "\',\'" + major + "');";
                //string query = "insert into users(login, telegramid, vkid, name, password, birthdate, datewhenadded, avatar, bio, major, userid) values('"+ login +"','" + telegram + "','" + vk + "','" + name + "','" + password + "','"
                //    + birthDate.ToShortDateString().Replace('.', '-') +
                //     "," + dateAdded.ToShortDateString().Replace('.', '-') + ",'"+avatar+"','" + bio + "','" + major+"')";
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                
                cmd.ExecuteNonQuery();
               
            }
        }

        public User Registration(string name, string login, string password, DateTime birthDate, string vk, string telegram, string avatar, string bio, string major)
        {           
            User user = new User
            {
                Name = name,
                Login = login,
                UserId = users.Count() + 1,
                Password = password,
                BirthDate = birthDate,
                DateAdded = DateTime.Now,
                VKID = vk,
                TelegramID = telegram,
                AvatarAdress = avatar, 
                Bio = bio, 
                Major = major
                           
            };
            users.Add(user);
            
            return user;

        }

        public User GetUserById(int Id)
        {
            foreach (var item in users)
            {
                if (item.UserId == Id)
                {
                    return item;
                }
            }
            return null;
        }
        public void GetFriends()
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string queryString = "SELECT * FROM Friend";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int SenderId = int.Parse(reader.GetValue(0).ToString());
                    int ReceiverId = int.Parse(reader.GetValue(1).ToString());
                    
                    foreach (var item in users)
                    {
                        if (item.UserId == ReceiverId)
                        {  
                            item.Friends.Add(GetUserById(SenderId));
                        }
                        if (item.UserId == SenderId)
                        {
                            item.Friends.Add(GetUserById(ReceiverId));
                        }               
                    }
                }
            }
        }
   
        public void FriendRequest(User sender, User receiver)
        {
            var request = new Request
            {
                Sender = sender,
                Receiver = receiver        
            };
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "SELECT * FROM Requests WHERE Sender=" + sender.UserId + "and Receiver=" + receiver.UserId;
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    requests.Add(request);
                    string queryString = "insert into Requests values(" + sender.UserId + "," + receiver.UserId + ")";
                    SqlCommand command1 = new SqlCommand(queryString, connection);
                    command1.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Request had already been sent!");
                }
            }
        }

        public User Authorization(string logtb, string passtb)
        {
            foreach (var user in users)
            {
                if (user.Login == logtb && user.Password == passtb)
                {
                    return user;
                }
            }
            MessageBox.Show("Oops! There's no user with such login & password.");
            return null;
            
        }
        public bool IsInRequests(User sender, User receiver)
        {
            foreach (var req in requests)
            {
                if (req.Sender == sender && req.Receiver == receiver)
                {
                    return false;
                }
            }
            return true;
        }
        public List<User> GetSuitableBuddies(User user) // ������� ���� ����� �� ���� ������, ���������� �� ���������, ����������� �� ���������� ���������� ���������
        {
            
            suitablebuddies = new List<User>();

            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {

                string listsubjectIds = "(";
                string listfriends = "(";
                //string listrequest
                if (user.NeedSubjects.Count() > 0)
                {
                    foreach (var item in user.NeedSubjects)
                    {
                        listsubjectIds = listsubjectIds + "SubSubjectId=" + item.InterestId + " or ";
                    }
                    foreach (var friend in user.Friends)
                    {
                        listfriends = listfriends + "Users.UserId!=" + friend.UserId + " and ";
                    }
                    listfriends = listfriends + ")";
                    listfriends = listfriends.Replace(" and )", ")");
                    if (listfriends == "()")
                    {
                        listfriends = "";
                    }
                    listsubjectIds = listsubjectIds + ")";
                    listsubjectIds = listsubjectIds.Replace(" or )", ")");
                    if (listsubjectIds == "()")
                    {
                        listsubjectIds = "";
                    }
                    string query = "SELECT Users.UserId, COUNT(*) as NumOfGoodSubjects FROM Interests join Users on Users.UserId = Interests.UserId WHERE Users.UserId != " + user.UserId + " and "+listfriends+ " and Relation_Type = 1 and " + listsubjectIds + " group by Users.UserId order by NumOfGoodSubjects desc";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foreach (var buddy in users)
                        {
                            
                            if (buddy.UserId == int.Parse(reader.GetValue(0).ToString()))
                            {
                                if (!IsInRequests(user, buddy))
                                {
                                    suitablebuddies.Add(buddy);
                                }
                                
                            }
                        }
                    }
                }

                return suitablebuddies;
            }

        }

        //public BitmapImage ImageUploading(User user)
        //{
        //    OpenFileDialog open = new OpenFileDialog();
        //    if (open.ShowDialog() == true)
        //    {
        //        Uri openUri = new Uri(open.FileName);
        //        var toSave = DateTime.Now.ToString() + Path.GetExtension(open.FileName);
        //        var imagePath = Path.Combine("C:\" + toSave);
        //        user.AvatarAdress = open.FileName;
        //        BitmapImage image = new BitmapImage(openUri);
        //        return image;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please, choose an image.");
        //        return null;
        //    }
        //}
        

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}