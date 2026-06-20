using DCOps.Web.Models.Entities;
using DCOps.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DCOps.Web.Controllers;

[Authorize]
public class AssetsController : Controller
{
    private readonly IAssetService _assetService;

    public AssetsController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    public IActionResult Index()
    {
        var assets = _assetService.GetAllAssets();
        return View(assets);
    }

    public IActionResult Details(int id)
    {
        var asset = _assetService.GetAssetById(id);
        if (asset == null) return NotFound();
        return View(asset);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Asset());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Asset asset)
    {
        if (!ModelState.IsValid) return View(asset);

        // Auto-generate asset code if empty
        if (string.IsNullOrEmpty(asset.AssetCode))
            asset.AssetCode = $"AST-{DateTime.UtcNow:yyyyMMddHHmmss}";

        _assetService.CreateAsset(asset);
        TempData["Success"] = "تم إضافة الأصل بنجاح";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var asset = _assetService.GetAssetById(id);
        if (asset == null) return NotFound();
        return View(asset);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Asset asset)
    {
        if (id != asset.Id) return BadRequest();
        if (!ModelState.IsValid) return View(asset);

        _assetService.UpdateAsset(asset);
        TempData["Success"] = "تم تحديث الأصل بنجاح";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var asset = _assetService.GetAssetById(id);
        if (asset == null) return NotFound();
        return View(asset);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        _assetService.DeleteAsset(id);
        TempData["Success"] = "تم حذف الأصل بنجاح";
        return RedirectToAction(nameof(Index));
    }
}
