using System;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class PencaEmpresaRepository : Repository<PencaEmpresa>, IPencaEmpresaRepository
    {
        private readonly AppDbContext _appDbContext;

        public PencaEmpresaRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}

