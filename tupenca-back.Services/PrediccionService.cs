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

        public Prediccion? getPrediccionByEventoId(int id) => _prediccionRepository.GetFirstOrDefault(e => e.EventoId == id);

        public bool isPrediccionCorrect(Prediccion prediccion, Resultado resultado)
        {
            if (prediccion == null)
            {
                return false;
            }
            if (resultado.resultado == prediccion.prediccion)
            {
                if (resultado.PuntajeEquipoLocal == prediccion.PuntajeEquipoLocal
                    && resultado.PuntajeEquipoVisitante == prediccion.PuntajeEquipoVisitante)
                    return true;
                else return false;

            }
            else return false;
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

        /*
        public void RemovePrediccion(Prediccion prediccion)
        {
            if (prediccion != null)
            {
                _prediccionRepository.Remove(prediccion);
                _prediccionRepository.Save();
            }
        }
        */
    }
}

