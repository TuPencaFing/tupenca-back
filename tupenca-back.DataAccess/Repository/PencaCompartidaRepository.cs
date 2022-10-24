using System;
using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class PencaCompartidaRepository : Repository<PencaCompartida>, IPencaCompartidaRepository
    {

        private readonly AppDbContext _appDbContext;

        public PencaCompartidaRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public IEnumerable<PencaCompartida> GetPencaCompartidas()
        {
            return _appDbContext.PencaCompartidas
                .Include(p => p.Campeonato)
                .Include(p => p.Premios)
                .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

    }
}

