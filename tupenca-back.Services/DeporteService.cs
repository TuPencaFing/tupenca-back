using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class DeporteService
    {
        private readonly IDeporteRepository _deporteRepository;

        public DeporteService(IDeporteRepository deporteRepository)
        {
            _deporteRepository = deporteRepository;
        }

        public IEnumerable<Deporte> getDeportes() => _deporteRepository.GetAll();

        public Deporte? getDeporteById(int id) => _deporteRepository.GetFirstOrDefault(e => e.Id == id);

        public Deporte? getDeporteByNombre(string Nombre) => _deporteRepository.GetFirstOrDefault(e => e.Nombre == Nombre);

    }
}

