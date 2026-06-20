using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace DCOps.Web.Services;

public class AuditActionFilter : IActionFilter
{
    private readonly IAuditService _auditService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditActionFilter(IAuditService auditService, IHttpContextAccessor httpContextAccessor)
    {
        _auditService = auditService;
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Log on POST actions only to avoid noise
        if (context.HttpContext.Request.Method == "POST")
        {
            var user = context.HttpContext.User;
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = user.Identity?.Name;
            int? userId = int.TryParse(userIdClaim, out var uid) ? uid : null;

            var routeData = context.RouteData;
            var controller = routeData.Values["controller"]?.ToString() ?? "";
            var action = routeData.Values["action"]?.ToString() ?? "";
            var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            try
            {
                _auditService.Log(userId, username, $"{controller}/{action}", controller, null, null, ip);
            }
            catch
            {
                // Swallow audit errors - don't break the request
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // No-op
    }
}
