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
            var today = DateTime.UtcNow;
            return _appDbContext.Eventos
                .Where(evento => evento.FechaInicial > today & evento.FechaInicial < today.AddDays(7))
                .Include(evento => evento.EquipoLocal)
                .Include(evento => evento.EquipoVisitante)
                .ToList();
        }


        public IEnumerable<Evento> GetEventosFinalizados()
        {
            return _appDbContext.Eventos
                    .Where(evento => evento.FechaInicial < DateTime.UtcNow)
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

        public IEnumerable<Usuario> getUsuariosWithoutPredictionForEvent()
        {
            HashSet<Usuario> usuarios = new HashSet<Usuario>();
            var today = DateTime.UtcNow;
            var eventos = _appDbContext.Eventos
                .Where(evento => evento.FechaInicial > today & evento.FechaInicial < today.AddDays(7))
                .OrderBy(evento => evento.FechaInicial).Include(e=>e.Campeonatos);

            foreach(Evento evento in eventos)
            {
                var temp = _appDbContext.Eventos.Where(e => e.Id == evento.Id).SelectMany(e => e.Campeonatos)
                .SelectMany(c => c.Pencas)
                .SelectMany(p => p.UsuariosPencas)
                .Select(u => u.Usuario)
                .Where(u => !_appDbContext.Predicciones.Any(up => up.UsuarioId == u.Id && up.EventoId == evento.Id)
                 );
                usuarios.UnionWith(temp);
            }

            return usuarios.ToList();
        }
    }
}
