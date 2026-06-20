using DCOps.Web.Data;
using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class AssetService : IAssetService
{
    private readonly AppDbContext _db;

    public AssetService(AppDbContext db)
    {
        _db = db;
    }

    public List<Asset> GetAllAssets()
    {
        return _db.Assets.OrderBy(a => a.Name).ToList();
    }

    public Asset? GetAssetById(int id)
    {
        return _db.Assets.FirstOrDefault(a => a.Id == id);
    }

    public void CreateAsset(Asset asset)
    {
        asset.CreatedAt = DateTime.UtcNow;
        asset.UpdatedAt = DateTime.UtcNow;
        _db.Assets.Add(asset);
        _db.SaveChanges();
    }

    public void UpdateAsset(Asset asset)
    {
        asset.UpdatedAt = DateTime.UtcNow;
        _db.Assets.Update(asset);
        _db.SaveChanges();
    }

    public void DeleteAsset(int id)
    {
        var asset = _db.Assets.FirstOrDefault(a => a.Id == id);
        if (asset != null)
        {
            _db.Assets.Remove(asset);
            _db.SaveChanges();
        }
    }
}
