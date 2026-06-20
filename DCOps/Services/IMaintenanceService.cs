using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public interface IMaintenanceService
{
    List<MaintenancePlan> GetAllPlans();
    MaintenancePlan? GetPlanById(int id);
    void CreatePlan(MaintenancePlan plan);
    List<WorkOrder> GetAllWorkOrders();
    WorkOrder? GetWorkOrderById(int id);
    void CreateWorkOrder(WorkOrder workOrder);
    void UpdateWorkOrder(WorkOrder workOrder);
}
