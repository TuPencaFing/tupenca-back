using System;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class UsuarioPemioRepository : Repository<UsuarioPremio>, IUsuarioPremioRepository
    {

        private AppDbContext _appDbContext;

        public UsuarioPemioRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

    }
}

