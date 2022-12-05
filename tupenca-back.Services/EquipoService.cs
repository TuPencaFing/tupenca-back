using Microsoft.AspNetCore.Http;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class EquipoService
    {
        private readonly IEquipoRepository _equipoRepository;
        private readonly ImagesService _imagesService;

        public EquipoService(IEquipoRepository equipoRepository,
                             ImagesService imagesService)
        {
            _equipoRepository = equipoRepository;
            _imagesService = imagesService;
        }

        public IEnumerable<Equipo> getEquipos() => _equipoRepository.GetAll();

        public Equipo? getEquipoById(int id) => _equipoRepository.GetFirstOrDefault(e => e.Id == id);

        public Equipo? getEquipoByNombre(string Nombre) => _equipoRepository.GetFirstOrDefault(e => e.Nombre == Nombre);

        public IEnumerable<Equipo>? SearchEquipos(string searchString) => _equipoRepository.SearchEquipo(searchString); 

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

        public void SaveImagen(int id, IFormFile file)
        {
            var equipo = getEquipoById(id);

            string image = _imagesService.uploadImage(file.FileName, file.OpenReadStream());

            equipo.Image = image;

            UpdateEquipo(equipo);
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

