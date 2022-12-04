using System.Data;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class PrediccionService
    {
        private readonly IPrediccionRepository _prediccionRepository;

        public PrediccionService(IPrediccionRepository prediccionRepository)
        {
            _prediccionRepository = prediccionRepository;
        }

        public IEnumerable<Prediccion> getPredicciones() => _prediccionRepository.GetAll();

        public Prediccion? getPrediccionById(int id) => _prediccionRepository.GetFirstOrDefault(e => e.Id == id);

        public Prediccion? getPrediccionByEventoAndPencaAndUsuario(int eventoid, int pencaid, int usuarioid) => _prediccionRepository
            .GetFirstOrDefault(e => e.EventoId == eventoid && e.PencaId == pencaid && e.UsuarioId == usuarioid);

        public int? isPrediccionCorrect(Prediccion prediccion, Resultado resultado, Puntaje puntaje)
        {
            if (prediccion == null)
            {
                return null;
            }
            if (resultado == null)
            {
                return null;
            }
            if (resultado.resultado == prediccion.prediccion)
            {
                if (resultado.PuntajeEquipoLocal == prediccion.PuntajeEquipoLocal
                    && resultado.PuntajeEquipoVisitante == prediccion.PuntajeEquipoVisitante)
                    return puntaje.ResultadoExacto;
                else return puntaje.Resultado;

            }
            else return 0;
        }

        public void CreatePrediccion(Prediccion prediccion)
        {
            if (prediccion != null)
            {
                _prediccionRepository.Add(prediccion);
                _prediccionRepository.Save();
            }
        }

        public void UpdatePrediccion(Prediccion prediccion)
        {
            if (prediccion != null)
            {
                _prediccionRepository.Update(prediccion);
                _prediccionRepository.Save();
            }
        }

        
        public void RemovePrediccion(Prediccion prediccion)
        {
            if (prediccion != null)
            {
                _prediccionRepository.Remove(prediccion);
                _prediccionRepository.Save();
            }
        }

        public Prediccion? GetPrediccionByUsuarioEvento(int usuario, int evento, int penca)
        {
            return _prediccionRepository.GetPrediccionByUsuarioEvento(usuario, evento, penca);
        }
        
        
        public void UpdateScore(int eventoId, Resultado resultado)
        {
            _prediccionRepository.UpdateScore(eventoId, resultado);
        }

        public IEnumerable<Prediccion> getPrediccionesByEventoAndPenca(int eventoId, int pencaId)
        {
            return _prediccionRepository.getPrediccionesByEventoAndPenca(eventoId, pencaId);
        }

        public IEnumerable<Prediccion> getPrediccionesByEvento(int eventoId)
        {
            return _prediccionRepository.getPrediccionesByEvento(eventoId);
        }


        public decimal? GetPorcentajeLocal(int idPenca, int idEvento)
        {
            return _prediccionRepository.getPorcentajeLocal(idPenca, idEvento);
        }

        public decimal? GetPorcentajeEmpate(int idPenca, int idEvento)
        {
            return _prediccionRepository.getPorcentajeEmpate(idPenca, idEvento);
        }

    }
}

