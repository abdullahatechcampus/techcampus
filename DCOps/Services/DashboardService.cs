using DCOps.Web.Data;

namespace DCOps.Web.Services;

public class DashboardService : IDashboardService
{
    private readonly AppDbContext _db;

    public DashboardService(AppDbContext db)
    {
        _db = db;
    }

    public DashboardStats GetDashboardStats()
    {
        return new DashboardStats
        {
            TotalAssets = _db.Assets.Count(),
            OpenWorkOrders = _db.WorkOrders.Count(w => w.Status == "open" || w.Status == "in_progress"),
            ActiveAlerts = _db.Notifications.Count(n => !n.IsRead && n.Severity == "critical"),
            PendingApprovals = _db.AutomationApprovals.Count(a => a.Status == "pending"),
            ActiveContracts = _db.Contracts.Count(c => c.Status == "active"),
            ServiceRequestsOpen = _db.ServiceRequests.Count(s => s.Status == "open" || s.Status == "in_progress")
        };
    }
}
