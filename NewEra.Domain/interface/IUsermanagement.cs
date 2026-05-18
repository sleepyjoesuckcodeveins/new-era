using NewEra.Domain.Models;

namespace NewEra.Domain.Interface;
public interface IUserManagement
{
    public void RegisterUser(User user);
    public User? GetUserById(int id);
    public User? GetUserByUsername(string username);
    public void UpdateUser(User user);
    public void DeleteUser(int id);
}