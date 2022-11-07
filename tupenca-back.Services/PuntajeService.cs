using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class PuntajeService
    {
        private readonly IPuntajeRepository _puntajeRepository;

        public PuntajeService(IPuntajeRepository puntajeRepository)
        {
            _puntajeRepository = puntajeRepository;
        }

        public IEnumerable<Puntaje> getPuntajes() => _puntajeRepository.GetAll();

        public Puntaje? getPuntajeById(int id) => _puntajeRepository.GetFirstOrDefault(e => e.Id == id);

        public void CreatePuntaje(Puntaje puntaje)
        {
            if (puntaje != null)
            {
                _puntajeRepository.Add(puntaje);
                _puntajeRepository.Save();
            }
        }

        public void RemovePuntaje(Puntaje puntaje)
        {
            if (puntaje != null)
            {
                _puntajeRepository.Remove(puntaje);
                _puntajeRepository.Save();
            }
        }

    }
}

