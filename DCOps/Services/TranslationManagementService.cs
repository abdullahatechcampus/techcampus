using DCOps.Web.Data;
using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class TranslationManagementService : ITranslationManagementService
{
    private readonly AppDbContext _db;

    public TranslationManagementService(AppDbContext db)
    {
        _db = db;
    }

    public List<LocalizationKey> GetAllKeys()
    {
        return _db.LocalizationKeys.OrderBy(k => k.Module).ThenBy(k => k.KeyName).ToList();
    }

    public void UpdateKey(int id, string valueAr, string valueEn)
    {
        var key = _db.LocalizationKeys.FirstOrDefault(k => k.Id == id);
        if (key != null)
        {
            key.ValueAr = valueAr;
            key.ValueEn = valueEn;
            key.UpdatedAt = DateTime.UtcNow;
            _db.SaveChanges();
        }
    }
}
