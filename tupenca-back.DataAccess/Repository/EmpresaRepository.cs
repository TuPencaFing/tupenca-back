using Microsoft.EntityFrameworkCore;
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
            var today = DateTime.Now;
            return _appDbContext.Empresa
                .Where(empresa => empresa.FechaCreacion.AddDays(7) > today)
                .Count();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

    }
}
