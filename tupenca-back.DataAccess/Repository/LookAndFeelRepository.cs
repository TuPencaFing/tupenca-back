using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class LookAndFeelRepository : Repository<LookAndFeel>, ILookAndFeelRepository
    {
        private AppDbContext _appDbContext;
        public LookAndFeelRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
