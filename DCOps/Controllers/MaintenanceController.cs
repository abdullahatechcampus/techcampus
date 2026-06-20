using DCOps.Web.Models.Entities;
using DCOps.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DCOps.Web.Controllers;

[Authorize]
public class MaintenanceController : Controller
{
    private readonly IMaintenanceService _maintenanceService;
    private readonly IAssetService _assetService;

    public MaintenanceController(IMaintenanceService maintenanceService, IAssetService assetService)
    {
        _maintenanceService = maintenanceService;
        _assetService = assetService;
    }

    public IActionResult Index()
    {
        var plans = _maintenanceService.GetAllPlans();
        return View(plans);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadAssetSelectList();
        return View(new MaintenancePlan());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(MaintenancePlan plan)
    {
        if (!ModelState.IsValid)
        {
            LoadAssetSelectList();
            return View(plan);
        }

        if (string.IsNullOrEmpty(plan.PlanCode))
            plan.PlanCode = $"MP-{DateTime.UtcNow:yyyyMMddHHmmss}";

        _maintenanceService.CreatePlan(plan);
        TempData["Success"] = "تم إنشاء خطة الصيانة بنجاح";
        return RedirectToAction(nameof(Index));
    }

    private void LoadAssetSelectList()
    {
        var assets = _assetService.GetAllAssets();
        ViewBag.Assets = new SelectList(assets, "Id", "Name");
    }
}
