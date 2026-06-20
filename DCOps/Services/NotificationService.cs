using DCOps.Web.Data;
using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class NotificationService : INotificationService
{
    private readonly AppDbContext _db;

    public NotificationService(AppDbContext db)
    {
        _db = db;
    }

    public int GetUnreadCount(int userId)
    {
        return _db.Notifications.Count(n => n.UserId == userId && !n.IsRead);
    }

    public List<Notification> GetNotifications(int userId)
    {
        return _db.Notifications
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .Take(50)
            .ToList();
    }

    public void MarkAsRead(int id)
    {
        var notification = _db.Notifications.FirstOrDefault(n => n.Id == id);
        if (notification != null)
        {
            notification.IsRead = true;
            notification.ReadAt = DateTime.UtcNow;
            _db.SaveChanges();
        }
    }

    public void Create(Notification notification)
    {
        notification.CreatedAt = DateTime.UtcNow;
        _db.Notifications.Add(notification);
        _db.SaveChanges();
    }
}
