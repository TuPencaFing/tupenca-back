using Microsoft.AspNetCore.Http;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class DeporteService
    {
        private readonly IDeporteRepository _deporteRepository;
        private readonly ImagesService _imagesService;

        public DeporteService(IDeporteRepository deporteRepository, ImagesService imagesService)
        {
            _deporteRepository = deporteRepository;
            _imagesService = imagesService;
        }

        public IEnumerable<Deporte> getDeportes() => _deporteRepository.GetAll();

        public Deporte? getDeporteById(int id) => _deporteRepository.GetFirstOrDefault(e => e.Id == id);

        public Deporte? getDeporteByNombre(string Nombre) => _deporteRepository.GetFirstOrDefault(e => e.Nombre == Nombre);

        public void CreateDeporte(Deporte deporte)
        {
            if (deporte != null)
            {
                _deporteRepository.Add(deporte);
                _deporteRepository.Save();
            }
        }

        public void UpdateDeporte(Deporte deporte)
        {
            if (deporte != null)
            {
                _deporteRepository.Update(deporte);
                _deporteRepository.Save();
            }
        }

        public void RemoveDeporte(Deporte deporte)
        {
            if (deporte != null)
            {
                _deporteRepository.Remove(deporte);
                _deporteRepository.Save();
            }
        }

        public bool DeporteNombreExists(string nombre)
        {
            if (getDeporteByNombre(nombre) == null) return false;
            else return true;
        }


        public void SaveImagen(int id, IFormFile file)
        {
            var deporte = getDeporteById(id);

            string image = _imagesService.uploadImage(file.FileName, file.OpenReadStream());

            deporte.ImagenName = image;

            UpdateDeporte(deporte);
        }
    }
}

