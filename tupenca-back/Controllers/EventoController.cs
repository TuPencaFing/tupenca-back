using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;

namespace tupenca_back.Controllers
{
    [ApiController]    
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;
        private readonly EventoService _eventoService;

        public EventoController(ILogger<EventoController> logger, EventoService eventoService)
        {
            _logger = logger;
            _eventoService = eventoService;
        }

        //GET: api/eventos        
        [HttpGet]
        [Route("api/eventos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Evento>> GetEventos()
        {
            var eventos = _eventoService.getEventos();
            if (eventos == null)
            {
                return NoContent();
            }
            return Ok(eventos);
        }

        //GET: api/eventos/proximos        
        [HttpGet]
        [Route("api/eventos/proximos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Evento>> GetEventosProximos()
        {
            var eventos = _eventoService.GetEventosProximos();
            if (eventos == null)
            {
                return NoContent();
            }
            return Ok(eventos);
        }

        // GET: api/eventos/1        
        [HttpGet]
        [Route("api/eventos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Evento> GetEventoById(int id)
        {           
            var evento = _eventoService.getEventoById(id);
            if (evento == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(evento);
            }
        }


        // POST: api/eventos        
        [HttpPost]
        [Route("api/eventos/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Evento> CreateEvento(Evento evento)
        {
            if (_eventoService.EventoExists(evento.Id))            
                return BadRequest();
            
            if (!_eventoService.IsEventoCorrect(evento))            
                return BadRequest("No puede tener los mismos equipos enfrentados");
            
            if (!_eventoService.IsDateCorrect(evento.FechaFinal, evento.FechaFinal))            
                return BadRequest("La fecha final no puede ser anterior a la fecha inicial");
            
            if (!_eventoService.IsDateCorrect(evento.FechaFinal, DateTime.Now))            
                return BadRequest("El evento debe ser en el futuro");
            _eventoService.CreateEvento(evento);
            return CreatedAtAction("GetEventoById", new { id = evento.Id }, evento);
        }


        // PUT: api/eventos/1
        [HttpPut("{id}")]
        [Route("api/eventos/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public ActionResult<Evento> UpdateEvento(int id, [FromBody] Evento evento)
        {
            if (id != evento.Id)
                return BadRequest();
            
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_eventoService.EventoExists(id))
                return NotFound();

            if (!_eventoService.IsEventoCorrect(evento))
                return BadRequest("No puede tener los mismos equipos enfrentados");

            if (!_eventoService.IsDateCorrect(evento.FechaFinal, evento.FechaFinal))
                return BadRequest("La fecha final no puede ser anterior a la fecha inicial");

            if (!_eventoService.IsDateCorrect(evento.FechaFinal, DateTime.Now))
                return BadRequest("El evento debe ser en el futuro");

            _eventoService.UpdateEvento(evento);
            return CreatedAtAction("GetEventoById", new { id = evento.Id }, evento);
        }


        // DELETE: api/evento/1
        [HttpDelete]
        [Route("api/evento/delete/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEvento(int id)
        {
            var evento = _eventoService.getEventoById(id);

            if (evento == null)
            {
                return NotFound();
            }
            _eventoService.RemoveEvento(evento);
            return NoContent();
        }

    }
}

