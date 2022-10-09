using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IDeporteRepository : IRepository<Deporte>
    {
        IEnumerable<Deporte> GetDeportes();
        Deporte GetDeporteByID(int deporteId);
    }
}
