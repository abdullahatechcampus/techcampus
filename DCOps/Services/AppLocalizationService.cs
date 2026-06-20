using DCOps.Web.Data;
using Microsoft.Extensions.Caching.Memory;

namespace DCOps.Web.Services;

public class AppLocalizationService : IAppLocalizationService
{
    private readonly AppDbContext _db;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "loc_keys_ar";

    public AppLocalizationService(AppDbContext db, IMemoryCache cache)
    {
        _db = db;
        _cache = cache;
    }

    public string L(string key, string fallback = "")
    {
        var dict = _cache.GetOrCreate(CacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return _db.LocalizationKeys.ToDictionary(k => k.KeyName, k => k.ValueAr ?? k.ValueEn ?? "");
        });

        if (dict != null && dict.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value))
            return value;

        return string.IsNullOrEmpty(fallback) ? key : fallback;
    }
}
