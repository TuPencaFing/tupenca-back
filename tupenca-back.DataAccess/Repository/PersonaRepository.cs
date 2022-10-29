using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace tupenca_back.DataAccess.Repository
{
    public class PersonaRepository : Repository<Persona>, IPersonaRepository
    {
        private AppDbContext _appDbContext;
        public PersonaRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public object findPenca(int id, int pencaId)
        {
            //var idQuery =
            //from func in _appDbContext.Funcionarios
            //where func.I d == id
            //select func;
            Funcionario func = _appDbContext.Funcionarios.FirstOrDefault(f => f.Id == id);
            Empresa emp = _appDbContext.Empresas.FirstOrDefault(f => f.Id == func.EmpresaId);
            var je = (from d in func.Empresa.Pencas
                 where d.Id == id
                 select d);
            return je;
            //Empresa empresa = _appDbContext.Empresas.Select(e => e.Funcionarios.)
            //var query = _appDbContext.Funcionarios.Where(f => f.Empresa.Pencas.Where(p => p.Id == pencaId))
        }




    }
}
