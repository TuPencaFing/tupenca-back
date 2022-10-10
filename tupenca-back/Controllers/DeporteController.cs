using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;

namespace tupenca_back.Controllers
{
    [ApiController]    
    public class DeporteController : ControllerBase
    {
        private readonly ILogger<DeporteController> _logger;
        private readonly DeporteService _deporteService;

        public DeporteController(ILogger<DeporteController> logger, DeporteService deporteService)
        {
            _logger = logger;
            _deporteService = deporteService;
        }

        //GET: api/deportes        
        [HttpGet]
        [Route("api/deportes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Deporte>> GetDeportes()
        {
            var deportes = _deporteService.getDeportes();
            if (deportes == null)
            {
                return NoContent();
            }
            return Ok(deportes);
        }

        // GET: api/deportes/1        
        [HttpGet]
        [Route("api/deportes/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Deporte> GetDeporteById(int id)
        {           
            var deporte = _deporteService.getDeporteById(id);
            if (deporte == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(deporte);
            }
        }

        // GET: api/deportes/nombre        
        [HttpGet]
        [Route("api/deportes/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Deporte> GetDeporteByNombre(string nombre)
        {
            var deporte = _deporteService.getDeporteByNombre(nombre);
            if (deporte == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(deporte);
            }
        }
    }
}

