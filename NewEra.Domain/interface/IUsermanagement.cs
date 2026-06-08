using NewEra.Domain.Models;

namespace NewEra.Domain.Interface;
public interface IUserManagement
{
     void RegisterUser(User user);
     User? GetUser(User user);
}