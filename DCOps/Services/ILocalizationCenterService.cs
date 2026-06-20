using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public interface ILocalizationCenterService
{
    string L(string key, string fallback = "");
    List<LocalizationKey> GetAllKeys();
    void UpdateKey(int id, string valueAr, string valueEn);
    Task SeedWave1KeysAsync();
}
