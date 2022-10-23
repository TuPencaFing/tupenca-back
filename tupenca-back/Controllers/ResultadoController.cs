using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [ApiController]
    public class ResultadoController : ControllerBase
    {
        private readonly ILogger<ResultadoController> _logger;
        private readonly ResultadoService _resultadoService;

        public ResultadoController(ILogger<ResultadoController> logger, ResultadoService resultadoService)
        {
            _logger = logger;
            _resultadoService = resultadoService;
        }

        // GET: api/resultados/1        
        [HttpGet]
        [Route("api/resultados/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Resultado> GetResultadoById(int id)
        {
            var resultado = _resultadoService.getResultadoById(id);
            if (resultado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(resultado);
            }
        }

        // GET: api/resultados/evento/1        
        [HttpGet]
        [Route("api/resultados/evento/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Resultado> GetResultadoByEventoId(int id)
        {
            var resultado = _resultadoService.getResultadoByEventoId(id);
            if (resultado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(resultado);
            }
        }

        // POST: api/resultados        
        [HttpPost]
        [Route("api/resultados")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Resultado> CreateResultado(ResultadoDto resultadoDto)
        {
            if (_resultadoService.getResultadoByEventoId(resultadoDto.EventoId) != null)
                return BadRequest("Ya existe resultado ingresado para el evento");
            try
            {               
                Resultado resultado = new Resultado();
                resultado.resultado = resultadoDto.resultado;
                resultado.PuntajeEquipoLocal = resultadoDto.PuntajeEquipoLocal;
                resultado.PuntajeEquipoVisitante = resultadoDto.PuntajeEquipoVisitante;
                resultado.EventoId = resultadoDto.EventoId;
                _resultadoService.CreateResultado(resultado);
                return CreatedAtAction("GetResultadoById", new { id = resultado.Id }, resultado);
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // PUT: api/resultados/1
        [HttpPut]
        [Route("api/resultados/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Resultado> UpdateResultado(int id, [FromBody] ResultadoUpdateDto resultadoDto)
        {
            var resultado = _resultadoService.getResultadoById(id);

            if (resultado == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest();

            resultado.resultado = resultadoDto.resultado;
            resultado.PuntajeEquipoLocal = resultadoDto.PuntajeEquipoLocal;
            resultado.PuntajeEquipoVisitante = resultadoDto.PuntajeEquipoVisitante;
            _resultadoService.UpdateResultado(resultado);
            return CreatedAtAction("GetResultadoById", new { id = resultado.Id }, resultado);
        }

        // DELETE: api/resultados/1
        [HttpDelete]
        [Route("api/resultados/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteResultado(int id)
        {
            var resultado = _resultadoService.getResultadoById(id);

            if (resultado == null)
            {
                return NotFound();
            }

            _resultadoService.RemoveResultado(resultado);

            return NoContent();
        }

    }
}

