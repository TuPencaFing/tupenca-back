using System;
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


        public void Save()
        {
            _appDbContext.SaveChanges();
        }

    }
}

