using System;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class PuntajeRepository : Repository<Puntaje>, IPuntajeRepository
    {
        private readonly AppDbContext _appDbContext;

        public PuntajeRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

    }
}

