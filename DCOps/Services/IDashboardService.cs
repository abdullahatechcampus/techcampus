namespace DCOps.Web.Services;

public class DashboardStats
{
    public int TotalAssets { get; set; }
    public int OpenWorkOrders { get; set; }
    public int ActiveAlerts { get; set; }
    public int PendingApprovals { get; set; }
    public int ActiveContracts { get; set; }
    public int ServiceRequestsOpen { get; set; }
}

public interface IDashboardService
{
    DashboardStats GetDashboardStats();
}
