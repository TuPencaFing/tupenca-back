using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;

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
        public ActionResult<IEnumerable<Evento>> GetEventos()
        {
            var eventos = _eventoService.getEventos();
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

        // GET: api/evento/1        
        [HttpGet]
        [Route("api/evento/{id:int}")]
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


        // POST: api/evento        
        [HttpPost]
        [Route("api/evento/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Evento> CreateEvento(Evento evento)
        {
            if (_eventoService.EventoExists(evento.Id))            
                return BadRequest();
            
            if (!_eventoService.IsEventoCorrect(evento))            
                return BadRequest("No puede tener los mismos equipos enfrentados");

            if (!_eventoService.IsDateBeforeThan(DateTime.Now, evento.FechaInicial))            
                return BadRequest("El evento debe ser en el futuro");

            _eventoService.CreateEvento(evento);
            return CreatedAtAction("GetEventoById", new { id = evento.Id }, evento);
        }


        // PUT: api/evento/1
        [HttpPut]
        [Route("api/evento/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public ActionResult<Evento> UpdateEvento(int id, [FromBody] EventoDto eventoDto)
        {
            if (!eventoDto.FechaInicial.HasValue)
                return BadRequest();
            
            var fechaInicial = eventoDto.FechaInicial ?? DateTime.MinValue;

            if (!_eventoService.EventoExists(id))
                return NotFound();

            if (!_eventoService.IsDateBeforeThan(DateTime.Now, fechaInicial))
                return BadRequest("El evento debe ser en el futuro");

            var evento = _eventoService.getEventoById(id);
            evento.FechaInicial = fechaInicial;
            _eventoService.UpdateEvento(evento);
            return CreatedAtAction("GetEventoById", new { id = evento.Id }, evento);
        }


        // DELETE: api/evento/1
        [HttpDelete]
        [Route("api/evento/delete/{id:int}")]
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

