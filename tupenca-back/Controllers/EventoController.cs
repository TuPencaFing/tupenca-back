using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net;
using tupenca_back.Services.Exceptions;
using tupenca_back.Services.Dto;
using AutoMapper;

namespace tupenca_back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/eventos")]
    public class EventoController : ControllerBase
    {
        private readonly ILogger<EventoController> _logger;
        public readonly IMapper _mapper;

        private readonly EventoService _eventoService;
        private readonly EquipoService _equipoService;
        private readonly PrediccionService _prediccionService;

        public EventoController(IMapper mapper,
ILogger<EventoController> logger, EventoService eventoService, EquipoService equipoService, PrediccionService prediccionService)
        {
            _logger = logger;
            _eventoService = eventoService;
            _equipoService = equipoService;
            _prediccionService = prediccionService;
            _mapper = mapper;
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
        public ActionResult<IEnumerable<EventoPrediccionDto>> GetEventosProximosPencaCompartidaUsuario([FromQuery] int penca)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);         
            var eventos = _eventoService.GetEventosProximosPencaCompartida(Convert.ToInt32(userId), penca);
            if (eventos == null)
            {
                return NoContent();
            }

            List<EventoPrediccionDto> eventosRet = new List<EventoPrediccionDto>();

            foreach (var evento in eventos)
            {
                var prediccion = _prediccionService.GetPrediccionByUsuarioEvento(Convert.ToInt32(userId), evento.Id, penca);
                var equipolocal = _equipoService.getEquipoById(evento.EquipoLocalId);
                var equipovisitante = _equipoService.getEquipoById(evento.EquipoVisitanteId);
                var equipoLocalDto = _mapper.Map<EquipoDto>(equipolocal);
                var equipoVisitanteDto = _mapper.Map<EquipoDto>(equipovisitante);
                var prediccionDto = _mapper.Map<PrediccionDto>(prediccion);
                EventoPrediccionDto eventoret = new EventoPrediccionDto { Id = evento.Id, EquipoLocal = equipoLocalDto,
                                                                         EquipoVisitante = equipoVisitanteDto, FechaInicial = evento.FechaInicial,
                                                                         Prediccion = prediccionDto, IsEmpateValid = evento.IsEmpateValid,
                                                                         IsPuntajeEquipoValid = evento.IsPuntajeEquipoValid
                };



                var cantEmpate = 0;
                var cantVictoriaLocal = 0;
                var cantVictoriaVisitante = 0;
                var predicciones = _prediccionService.getPrediccionesByEventoAndPenca(evento.Id, penca);
                foreach (var elem in predicciones)
                {
                    if (elem != null)
                    {
                        if (elem.prediccion == 0) cantEmpate++;
                        if (elem.prediccion == TipoResultado.VictoriaEquipoLocal) cantVictoriaLocal++;
                        if (elem.prediccion == TipoResultado.VictoriaEquipoVisitante) cantVictoriaVisitante++;
                    }
                    var totalpred = cantEmpate + cantVictoriaLocal + cantVictoriaVisitante;
                    eventoret.PorcentajeEmpate = ((cantEmpate * 100) / totalpred);
                    eventoret.PorcentajeLocal = ((cantVictoriaLocal * 100) / totalpred);
                    eventoret.PorcentajeVisitante = ((cantVictoriaVisitante * 100) / totalpred);
                }

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

            if (!_eventoService.IsDateBeforeThan(DateTime.UtcNow, evento.FechaInicial))
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

            if (!_eventoService.IsDateBeforeThan(DateTime.UtcNow, eventoFechaDto.FechaInicial))
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
        public ActionResult<Prediccion> CreatePrediccionEvento(int id, PrediccionDto prediccionDto, [FromQuery] int pencaId)
        {   var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var evento = _eventoService.getEventoById(id);

            if (DateTime.UtcNow > evento.FechaInicial) return BadRequest("Ya es tarde para pronosticar este evento");

            if (evento == null)
            {
                return NotFound("No existe el evento");
            }
            var prediccionExistente = _prediccionService.getPrediccionByEventoAndPencaAndUsuario(id, pencaId, Convert.ToInt32(userId));
            if (prediccionExistente != null)
            {
                prediccionExistente.prediccion = prediccionDto.prediccion;
                prediccionExistente.PuntajeEquipoLocal = prediccionDto.PuntajeEquipoLocal;
                prediccionExistente.PuntajeEquipoVisitante = prediccionDto.PuntajeEquipoVisitante;
                _prediccionService.UpdatePrediccion(prediccionExistente);
                return Ok(prediccionExistente);
            }
            else
            {
                Prediccion prediccion = new Prediccion();
                prediccion.prediccion = prediccionDto.prediccion;
                prediccion.PuntajeEquipoLocal = prediccionDto.PuntajeEquipoLocal;
                prediccion.PuntajeEquipoVisitante = prediccionDto.PuntajeEquipoVisitante;
                prediccion.EventoId = id;
                prediccion.UsuarioId = Convert.ToInt32(userId);
                prediccion.PencaId = pencaId;
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


        //GET: api/eventos/finalizados        
        [HttpGet("finalizados")]
        public ActionResult<IEnumerable<EventoResultado>> GetEventosResultadoFinalizados()
        {
            var eventos = _eventoService.GetEventosResultadoFinalizados();
            if (eventos == null)
            {
                return NoContent();
            }
            return Ok(eventos);
        }

    }
}

