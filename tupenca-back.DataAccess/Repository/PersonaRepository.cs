using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public string createInviteToken(int id, int pencaId)
        {
            //var idQuery =
            //from func in _appDbContext.Funcionarios
            //where func.I d == id
            //select func;
            //Funcionario func = _appDbContext.Funcionarios.Include(p=>p.Empresa).FirstOrDefault(f => f.Id == id);
            //var emp =  _appDbContext.Empresas.Where(p => p.Id == func.EmpresaId).Include(p => p.Pencas).FirstOrDefault();

            //Empresa emp = _appDbContext.Empresas.FirstOrDefault(f => f.Id == func.EmpresaId);
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = BitConverter.GetBytes(pencaId);

            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            var userToken = new UserInviteToken
            {
                Token = token,
                PencaId = pencaId,
            };

            _appDbContext.UserInviteTokens.Add(userToken);
            _appDbContext.SaveChanges();


            //var pencas = emp.Pencas;
            //var je = (from d in pencas
            //     where d.Id == pencaId
            //     select d);
            return token;
            //Empresa empresa = _appDbContext.Empresas.Select(e => e.Funcionarios.)
            //var query = _appDbContext.Funcionarios.Where(f => f.Empresa.Pencas.Where(p => p.Id == pencaId))
        }




    }
}
