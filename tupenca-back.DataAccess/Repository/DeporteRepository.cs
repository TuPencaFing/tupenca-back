using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class DeporteRepository : Repository<Deporte>, IDeporteRepository
    {
        private AppDbContext _appDbContext;
        public DeporteRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }


        public IEnumerable<Deporte> GetDeportes()
        {
            return _appDbContext.Deporte.ToList();
        }

        public Deporte GetDeporteByID(int deporteId)
        {
            return _appDbContext.Deporte.Find(deporteId);
        }
    }
}
