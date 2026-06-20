using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public interface ISlaService
{
    List<SlaDefinition> GetAll();
    SlaDefinition? GetById(int id);
    void Create(SlaDefinition sla);
    void Update(SlaDefinition sla);
    void Delete(int id);
}
