using NewEra.Domain.Interface;
using NewEra.Domain.Models;


namespace NewEra.BLL;
public class LoginService
{
    private readonly IUserManagement _userManagement;

    public LoginService(IUserManagement userManagement)
    {
        _userManagement = userManagement;
    }

public User? Login(string email, string password)
    {
        var user = new User { Email = email, Password = password };
        var existingUser = _userManagement.GetUser(user);

        if (existingUser == null || existingUser.Password != password)
        {
            return null; // Login failed
        }
        return existingUser; // Return user with Id
    }

    public void Register(string username, string password, string email, string role)
    {
        var user = new User { Username = username, Password = password, Email = email, Role = role };
        _userManagement.RegisterUser(user);
    }

}