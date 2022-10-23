using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class PrediccionRepository : Repository<Prediccion>, IPrediccionRepository
    {
        private AppDbContext _appDbContext;
        public PrediccionRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
