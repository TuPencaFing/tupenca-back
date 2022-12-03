using System;
using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class PuntajeUsuarioPencaRepository : Repository<PuntajeUsuarioPenca>, IPuntajeUsuarioPencaRepository
    {
        private AppDbContext _appDbContext;

        public PuntajeUsuarioPencaRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PuntajeUsuarioPenca> GetAllByPenca(int pencaId)
        {
            return _appDbContext.PuntajeUsuarioPencas
                .Where(pup => pup.PencaId == pencaId )
                .OrderBy(pup => pup.Score)
                .Include(pup => pup.Usuario)
                .ToList();
        }

        public IEnumerable<PuntajeUsuarioPenca> GetAllByUsuario(int usuarioId)
        {
            return _appDbContext.PuntajeUsuarioPencas
                .Where(pup => pup.UsuarioId == usuarioId)
                .Include(pup => pup.Usuario)
                .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}

