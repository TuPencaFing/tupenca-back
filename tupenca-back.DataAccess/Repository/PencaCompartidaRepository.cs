using System;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class PencaCompartidaRepository : Repository<PencaCompartida>, IPencaCompartidaRepository
    {

        private readonly AppDbContext _appDbContext;

        public PencaCompartidaRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }

        public IEnumerable<PencaCompartida> GetPencaCompartidas()
        {
            return _appDbContext.PencaCompartidas
                .Include(p => p.Campeonato)
                .Include(p => p.Premios)
                .ToList();
        }


        public PencaCompartida GetFirst(Expression<Func<PencaCompartida, bool>> filter)
        {
            return _appDbContext.PencaCompartidas
                    .Where(filter)
                    .Include(p => p.Campeonato)
                    .Include(p => p.Premios)
                    .First();
        }

        public int GetCantActivas()
        {
            return _appDbContext.PencaCompartidas
                    .Where(p => p.Campeonato.FinishDate > DateTime.UtcNow)
                    .Count();
        }


        public IEnumerable<PencaCompartida> GetPencasHot()
        {
            return _appDbContext.PencaCompartidas
                .Where(p => p.Campeonato.FinishDate > DateTime.UtcNow)
                .OrderByDescending(p => p.Pozo)
                .Take(5)
                .ToList();
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public IEnumerable<EventoPrediccion?> GetInfoEventosByPencaUsuario(int PencaId, int userId)
        {
            var eventos = _appDbContext.Pencas.Where(p => p.Id == PencaId).Select(p => p.Campeonato).Select(c => c.Eventos).Single().ToList();
            var eventosresultados = eventos.GroupJoin(_appDbContext.Resultados, e => e.Id, r => r.EventoId, (e, r) => new EventoPrediccion
            {
                Id = e.Id,
                Image = e.Image,
                FechaInicial = e.FechaInicial,
                EquipoLocalId = e.EquipoLocalId,
                EquipoLocal = _appDbContext.Equipos.FirstOrDefault(eq => eq.Id == e.EquipoLocalId),
                EquipoVisitanteId = e.EquipoVisitanteId,
                EquipoVisitante = _appDbContext.Equipos.FirstOrDefault(eq => eq.Id == e.EquipoVisitanteId),
                IsEmpateValid = e.IsEmpateValid,
                IsPuntajeEquipoValid = e.IsPuntajeEquipoValid,
                Resultado = r.FirstOrDefault(res => res.EventoId == e.Id)
            
            });
            var predicciones = _appDbContext.Predicciones.Where(pred => pred.PencaId == PencaId && pred.UsuarioId == userId).ToList();
            var eventosresultadospredicciones = eventosresultados.GroupJoin(predicciones, e => e.Id, pred => pred.EventoId, (e, pred) => new EventoPrediccion
            {
                Id = e.Id,
                Image = e.Image,
                FechaInicial = e.FechaInicial,
                EquipoLocalId = e.EquipoLocalId,
                EquipoLocal = e.EquipoLocal,
                EquipoVisitanteId = e.EquipoVisitanteId,
                EquipoVisitante = e.EquipoVisitante,
                IsEmpateValid = e.IsEmpateValid,
                IsPuntajeEquipoValid = e.IsPuntajeEquipoValid,
                Resultado = e.Resultado,
                Prediccion = pred.FirstOrDefault(res => res.EventoId == e.Id)
            });
            return eventosresultadospredicciones;
        }


    }
}

