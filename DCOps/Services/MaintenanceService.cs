using DCOps.Web.Data;
using DCOps.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DCOps.Web.Services;

public class MaintenanceService : IMaintenanceService
{
    private readonly AppDbContext _db;

    public MaintenanceService(AppDbContext db)
    {
        _db = db;
    }

    public List<MaintenancePlan> GetAllPlans()
    {
        return _db.MaintenancePlans.Include(p => p.Asset).OrderByDescending(p => p.CreatedAt).ToList();
    }

    public MaintenancePlan? GetPlanById(int id)
    {
        return _db.MaintenancePlans.Include(p => p.Asset).FirstOrDefault(p => p.Id == id);
    }

    public void CreatePlan(MaintenancePlan plan)
    {
        plan.CreatedAt = DateTime.UtcNow;
        _db.MaintenancePlans.Add(plan);
        _db.SaveChanges();
    }

    public List<WorkOrder> GetAllWorkOrders()
    {
        return _db.WorkOrders.Include(w => w.Asset).OrderByDescending(w => w.CreatedAt).ToList();
    }

    public WorkOrder? GetWorkOrderById(int id)
    {
        return _db.WorkOrders.Include(w => w.Asset).FirstOrDefault(w => w.Id == id);
    }

    public void CreateWorkOrder(WorkOrder workOrder)
    {
        workOrder.CreatedAt = DateTime.UtcNow;
        _db.WorkOrders.Add(workOrder);
        _db.SaveChanges();
    }

    public void UpdateWorkOrder(WorkOrder workOrder)
    {
        _db.WorkOrders.Update(workOrder);
        _db.SaveChanges();
    }
}
