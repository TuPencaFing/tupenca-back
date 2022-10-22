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
        private readonly EquipoService _equipoService;

        public EventoController(ILogger<EventoController> logger, EventoService eventoService, EquipoService equipoService)
        {
            _logger = logger;
            _eventoService = eventoService;
            _equipoService = equipoService;
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

        // GET: api/eventos/1        
        [HttpGet]
        [Route("api/eventos/{id:int}")]
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
        [Route("api/eventos")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Evento> CreateEvento(EventoDto eventoDto)
        {
            Evento evento = new Evento();
            evento.EquipoLocalId = eventoDto.EquipoLocalId;
            evento.EquipoVisitanteId = eventoDto.EquipoVisitanteId;
            evento.FechaInicial = eventoDto.FechaInicial;

            if (!_eventoService.IsEventoCorrect(evento))
                return BadRequest("No puede tener los mismos equipos enfrentados");

            if (!_equipoService.EquipoExists(evento.EquipoVisitanteId))
                return BadRequest("El equipo Visitante ingresado no existe");

            if (!_equipoService.EquipoExists(evento.EquipoLocalId))
                return BadRequest("El equipo Local ingresado no existe");

            if (!_eventoService.IsDateBeforeThan(DateTime.Now, evento.FechaInicial))
                return BadRequest("El evento debe ser en el futuro");

            _eventoService.CreateEvento(evento);
            evento.EquipoLocal = _equipoService.getEquipoById(evento.EquipoLocalId);
            evento.EquipoVisitante = _equipoService.getEquipoById(evento.EquipoVisitanteId);

            return CreatedAtAction("GetEventoById", new { id = evento.Id }, evento);
        }


        // PUT: api/eventos/1
        [HttpPut]
        [Route("api/eventos/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public ActionResult<Evento> UpdateEvento(int id, [FromBody] EventoFechaDto eventoFechaDto)
        {
            var evento = _eventoService.getEventoById(id);
            if (evento == null)
            {
                return NotFound();
            }

            if (!_eventoService.IsDateBeforeThan(DateTime.Now, eventoFechaDto.FechaInicial))
                return BadRequest("El evento debe ser en el futuro");

            evento.FechaInicial = eventoFechaDto.FechaInicial;
            _eventoService.UpdateEvento(evento);
            return Ok(evento);
        }


        // DELETE: api/eventos/1
        [HttpDelete]
        [Route("api/eventos/{id:int}")]
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

