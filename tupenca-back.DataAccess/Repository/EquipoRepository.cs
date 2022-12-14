using System.Linq.Expressions;
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

        public IEnumerable<Equipo>? SearchEquipo(string searchString)
        {
            return _appDbContext.Equipos.Where(e => e.Nombre.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

    }
}
