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
        private readonly IWebHostEnvironment _hostEnvironment;

        public DeporteController(ILogger<DeporteController> logger, DeporteService deporteService, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _deporteService = deporteService;
            this._hostEnvironment = hostEnvironment;
        }

        //GET: api/deportes        
        [HttpGet]
        [Route("api/deportes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Deporte>> GetDeportes()
        {
            var deportes = _deporteService.getDeportes();
            return Ok(deportes);
        }

        // GET: api/deporte/1        
        [HttpGet]
        [Route("api/deporte/{id:int}")]
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

        // GET: api/deporte/nombre        
        [HttpGet]
        [Route("api/deporte/{nombre}")]
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

        // POST: api/deporte        
        [HttpPost]
        [Route("api/deporte/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Deporte> CreateDeporte(Deporte deporte)
        {
            if (_deporteService.DeporteNombreExists(deporte.Nombre))
                return BadRequest("Ya existe el deporte");

            _deporteService.CreateDeporte(deporte);
            return CreatedAtAction("GetDeporteById", new { id = deporte.Id }, deporte);
        }

        // PUT: api/deporte/1
        [HttpPut]
        [Route("api/deporte/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Deporte> UpdateEquipo(int id, [FromBody] Deporte deporte)
        {
            if (id != deporte.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (_deporteService.DeporteNombreExists(deporte.Nombre))
                return BadRequest("Ya existe el deporte");

            _deporteService.UpdateDeporte(deporte);
            return CreatedAtAction("GetDeporteById", new { id = deporte.Id }, deporte);
        }

        // DELETE: api/deporte/1
        [HttpDelete]
        [Route("api/deporte/delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDeporte(int id)
        {
            var deporte = _deporteService.getDeporteById(id);

            if (deporte == null)
            {
                return NotFound();
            }
            _deporteService.RemoveDeporte(deporte);
            return NoContent();
        }

    }
}

