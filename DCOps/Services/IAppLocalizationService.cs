namespace DCOps.Web.Services;

public interface IAppLocalizationService
{
    string L(string key, string fallback = "");
}
