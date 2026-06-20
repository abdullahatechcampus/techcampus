using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public interface ITranslationManagementService
{
    List<LocalizationKey> GetAllKeys();
    void UpdateKey(int id, string valueAr, string valueEn);
}
