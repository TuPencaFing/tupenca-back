using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.Model;

namespace tupenca_back.Services
{
    public class EventoService
    {
        private readonly IEventoRepository _eventoRepository;

        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
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

