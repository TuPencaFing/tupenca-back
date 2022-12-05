using Microsoft.EntityFrameworkCore;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class ForoRepository : Repository<Foro>, IForoRepository
    {
        private AppDbContext _appDbContext;
        public ForoRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }


        public IEnumerable<ForoUsers> getMessagesByPenca(int pencaId)
        {
            return _appDbContext.Foros.Where(f => f.PencaId == pencaId)
                                      .Join(_appDbContext.Personas, f => f.UsuarioId, p => p.Id, (f, p) => new ForoUsers
                                      {
                                          Id = f.Id,
                                          Message = f.Message,
                                          UsuarioId = f.UsuarioId,
                                          PencaId = f.PencaId,
                                          Creacion = f.Creacion,
                                          UserName = p.UserName,
                                          Image = p.Image
                                      })
                                      .OrderBy(f => f.Creacion)
                                      .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
