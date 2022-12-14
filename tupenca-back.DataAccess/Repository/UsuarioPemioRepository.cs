using System;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class UsuarioPemioRepository : Repository<UsuarioPremio>, IUsuarioPremioRepository
    {

        private AppDbContext _appDbContext;

        public UsuarioPemioRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public UsuarioPremio? GetUsuarioPremioById(int id)
        {
            var premio = _appDbContext.UsuarioPremios.Where(u => u.IdUsuario == id).FirstOrDefault();
            if (premio != null)
            {
                premio.Penca = _appDbContext.Pencas.Where(p => p.Id == premio.PencaId).First();
            }
            return premio;
        }

        public UsuarioPremio? GetUsuarioPremioByUsuarioAndPenca(int idUsuario, int idPenca)
        {
            var premio = _appDbContext.UsuarioPremios.Where(u => u.IdUsuario == idUsuario && u.PencaId == idPenca).FirstOrDefault();
            if (premio != null)
            {
                premio.Penca = _appDbContext.Pencas.Where(p => p.Id == premio.PencaId).First();
            }
            return premio;
        }
    }
}

