using System.Linq.Expressions;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public interface IEventoRepository : IRepository<Evento>
    {
        IEnumerable<Evento> GetEventosProximos();

        IEnumerable<Evento> GetEventosFinalizados();

        IEnumerable<Evento> GetEventos();

        Evento GetFirst(Expression<Func<Evento, bool>> filter);

        void Save();
    }
}
