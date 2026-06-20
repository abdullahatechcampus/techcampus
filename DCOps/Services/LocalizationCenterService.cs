using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class LocalizationCenterService : ILocalizationCenterService
{
    private readonly IAppLocalizationService _appLoc;
    private readonly ITranslationManagementService _translationMgmt;
    private readonly IDatabaseLocalizationService _dbLoc;

    public LocalizationCenterService(
        IAppLocalizationService appLoc,
        ITranslationManagementService translationMgmt,
        IDatabaseLocalizationService dbLoc)
    {
        _appLoc = appLoc;
        _translationMgmt = translationMgmt;
        _dbLoc = dbLoc;
    }

    public string L(string key, string fallback = "") => _appLoc.L(key, fallback);

    public List<LocalizationKey> GetAllKeys() => _translationMgmt.GetAllKeys();

    public void UpdateKey(int id, string valueAr, string valueEn) =>
        _translationMgmt.UpdateKey(id, valueAr, valueEn);

    public Task SeedWave1KeysAsync() => _dbLoc.SeedWave1KeysAsync();
}
