using System;
using Microsoft.EntityFrameworkCore;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository.IRepository
{
    public class CampeonatoRepository : Repository<Campeonato>, ICampeonatoRepository
    {
        private readonly AppDbContext _appDbContext;

        public CampeonatoRepository(AppDbContext appDbContext)
            :base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Campeonato? FindCampeonatoById(int id)
        {
            return _appDbContext.Campeonatos
                    .Include(c => c.Deporte)
                    .Include(c => c.Eventos).ThenInclude(e => e.EquipoLocal)
                    .Include(c => c.Eventos).ThenInclude(e => e.EquipoVisitante)
                    .Where(c => c.Id == id)
                    .FirstOrDefault();
        }

        public IEnumerable<Campeonato> GetCampeonatos()
        {
            return _appDbContext.Campeonatos
                .Include(c => c.Deporte)
                .Include(c => c.Eventos).ThenInclude(e => e.EquipoLocal)
                .Include(c => c.Eventos).ThenInclude(e => e.EquipoVisitante)
                .ToList();
        }

        public IEnumerable<Campeonato> GetCampeonatosFinalized()
        {
            return _appDbContext.Campeonatos
                .Where(c => c.FinishDate < DateTime.Now && !c.PremiosEntregados)
                .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Campeonato>? SearchCampeonato(string searchString)
        {
            return _appDbContext.Campeonatos.Where(e => e.Name.Contains(searchString)).ToList();
        }
    }
}

