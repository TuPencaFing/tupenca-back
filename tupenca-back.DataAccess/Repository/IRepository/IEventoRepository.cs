using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IEnumerable<Evento> GetEventosProximos();

        IEnumerable<Evento> GetEventos();

        void Save();
    }
}
