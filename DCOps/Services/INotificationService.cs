using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public interface INotificationService
{
    int GetUnreadCount(int userId);
    List<Notification> GetNotifications(int userId);
    void MarkAsRead(int id);
    void Create(Notification notification);
}
