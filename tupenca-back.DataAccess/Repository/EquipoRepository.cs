using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class EquipoRepository : Repository<Equipo>, IEquipoRepository
    {
        private AppDbContext _appDbContext;
        public EquipoRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }


        public IEnumerable<Equipo> GetEquipos()
        {
            return _appDbContext.Equipo.ToList();
        }

        public Equipo GetEquipoByID(int equipoId)
        {
            return _appDbContext.Equipo.Find(equipoId);
        }

        public Equipo GetEquipoByName(string nombreEquipo)
        {
            return _appDbContext.Equipo.Find(nombreEquipo);
        }

        public void Create(Equipo obj)
        {
            _appDbContext.Equipo.Add(obj);
            _appDbContext.SaveChanges();
        }

        public void Update(Equipo obj)
        {
            _appDbContext.Equipo.Add(obj);
            _appDbContext.SaveChanges();
        }

        public void Delete(int equipoID) 
        {
            _appDbContext.Equipo.Remove(equipoID);
            _appDbContext.SaveChanges();
        }

    }
}
