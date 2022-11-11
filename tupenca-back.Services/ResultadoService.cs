using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class ResultadoService
    {
        private readonly IResultadoRepository _resultadoRepository;

        public ResultadoService(IResultadoRepository resultadoRepository)
        {
            _resultadoRepository = resultadoRepository;
        }

        public IEnumerable<Resultado> getResultados() => _resultadoRepository.GetAll();

        public Resultado? getResultadoById(int id) => _resultadoRepository.GetFirstOrDefault(e => e.Id == id);

        public Resultado? getResultadoByEventoId(int id) => _resultadoRepository.GetFirstOrDefault(e => e.EventoId == id);

        public void CreateResultado(Resultado resultado)
        {
            if (resultado != null)
            {
                _resultadoRepository.Add(resultado);
                _resultadoRepository.Save();
            }
        }

        public void UpdateResultado(Resultado resultado)
        {
            if (resultado != null)
            {
                _resultadoRepository.Update(resultado);
                _resultadoRepository.Save();
            }
        }

        public void RemoveResultado(Resultado resultado)
        {
            if (resultado != null)
            {
                _resultadoRepository.Remove(resultado);
                _resultadoRepository.Save();
            }
        }


    }
}

