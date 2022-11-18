using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<PencaEmpresa> GetPencaEmpresas()
        {
            return _appDbContext.PencaEmpresas
                 .Include(p => p.Campeonato)
                 .Include(p => p.Premios)
                 .Include(p => p.Empresa).ThenInclude(e => e.Plan)
                 .ToList();
        }

        public int GetCantPencaEmpresas(int id)
        {
            return _appDbContext.PencaEmpresas
                 .Where(p => p.Empresa.Id == id)
                 .Include(p => p.Campeonato)
                 .Include(p => p.Premios)
                 .Include(p => p.Empresa)
                 .Count();
        }


        public IEnumerable<PencaEmpresa> GetPencaEmpresasByEmpresa(int id)
        {
            return _appDbContext.PencaEmpresas
                 .Where(p => p.Empresa.Id == id)
                 .Include(p => p.Campeonato)
                 .Include(p => p.Premios)
                 .Include(p => p.Empresa)
                 .ToList();
        }

        public int GetCantActivas()
        {
            return _appDbContext.PencaEmpresas
                    .Where(p => p.Campeonato.FinishDate > DateTime.Now)
                    .Count();
        }

        public PencaEmpresa GetFirst(Expression<Func<PencaEmpresa, bool>> filter)
        {
            return _appDbContext.PencaEmpresas
                    .Where(filter)
                    .Include(p => p.Campeonato)
                    .Include(p => p.Premios)
                    .Include(p => p.Empresa)
                    .First();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

       
    }
}

