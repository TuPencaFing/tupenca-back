using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class EquipoService
    {
        private readonly IEquipoRepository _equipoRepository;

        public EquipoService(IEquipoRepository equipoRepository)
        {
            _equipoRepository = equipoRepository;
        }

        public IEnumerable<Equipo> getEquipos() => _equipoRepository.GetAll();

        public Equipo? getEquipoById(int id) => _equipoRepository.GetFirstOrDefault(e => e.Id == id);

        public Equipo? getEquipoByNombre(string Nombre) => _equipoRepository.GetFirstOrDefault(e => e.Nombre == Nombre);

        public void CreateEquipo(Equipo equipo)
        {
            if (equipo != null)
            {
                _equipoRepository.Add(equipo);
                _equipoRepository.Save();
            }
        }

        public void UpdateEquipo(Equipo equipo)
        {
            if (equipo != null)
            {
                _equipoRepository.Update(equipo);
                _equipoRepository.Save();
            }
        }

        public void RemoveEquipo(Equipo equipo)
        {
            if (equipo != null)
            {
                _equipoRepository.Remove(equipo);
                _equipoRepository.Save();
            }
        }

        public bool EquipoNombreExists(string nombre)
        {
            if (getEquipoByNombre(nombre) == null) return false;
            else return true;
        }

        public bool EquipoExists(int id)
        {
            if (getEquipoById(id) == null) return false;
            else return true;
        }

    }
}

