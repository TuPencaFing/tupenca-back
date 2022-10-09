using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IEnumerable<Evento> GetEventos();
        IEnumerable<Evento> GetEventosProximos();
        Evento GetEventoByID(int eventoId);
        void Create(Evento obj);
        void Update(Evento obj);
        void Delete(int eventoID);
    }
}
