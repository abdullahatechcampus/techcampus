using DCOps.Web.Data;
using DCOps.Web.Models.Entities;
using DCOps.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace DCOps.Web.Controllers;

[Authorize]
public class ServiceRequestsController : Controller
{
    private readonly AppDbContext _db;
    private readonly IAssetService _assetService;
    private readonly ISlaService _slaService;

    public ServiceRequestsController(AppDbContext db, IAssetService assetService, ISlaService slaService)
    {
        _db = db;
        _assetService = assetService;
        _slaService = slaService;
    }

    public IActionResult Index()
    {
        var requests = _db.ServiceRequests
            .OrderByDescending(r => r.CreatedAt)
            .ToList();
        return View(requests);
    }

    public IActionResult Details(int id)
    {
        var request = _db.ServiceRequests
            .FirstOrDefault(r => r.Id == id);
        if (request == null) return NotFound();
        return View(request);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadSelectLists();
        return View(new ServiceRequest());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ServiceRequest request)
    {
        if (!ModelState.IsValid)
        {
            LoadSelectLists();
            return View(request);
        }

        if (string.IsNullOrEmpty(request.RequestNo))
            request.RequestNo = $"SR-{DateTime.UtcNow:yyyyMMddHHmmss}";

        var username = User.FindFirst(ClaimTypes.Name)?.Value;
        if (string.IsNullOrEmpty(request.RequestedBy))
            request.RequestedBy = username;

        request.CreatedAt = DateTime.UtcNow;
        request.UpdatedAt = DateTime.UtcNow;

        _db.ServiceRequests.Add(request);
        _db.SaveChanges();

        TempData["Success"] = "تم إنشاء طلب الخدمة بنجاح";
        return RedirectToAction(nameof(Index));
    }

    private void LoadSelectLists()
    {
        var assets = _assetService.GetAllAssets();
        ViewBag.Assets = new SelectList(assets, "Id", "Name");

        var slas = _slaService.GetAll();
        ViewBag.Slas = new SelectList(slas, "Id", "Name");
    }
}
