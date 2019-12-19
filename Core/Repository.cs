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
            Users = users;
            Interests = interests;
            Subjects = subjects;
            Requests = requests;
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
                        AvatarAdress = reader.GetValue(1).ToString()
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

        public void UpdateDatabase(User user)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string query = "insert into Users values("+user.Login+","+user.AvatarAdress+","+user.TelegramID+","+
                user.VKID+","+user.Name+","+user.Password+","+user.BirthDate+","+user.DateAdded+","+user.UserId+","+user.Bio+","+user.Major+");";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                cmd.ExecuteNonQuery();

                // + add to /remove from Interests 

            }
        }

        public void GetSubjects() // здесь надо сделать массив всех существующих предметов
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
        public void GetInterests() // здесь надо сделать массив всех существующих подпредметов
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
        public void SavingToDatabase(string login, string telegram, string vk, string name, string password, DateTime birthDate, DateTime dateAdded, string avatar)
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
                cmd.CommandText = "insert into users(login, telegramid, vkid, name, password, birthdate, datewhenadded, avatar) values(\'" + login + "\',\'" + telegram + "\',\'" + vk + "\',\'" + name + "\',\'" + password + "\',\'" + birthDate
                    + "," + dateAdded + ","+avatar+");";
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public User Registration(string name, string login, string password, DateTime birthDate, string vk, string telegram, string avatar)
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
                AvatarAdress = avatar
                           
            };
            users.Add(user);
            SavingToDatabase(login, telegram, vk, name, password, birthDate, DateTime.Now, avatar);
            return user;

        }

        public void FriendRequest(User sender, User receiver)
        {
            var request = new Request
            {
                Sender = sender,
                Receiver = receiver,
                Status = true
            };
            requests.Add(request);
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string queryString = "insert into Friends values(" + sender.UserId + "," + receiver.UserId + "," + 1;
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
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

        public List<User> GetSuitableBuddies(User user) // выбрать всех людей из базы данных, подходящих по предметам, ранжировать по количеству подходящих предметов
        {
            
            suitablebuddies = new List<User>();
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {

                string listsubjectIds = "(";
                if (user.NeedSubjects.Count() > 0)
                {
                    foreach (var item in user.NeedSubjects)
                    {
                        listsubjectIds = listsubjectIds + "SubSubjectId=" + item.InterestId + " or ";
                    }

                    listsubjectIds = listsubjectIds + ")";
                    listsubjectIds = listsubjectIds.Replace(" or )", ")");

                    string query = "SELECT Users.UserId, COUNT(*) as NumOfGoodSubjects FROM Interests join Users on Users.UserId = Interests.UserId WHERE Users.UserId != " + user.UserId + " and Relation_Type = 1 and " + listsubjectIds + " group by Users.UserId order by NumOfGoodSubjects desc";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foreach (var buddy in users)
                        {
                            if (buddy.UserId == int.Parse(reader.GetValue(0).ToString()))

                            {
                                //buddy.CanHelpWithSubjects = GetCanHelpWithSubjectsForUser(user);
                                suitablebuddies.Add(buddy);
                            }
                        }
                    }
                }

                return suitablebuddies;
            }

        }

        public BitmapImage ImageUploading(User user)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                Uri openUri = new Uri(open.FileName);
                var toSave = DateTime.Now.ToString() + Path.GetExtension(open.FileName);
                var imagePath = Path.Combine("C:\"" + toSave);
                user.AvatarAdress = open.FileName;
                BitmapImage image = new BitmapImage(openUri);
                return image;
            }
            else
            {
                MessageBox.Show("Please, choose an image.");
                return null;
            }
        }
        //public bool ChangeUserProfile(User user)
        //{ // ??????? ????? ?????? ????? ? ???????????
        //    var user1 = users.FirstOrDefault(useritem => useritem.UserId == user.UserId);
        //    if (user1 != null)
        //    {
        //        var index = users.IndexOf(user1);
        //        users[index] = user;
        //    }
        //    else
        //        MessageBox.Show("Данный юзер не найден");
        //    var flag = false;
        //    // ????????:
        //    // ????? ? ???? ?????? ????? ? user.Id
        //    // ???????? ??? ???? ?????????? ?????, ?? ??????? id
        //    MessageBox.Show("нужно дополнить метод UserChangedProfile в классе repository");
        //    flag = true;
        //    return flag;
        //}

        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}