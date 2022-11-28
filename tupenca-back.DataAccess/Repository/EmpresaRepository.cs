using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using tupenca_back.DataAccess.Migrations;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class EmpresaRepository : Repository<Empresa>, IEmpresaRepository
    {
        private AppDbContext _appDbContext;
        public EmpresaRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public int GetCantEmpresasNuevas()
        {
            var today = DateTime.UtcNow;
            return _appDbContext.Empresas
                .Where(empresa => empresa.FechaCreacion.AddDays(7) > today)
                .Count();
        }

        public int GetCantEmpresas()
        {
            return _appDbContext.Empresas
                   .Count();
        }


        public Empresa GetFirst(Expression<Func<Empresa, bool>> filter)
        {
            return _appDbContext.Empresas.Where(filter)
                .Include(empresa => empresa.Plan)
                .FirstOrDefault();
        }
        public IEnumerable<Empresa> GetEmpresas()
        {
            return _appDbContext.Empresas
                .Include(empresa => empresa.Plan)
                .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

    }
}
