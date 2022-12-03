using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.DataAccess.Repository
{
    public class PrediccionRepository : Repository<Prediccion>, IPrediccionRepository
    {
        private AppDbContext _appDbContext;
        public PrediccionRepository(AppDbContext db) : base(db)
        {
            _appDbContext = db;
        }


        public Prediccion GetPrediccionByUsuarioEvento(int usuarioId, int eventoId, int pencaId)
        {
            return _appDbContext.Predicciones
                .Where(pred => pred.EventoId == eventoId && pred.UsuarioId == usuarioId && pred.PencaId == pencaId)
                .FirstOrDefault();
        }

        public void UpdateScore(int eventoId, Resultado resultado)
        {
            var predicciones = _appDbContext.Predicciones.Where(pred => pred.EventoId == eventoId).ToList();
            var evento = _appDbContext.Eventos.Where(e => e.Id == eventoId).First();
            foreach (var pred in predicciones)
            {
                var penca = _appDbContext.Pencas.Where(p => p.Id == pred.PencaId).First();
                var puntaje = _appDbContext.Puntajes.Where(pu => pu.Id == penca.PuntajeId).First();

                if (resultado.resultado == pred.prediccion)
                {
                    if (evento.IsPuntajeEquipoValid)
                    {
                        if (resultado.PuntajeEquipoLocal == pred.PuntajeEquipoLocal
                            && resultado.PuntajeEquipoVisitante == pred.PuntajeEquipoVisitante)
                            pred.Score = puntaje.ResultadoExacto;                       
                    }
                    else pred.Score = puntaje.Resultado;

                }
                else pred.Score = 0;
                _appDbContext.Predicciones.Update(pred);
                _appDbContext.SaveChanges();
            }
        }


        public IEnumerable<UsuarioScore> GetUsuariosByPenca(int id)
        {
            var list = _appDbContext.Predicciones
            .Where(p => p.PencaId == id && p.Score != null)
            //.Join(_appDbContext.Usuarios, p => p.UsuarioId, user => user.Id, (p, user) => user)
            .GroupBy(p => p.UsuarioId)
            .Select(p => new { ID = p.Key, TotalScore = p.Sum(b => b.Score)})
            .OrderByDescending(a => a.TotalScore)
            .ToList();

            List<UsuarioScore> usuarios = new List<UsuarioScore>();

            foreach (var elem in list)
            {
                var user = _appDbContext.Personas.Where(p => p.Id == elem.ID).First();

                var usuarioScore = new UsuarioScore{ ID = elem.ID, UserName = user.UserName,TotalScore = elem.TotalScore};
                usuarios.Add(usuarioScore);
            }
            return usuarios;
        }


        public IEnumerable<Prediccion> getPrediccionesByEventoAndPenca(int eventoId, int pencaId)
        {
            return _appDbContext.Predicciones
                .Where(p => p.EventoId == eventoId && p.PencaId == pencaId)
                .ToList();
        }

        public decimal? getPorcentajeLocal(int idPenca, int idEvento)
        {
            var cantTotal = _appDbContext.Predicciones.Where(p => p.EventoId == idEvento && p.PencaId == idPenca).Count();
            if (cantTotal == 0) return null;
            var cantLocal = _appDbContext.Predicciones
                            .Where(p => p.EventoId == idEvento && p.PencaId == idPenca && p.prediccion == TipoResultado.VictoriaEquipoLocal)
                            .Count();
            
            return ((cantLocal*100)/cantTotal);
        }

        public decimal? getPorcentajeEmpate(int idPenca, int idEvento)
        {
            var cantTotal = _appDbContext.Predicciones.Where(p => p.EventoId == idEvento && p.PencaId == idPenca).Count();
            if (cantTotal == 0) return null;
            var cantLocal = _appDbContext.Predicciones
                            .Where(p => p.EventoId == idEvento && p.PencaId == idPenca && p.prediccion == TipoResultado.Empate)
                            .Count();
            return ((cantLocal * 100) / cantTotal);
        }



        public IEnumerable<Prediccion> getPrediccionesByEvento(int eventoId)
        {
            return _appDbContext.Predicciones
                .Where(p => p.EventoId == eventoId)
                .ToList();
        }


        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    
    }
}
