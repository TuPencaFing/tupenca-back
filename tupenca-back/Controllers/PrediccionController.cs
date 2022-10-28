using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Authorize]
    public class PrediccionController : ControllerBase
    {
        private readonly ILogger<PrediccionController> _logger;
        private readonly PrediccionService _prediccionService;
        private readonly ResultadoService _resultadoService;

        public PrediccionController(ILogger<PrediccionController> logger, PrediccionService prediccionService, ResultadoService resultadoService)
        {
            _logger = logger;
            _prediccionService = prediccionService;
            _resultadoService = resultadoService;
        }

        // GET: api/predicciones/1        
        [HttpGet]
        [Route("api/predicciones/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Prediccion> GetPrediccionById(int id)
        {
            var prediccion = _prediccionService.getPrediccionById(id);
            if (prediccion == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prediccion);
            }
        }

        // GET: api/predicciones/evento/1        
        [HttpGet]
        [Route("api/predicciones/evento/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Prediccion> GetPrediccionByEventoId(int id)
        {
            var prediccion = _prediccionService.getPrediccionByEventoId(id);
            if (prediccion == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prediccion);
            }
        }

        // GET: api/predicciones/evento/1/puntaje        
        [HttpGet]
        [Route("api/predicciones/evento/{id:int}/puntaje")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<bool> GetPuntajePrediccion(int id)
        {
            var prediccion = _prediccionService.getPrediccionByEventoId(id);
            if (prediccion == null)
            {
                return NotFound();
            }
            else
            {
                var resultado = _resultadoService.getResultadoByEventoId(id);
                if (resultado == null)
                {
                    return BadRequest("No existe resultado del evento");
                }                
                return Ok(_prediccionService.isPrediccionCorrect(prediccion, resultado));
            }
        }

        // POST: api/predicciones        
        [HttpPost]
        [Route("api/predicciones")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Prediccion> CreatePrediccion(ResultadoDto prediccionDto)
        {
            if (_prediccionService.getPrediccionByEventoId(prediccionDto.EventoId) != null)
                return BadRequest("Ya existe prediccion ingresada para el evento");
            try
            {               
                Prediccion prediccion = new Prediccion();
                prediccion.prediccion = prediccionDto.resultado;
                prediccion.PuntajeEquipoLocal = prediccionDto.PuntajeEquipoLocal;
                prediccion.PuntajeEquipoVisitante = prediccionDto.PuntajeEquipoVisitante;
                prediccion.EventoId = prediccionDto.EventoId;
                prediccion.UsuarioId = prediccionDto.UsuarioId;
                _prediccionService.CreatePrediccion(prediccion);
                return CreatedAtAction("GetPrediccionById", new { id = prediccion.Id }, prediccion);
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // PUT: api/predicciones/1
        [HttpPut]
        [Route("api/predicciones/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Prediccion> UpdatePrediccion(int id, [FromBody] ResultadoUpdateDto prediccionDto)
        {
            var prediccion = _prediccionService.getPrediccionById(id);

            if (prediccion == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest();

            prediccion.prediccion = prediccionDto.resultado;
            prediccion.PuntajeEquipoLocal = prediccionDto.PuntajeEquipoLocal;
            prediccion.PuntajeEquipoVisitante = prediccionDto.PuntajeEquipoVisitante;
            _prediccionService.UpdatePrediccion(prediccion);
            return CreatedAtAction("GetPrediccionById", new { id = prediccion.Id }, prediccion);
        }

        /*
        // DELETE: api/predicciones/1
        [HttpDelete]
        [Route("api/predicciones/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePrediccion(int id)
        {
            var prediccion = _prediccionService.getPrediccionById(id);

            if (prediccion == null)
            {
                return NotFound();
            }

            _prediccionService.RemovePrediccion(prediccion);

            return NoContent();
        }
        */

    }
}

