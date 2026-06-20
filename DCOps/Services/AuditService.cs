using DCOps.Web.Data;
using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class AuditService : IAuditService
{
    private readonly AppDbContext _db;

    public AuditService(AppDbContext db)
    {
        _db = db;
    }

    public void Log(int? userId, string? username, string action, string entityType, string? entityId, string? details, string? ip)
    {
        _db.AuditLogs.Add(new AuditLog
        {
            UserId = userId,
            Username = username,
            Action = action,
            EntityType = entityType,
            EntityId = entityId,
            Details = details,
            IpAddress = ip,
            CreatedAt = DateTime.UtcNow
        });
        _db.SaveChanges();
    }

    public async Task LogAsync(int? userId, string? username, string action, string entityType, string? entityId, string? details, string? ip)
    {
        _db.AuditLogs.Add(new AuditLog
        {
            UserId = userId,
            Username = username,
            Action = action,
            EntityType = entityType,
            EntityId = entityId,
            Details = details,
            IpAddress = ip,
            CreatedAt = DateTime.UtcNow
        });
        await _db.SaveChangesAsync();
    }
}
