using DCOps.Web.Data;
using DCOps.Web.Models.Entities;

namespace DCOps.Web.Services;

public class SlaService : ISlaService
{
    private readonly AppDbContext _db;

    public SlaService(AppDbContext db)
    {
        _db = db;
    }

    public List<SlaDefinition> GetAll()
    {
        return _db.SlaDefinitions.OrderBy(s => s.Priority).ToList();
    }

    public SlaDefinition? GetById(int id)
    {
        return _db.SlaDefinitions.FirstOrDefault(s => s.Id == id);
    }

    public void Create(SlaDefinition sla)
    {
        sla.CreatedAt = DateTime.UtcNow;
        _db.SlaDefinitions.Add(sla);
        _db.SaveChanges();
    }

    public void Update(SlaDefinition sla)
    {
        _db.SlaDefinitions.Update(sla);
        _db.SaveChanges();
    }

    public void Delete(int id)
    {
        var sla = _db.SlaDefinitions.FirstOrDefault(s => s.Id == id);
        if (sla != null)
        {
            _db.SlaDefinitions.Remove(sla);
            _db.SaveChanges();
        }
    }
}
