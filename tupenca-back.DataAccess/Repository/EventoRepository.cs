using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class EventoRepository : Repository<Evento>, IEventoRepository
    {
        private AppDbContext _appDbContext;
        public EventoRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public IEnumerable<Evento> GetEventosProximos()
        {
            var today = DateTime.Now;
            return _appDbContext.Eventos
                .Where(evento => evento.FechaInicial > today & evento.FechaInicial < today.AddDays(7))
                .Include(evento => evento.EquipoLocal)
                .Include(evento => evento.EquipoVisitante)
                .ToList();
        }

        public IEnumerable<Evento> GetEventos()
        {
            return _appDbContext.Eventos
                .Include(evento => evento.EquipoLocal)
                .Include(evento => evento.EquipoVisitante)
                .ToList();
        }

        public Evento GetFirst(Expression<Func<Evento, bool>> filter)
        {
            return _appDbContext.Eventos.Where(filter)
                .Include(evento => evento.EquipoLocal)
                .Include(evento => evento.EquipoVisitante)
                .FirstOrDefault();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
