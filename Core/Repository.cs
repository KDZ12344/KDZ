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
                        Name = reader.GetValue(4).ToString(),
                        Login = reader.GetValue(0).ToString(),
                        UserId = int.Parse(reader.GetValue(8).ToString()),
                        Password = reader.GetValue(5).ToString(),
                        BirthDate = DateTime.Parse(reader.GetValue(6).ToString()),
                        DateAdded = DateTime.Parse(reader.GetValue(7).ToString()),
                        VKID = reader.GetValue(3).ToString(),
                        TelegramID = reader.GetValue(2).ToString()
                    };
                    user00 = user;
                    Id = user.UserId;
                    users.Add(user);

                }
                reader.Close();
                

                string queryString1 = "SELECT * FROM Users join Interests on Users.Userid = Interests.Userid where Users.UserId=\'" + Id + "\';";
                SqlCommand command1 = new SqlCommand(queryString1, connection);
                SqlDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    foreach (var subsubject in Interests)
                    {
                        if (subsubject.InterestId == int.Parse(reader.GetValue(0).ToString()) && reader.GetValue(2).ToString() == "1")
                        {
                            user00.CanHelpWithSubjects.Add(subsubject);
                        }
                        if (subsubject.InterestId == int.Parse(reader.GetValue(0).ToString()) && reader.GetValue(2).ToString() == "0")
                        {
                            user00.NeedSubjects.Add(subsubject);
                        }
                    }
                
                }
                
            }
        }
        // написать метод для вывода списка с canHelpwith
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
                            interest.SubjectName = item.Name;
                        }
                    }                   
                    interests.Add(interest);
                }
            }
        }

        public List<Interest> GetNeededSubjectsForUser(User user1)
        {

            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {

                string queryString = "SELECT * FROM Users join Interests on Users.Userid = Interests.Userid where Users.UserId=\'" + user1.UserId + "\' and Interests.Relation_Type = 0;";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {                   
                    foreach (var subsubject in interests)// interests == null
                    {
                        if (subsubject.InterestId == int.Parse(reader.GetValue(9).ToString())) 
                        {
                            user1.NeedSubjects.Add(subsubject);
                        }
                    }
                }

                return user1.NeedSubjects;
            }
        }


        public List<Interest> GetCanHelpWithSubjectsForUser(User user1)
        { // сделать, чтобы для каждого юзера подгружался список его интересов. НЕ ДОДЕЛАНО!

           
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {

                string queryString = "SELECT * FROM Users join Interests on Users.Userid = Interests.Userid where Users.UserId=\'" + user1.UserId + "\' and Interests.Relation_Type = 1;";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foreach (var subsubject in interests)// interests == null
                    {
                        if (subsubject.InterestId == int.Parse(reader.GetValue(9).ToString()))
                        {
                            user1.CanHelpWithSubjects.Add(subsubject);
                        }
                    }
                }

                return user1.CanHelpWithSubjects;
            }
        }



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
                cmd.CommandText = "insert into users(login, telegramid, vkid, name, password, birthdate, datewhenadded) values(\'" + login + "\',\'" + telegram + "\',\'" + vk + "\',\'" + name + "\',\'" + password + "\',\'" + birthDate
                    + "\',\'" + dateAdded + "\');";
                cmd.Connection = connection;
                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public User Registration(string name, string login, string password, DateTime birthDate, string vk, string telegram)
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
        

        public User Authorization(string logtb, string passtb)
        {
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string queryString = "SELECT * FROM Users WHERE Login=\'" + logtb + "\' and Password=\'" + passtb + "\';";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                string t = reader.Read().ToString();

                if (t == "False")
                {
                    MessageBox.Show("Oops! There's no user with such login&password.");
                    throw NotImplementedException();
                }
                else
                {
                    User user = new User();
                    Object[] values = new Object[reader.FieldCount];
                    user.UserId = int.Parse(reader.GetValue(8).ToString());

                    user.Login = reader.GetValue(0).ToString();
                    user.TelegramID = reader.GetValue(2).ToString();
                    user.VKID = reader.GetValue(3).ToString();
                    user.Name = reader.GetValue(4).ToString();
                    user.Password = reader.GetValue(5).ToString();
                    user.BirthDate = DateTime.Parse(reader.GetValue(6).ToString());
                    user.DateAdded = DateTime.Parse(reader.GetValue(7).ToString());
                    return user;
                }
                

            }
        }

        public List<User> GetSuitableBuddies(User user) // выбрать всех людей из базы данных, подходящих по предметам, ранжировать по количеству подходящих предметов
        {
            user.NeedSubjects = GetNeededSubjectsForUser(user);
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
                                buddy.CanHelpWithSubjects = GetCanHelpWithSubjectsForUser(user);
                                suitablebuddies.Add(buddy);
                            }
                        }
                    }
                }
                
                return suitablebuddies;
            }
                
        }
        public bool ChangeUserProfile(User user)
        { // сначала меняю данные юзера в репозитории
            var user1 = users.FirstOrDefault(useritem => useritem.UserId == user.UserId);
            if (user1 != null)
            {
                var index = users.IndexOf(user1);
                users[index] = user;
            }
            else
                MessageBox.Show("Данный юзер не найден");
            var flac = false;
            // Доделать:
            // найти в базе данных юзера с ай ди , равным user.Id
            // заменить все поля найденного юзера на поля user, не изменяя id
            MessageBox.Show("нужно дополнить метод UserChangedProfile в классе repository");
            flac = true;
            return flac;
        }


        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}