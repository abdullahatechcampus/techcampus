namespace DCOps.Web.Services;

public interface IAuditService
{
    void Log(int? userId, string? username, string action, string entityType, string? entityId, string? details, string? ip);
    Task LogAsync(int? userId, string? username, string action, string entityType, string? entityId, string? details, string? ip);
}
