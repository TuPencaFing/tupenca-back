using Microsoft.AspNetCore.Http;
using tupenca_back.DataAccess.Repository;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class EventoService
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IUsuarioPencaRepository _usuariopencaRepository;
        private readonly UsuarioService _usuarioService;
        private readonly ImagesService _imagesService;


        public EventoService(IEventoRepository eventoRepository,
                             UsuarioService usuarioService,
                             IUsuarioPencaRepository usuariopencaRepository,
                             ImagesService imagesService)
        {
            _eventoRepository = eventoRepository;
            _usuariopencaRepository = usuariopencaRepository;
            _usuarioService = usuarioService;
            _imagesService = imagesService;
        }

        public IEnumerable<Evento> getEventos() => _eventoRepository.GetEventos();

        public Evento? getEventoById(int? id) => _eventoRepository.GetFirst(e => e.Id == id);

        public void CreateEvento(Evento evento)
        {
            if (evento != null)
            {
                _eventoRepository.Add(evento);
                _eventoRepository.Save();
            }
        }

        public void UpdateEvento(Evento evento)
        {
            if (evento != null)
            {
                _eventoRepository.Update(evento);
                _eventoRepository.Save();
            }
        }

        public void RemoveEvento(Evento evento)
        {
            if (evento != null)
            {
                _eventoRepository.Remove(evento);
                _eventoRepository.Save();
            }
        }

        public IEnumerable<Evento> GetEventosProximos()
        {
            return _eventoRepository.GetEventosProximos();        
        }


        public IEnumerable<Evento> GetEventosProximosPencaCompartida(int userId, int pencaid)
        {
            var usuario = _usuarioService.find(userId);
            return _usuariopencaRepository.GetEventosProximosPencaCompartida(userId, pencaid);
        }

        public void SaveImagen(int id, IFormFile file)
        {
            var evento = getEventoById(id);

            string image = _imagesService.uploadImage(file.FileName, file.OpenReadStream());

            evento.Image = image;

            UpdateEvento(evento);
        }



        public bool IsEventoCorrect(Evento evento)
        {
            if (evento.EquipoLocalId != evento.EquipoVisitanteId)
            {
                return true;
            }
            else return false;
        }

        public bool IsDateBeforeThan(DateTime fechaInicial, DateTime fechaFinal)
        {
            if (DateTime.Compare(fechaInicial, fechaFinal) < 0)
            {
                return true;
            }
            else return false;
        }


    }
}

