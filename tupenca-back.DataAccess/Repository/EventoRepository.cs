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


        public IEnumerable<Evento> GetEventos()
        {
            return _appDbContext.Evento.ToList();
        }

        IEnumerable<Evento> GetEventosProximos()
        {
            var today = DateTime.Today;
            return _appDbContext.Evento.Where(evento => evento.FechaInicial > today & evento.FechaInicial < today.AddDays(7));
        }

        public Equipo GetEventoByID(int eventoId)
        {
            return _appDbContext.Evento.Find(eventoId);
        }

        public void Create(Evento obj)
        {
            _appDbContext.Evento.Add(obj);
            _appDbContext.SaveChanges();
        }

        public void Update(Evento obj)
        {
            _appDbContext.Evento.Add(obj);
            _appDbContext.SaveChanges();
        }

        public void Delete(int eventoID) 
        {
            _appDbContext.Evento.Remove(eventoID);
            _appDbContext.SaveChanges();
        }

    }
}
