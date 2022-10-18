using System;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Campeonato> GetCampeonatos()
        {
            return _appDbContext.Campeonatos
                .Include(c => c.Deporte)
                .Include(c => c.Eventos)
                .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}

