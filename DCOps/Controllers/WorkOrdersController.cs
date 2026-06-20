using DCOps.Web.Models.Entities;
using DCOps.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DCOps.Web.Controllers;

[Authorize]
public class WorkOrdersController : Controller
{
    private readonly IMaintenanceService _maintenanceService;
    private readonly IAssetService _assetService;

    public WorkOrdersController(IMaintenanceService maintenanceService, IAssetService assetService)
    {
        _maintenanceService = maintenanceService;
        _assetService = assetService;
    }

    public IActionResult Index()
    {
        var workOrders = _maintenanceService.GetAllWorkOrders();
        return View(workOrders);
    }

    public IActionResult Details(int id)
    {
        var workOrder = _maintenanceService.GetWorkOrderById(id);
        if (workOrder == null) return NotFound();
        return View(workOrder);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadAssetSelectList();
        return View(new WorkOrder());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(WorkOrder workOrder)
    {
        if (!ModelState.IsValid)
        {
            LoadAssetSelectList();
            return View(workOrder);
        }

        if (string.IsNullOrEmpty(workOrder.OrderNo))
            workOrder.OrderNo = $"WO-{DateTime.UtcNow:yyyyMMddHHmmss}";

        _maintenanceService.CreateWorkOrder(workOrder);
        TempData["Success"] = "تم إنشاء أمر العمل بنجاح";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var workOrder = _maintenanceService.GetWorkOrderById(id);
        if (workOrder == null) return NotFound();
        LoadAssetSelectList();
        return View(workOrder);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, WorkOrder workOrder)
    {
        if (id != workOrder.Id) return BadRequest();
        if (!ModelState.IsValid)
        {
            LoadAssetSelectList();
            return View(workOrder);
        }

        _maintenanceService.UpdateWorkOrder(workOrder);
        TempData["Success"] = "تم تحديث أمر العمل بنجاح";
        return RedirectToAction(nameof(Index));
    }

    private void LoadAssetSelectList()
    {
        var assets = _assetService.GetAllAssets();
        ViewBag.Assets = new SelectList(assets, "Id", "Name");
    }
}
