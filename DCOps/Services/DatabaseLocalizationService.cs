using DCOps.Web.Data;
using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class DatabaseLocalizationService : IDatabaseLocalizationService
{
    private readonly AppDbContext _db;

    public DatabaseLocalizationService(AppDbContext db)
    {
        _db = db;
    }

    public async Task SeedWave1KeysAsync()
    {
        if (_db.LocalizationKeys.Any()) return;

        var keys = new List<LocalizationKey>
        {
            new() { KeyName = "nav.dashboard", Module = "nav", ValueAr = "لوحة التحكم", ValueEn = "Dashboard" },
            new() { KeyName = "nav.assets", Module = "nav", ValueAr = "إدارة الأصول", ValueEn = "Assets" },
            new() { KeyName = "nav.workorders", Module = "nav", ValueAr = "أوامر العمل", ValueEn = "Work Orders" },
            new() { KeyName = "nav.maintenance", Module = "nav", ValueAr = "الصيانة", ValueEn = "Maintenance" },
            new() { KeyName = "nav.servicerequests", Module = "nav", ValueAr = "طلبات الخدمة", ValueEn = "Service Requests" },
            new() { KeyName = "nav.notifications", Module = "nav", ValueAr = "الإشعارات", ValueEn = "Notifications" },
            new() { KeyName = "common.save", Module = "common", ValueAr = "حفظ", ValueEn = "Save" },
            new() { KeyName = "common.cancel", Module = "common", ValueAr = "إلغاء", ValueEn = "Cancel" },
            new() { KeyName = "common.edit", Module = "common", ValueAr = "تعديل", ValueEn = "Edit" },
            new() { KeyName = "common.delete", Module = "common", ValueAr = "حذف", ValueEn = "Delete" },
            new() { KeyName = "common.details", Module = "common", ValueAr = "التفاصيل", ValueEn = "Details" },
            new() { KeyName = "common.create", Module = "common", ValueAr = "إضافة جديد", ValueEn = "Create New" },
            new() { KeyName = "status.active", Module = "status", ValueAr = "نشط", ValueEn = "Active" },
            new() { KeyName = "status.inactive", Module = "status", ValueAr = "غير نشط", ValueEn = "Inactive" },
            new() { KeyName = "status.open", Module = "status", ValueAr = "مفتوح", ValueEn = "Open" },
            new() { KeyName = "status.closed", Module = "status", ValueAr = "مغلق", ValueEn = "Closed" },
            new() { KeyName = "priority.low", Module = "priority", ValueAr = "منخفض", ValueEn = "Low" },
            new() { KeyName = "priority.medium", Module = "priority", ValueAr = "متوسط", ValueEn = "Medium" },
            new() { KeyName = "priority.high", Module = "priority", ValueAr = "عالي", ValueEn = "High" },
            new() { KeyName = "priority.critical", Module = "priority", ValueAr = "حرج", ValueEn = "Critical" },
        };

        foreach (var key in keys)
        {
            key.CreatedAt = DateTime.UtcNow;
            key.UpdatedAt = DateTime.UtcNow;
        }

        _db.LocalizationKeys.AddRange(keys);
        await _db.SaveChangesAsync();
    }
}
