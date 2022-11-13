using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class ForoRepository : Repository<Foro>, IForoRepository
    {
        private AppDbContext _appDbContext;
        public ForoRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }


        public IEnumerable<Foro> getMessagesByPenca(int pencaId)
        {
            return _appDbContext.Foros.Where(f => f.PencaId == pencaId)
                                      .Include(f => f.Usuario)
                                      .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
