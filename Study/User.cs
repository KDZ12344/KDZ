using System;
public class User
{
    public string Name { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime DateAdded { get; set; }
    public List<Interest> Interests { get; set; }
    public List<User> Friends { get; set; }

    public User()
    {
    }
}

