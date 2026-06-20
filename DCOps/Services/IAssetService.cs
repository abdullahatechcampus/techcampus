using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public interface IAssetService
{
    List<Asset> GetAllAssets();
    Asset? GetAssetById(int id);
    void CreateAsset(Asset asset);
    void UpdateAsset(Asset asset);
    void DeleteAsset(int id);
}
