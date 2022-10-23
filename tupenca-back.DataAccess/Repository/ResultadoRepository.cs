using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class ResultadoRepository : Repository<Resultado>, IResultadoRepository
    {
        private AppDbContext _appDbContext;
        public ResultadoRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
