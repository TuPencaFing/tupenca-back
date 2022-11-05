using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;
        private readonly EventoService _eventoService;
        private readonly EquipoService _equipoService;
        private readonly PrediccionService _prediccionService;

        public EventoController(ILogger<EventoController> logger, EventoService eventoService, EquipoService equipoService, PrediccionService prediccionService)
        {
            _logger = logger;
            _eventoService = eventoService;
            _equipoService = equipoService;
            _prediccionService = prediccionService;
        }

        //GET: api/eventos        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Evento>> GetEventos()
        {
            var eventos = _eventoService.getEventos();
            return Ok(eventos);
        }

        //GET: api/eventos/proximos        
        [HttpGet("proximos")]
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

        //GET: api/eventos/proximos        
        [HttpGet("misproximos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<EventoPrediccionDto>> GetEventosProximosPencaCompartidaUsuario()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);         
            var eventos = _eventoService.GetEventosProximosPencaCompartida(Convert.ToInt32(userId));
            if (eventos == null)
            {
                return NoContent();
            }

            List<EventoPrediccionDto> eventosRet = new List<EventoPrediccionDto>();            

            foreach (var evento in eventos)
            {
                var prediccion = _prediccionService.GetPrediccionByUsuarioEvento(Convert.ToInt32(userId), evento.Id);
                var equipolocal = _equipoService.getEquipoById(evento.EquipoLocalId);
                var equipovisitante = _equipoService.getEquipoById(evento.EquipoVisitanteId);
                EventoPrediccionDto eventoret = new EventoPrediccionDto { Id = evento.Id, EquipoLocal = equipolocal,
                                                                         EquipoVisitante = equipovisitante, FechaInicial = evento.FechaInicial,
                                                                         Prediccion = prediccion};
                eventosRet.Add(eventoret);
            }
            return Ok(eventosRet);
        }

        // GET: api/eventos/1        
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
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

        //Prediccion
        // POST: api/eventos/1/prediccion
        // 
        [HttpPost("{id}/prediccion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Prediccion> CreatePrediccionEvento(int id, PrediccionDto prediccionDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var evento = _eventoService.getEventoById(id);
            if (evento == null)
            {
                return NotFound("No existe el evento");
            }
            var prediccionExistente = _prediccionService.getPrediccionByEventoId(id);
            if (prediccionExistente != null)
            {
                prediccionExistente.prediccion = prediccionDto.resultado;
                prediccionExistente.PuntajeEquipoLocal = prediccionDto.PuntajeEquipoLocal;
                prediccionExistente.PuntajeEquipoVisitante = prediccionDto.PuntajeEquipoVisitante;
                _prediccionService.UpdatePrediccion(prediccionExistente);
                return Ok(prediccionExistente);
            }
            else
            {
                Prediccion prediccion = new Prediccion();
                prediccion.prediccion = prediccionDto.resultado;
                prediccion.PuntajeEquipoLocal = prediccionDto.PuntajeEquipoLocal;
                prediccion.PuntajeEquipoVisitante = prediccionDto.PuntajeEquipoVisitante;
                prediccion.EventoId = id;
                prediccion.UsuarioId = Convert.ToInt32(userId);
                _prediccionService.CreatePrediccion(prediccion);
                return CreatedAtAction("GetPrediccionById", "Prediccion", new { id = prediccion.Id }, prediccion);
            }

        }

        // PATCH: api/deportes/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _eventoService.SaveImagen(id, imagenDto.file);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}

