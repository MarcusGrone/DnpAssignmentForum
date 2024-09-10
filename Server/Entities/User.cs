namespace Entities;

public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }

    public User(int userId, string userName, string password)
    {
        UserId = userId;
        UserName = userName;
        Password = password;
    }
}