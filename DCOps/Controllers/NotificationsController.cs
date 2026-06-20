using DCOps.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DCOps.Web.Controllers;

[Authorize]
public class NotificationsController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public IActionResult Index()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out var userId))
            return RedirectToAction("Login", "Account");

        var notifications = _notificationService.GetNotifications(userId);
        return View(notifications);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult MarkRead(int id)
    {
        _notificationService.MarkAsRead(id);
        return RedirectToAction(nameof(Index));
    }
}
