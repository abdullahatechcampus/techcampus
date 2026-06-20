using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public interface IUserService
{
    User? ValidateUser(string username, string password);
    User? GetUserById(int id);
    List<User> GetAllUsers();
}
