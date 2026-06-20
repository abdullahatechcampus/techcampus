using DCOps.Web.Data;
using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _db;

    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public User? ValidateUser(string username, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Username == username && u.IsActive);
        if (user == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
        user.LastLogin = DateTime.UtcNow;
        _db.SaveChanges();
        return user;
    }

    public User? GetUserById(int id)
    {
        return _db.Users.FirstOrDefault(u => u.Id == id);
    }

    public List<User> GetAllUsers()
    {
        return _db.Users.OrderBy(u => u.FullName).ToList();
    }
}
