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
        List<User> suitablebuddies = new List<User>();
        List<User> friends = new List<User>();
        List<Request> requests = new List<Request>();

        public void GetUsers() // аналогично нужно написать методы для выгрузки предметов (subjects, subsubjects) , и 
        { // сделать, чтобы для каждого юзера подгружался список его интересов.
            
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {

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
                    users.Add(user);
                }
            }
        }

        public void GetSubjects() // здесь надо сделать массив всех существующих предметов
        {

        }
        public void GetSubSubjects() // здесь надо сделать массив всех существующих подпредметов
        {

        }
        public List<Interest> GetNeededSubjectsForUser(User user1)
        { // сделать, чтобы для каждого юзера подгружался список его интересов. НЕ ДОДЕЛАНО!

            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
               
                string queryString = "SELECT * FROM Interests where UserId=\'" + user1.UserId + "\'";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetValue(2).ToString() == "1")
                    {
                        
                    }
                }
            }
            return user1.NeedSubjects;
        }


        public List<Interest> GetCanHelpWithSubjectsForUser(User user1)
        { // сделать, чтобы для каждого юзера подгружался список его интересов. НЕ ДОДЕЛАНО!

            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {

                string queryString = "SELECT * FROM Interests where UserId=\'" + user1.UserId + "\'";
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.GetValue(2).ToString() == "1")
                    {

                    }
                }
            }
            return user1.CanHelpWithSubjects;



            //using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            //{

            //    string query = "SELECT * FROM Interests WHERE UserId=\'" + User.UserId + "\';";
            //    SqlCommand command = new SqlCommand(query, connection);
            //    connection.Open();
            //    SqlDataReader reader = command.ExecuteReader();

            //    while (reader.Read()) // построчно считываем данные, SubjectsID
            //    {
            //        if (reader.GetValue(2).ToString() == "1")
            //        {

            //            interestsIDCanHelp.Add(int.Parse(reader.GetValue(0).ToString()));
            //        }
            //        else
            //        {
            //            interestsIDNeedHelp.Add(int.Parse(reader.GetValue(0).ToString()));
            //        }
            //    }
            //    reader.Close();
            //} это из главного кода, как шаблон
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
            using (SqlConnection connection = new SqlConnection("Data Source = (local)\\SQLEXPRESS; Initial Catalog = UsersDatabaseKDZ; Integrated Security = True; Pooling = False"))
            {
                string listsubjectIds = "(";
                foreach (var item in user.NeedSubjects)
                {
                    listsubjectIds = listsubjectIds + "SubSubjectId=" + item.InterestId + " or ";
                }
                listsubjectIds = listsubjectIds + ")";

                string query = "SELECT Users.UserId, COUNT(*) as NumOfGoodSubjects FROM Interests join Users on Users.UserId = Interests.UserId WHERE Users.UserId !=\'" + user.UserId + " and Relation_Type = 1 and \'" + listsubjectIds + " group by Users.UserId order by NumOfGoodSubjects desc";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foreach (var buddy in users)
                    {
                        if (buddy.UserId == int.Parse(reader.GetValue(0).ToString()))
                        {
                            suitablebuddies.Add(buddy);
                        }
                    }                   
                }
            }
            return suitablebuddies;
        }

        
        private Exception NotImplementedException()
        {
            throw new NotImplementedException();
        }
    }
}