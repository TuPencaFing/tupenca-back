using System;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public class CampeonatoRepository : Repository<Campeonato>, ICampeonatoRepository
    {
        private readonly AppDbContext _appDbContext;

        public CampeonatoRepository(AppDbContext appDbContext)
            :base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Save()
        {
            _appDbContext.SaveChangesAsync();
        }
    }
}

