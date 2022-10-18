using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;

namespace tupenca_back.Controllers
{
    [ApiController]    
    public class EquipoController : ControllerBase
    {
        private readonly ILogger<EquipoController> _logger;
        private readonly EquipoService _equipoService;

        public EquipoController(ILogger<EquipoController> logger, EquipoService equipoService)
        {
            _logger = logger;
            _equipoService = equipoService;
        }

        //GET: api/equipos        
        [HttpGet]
        [Route("api/equipos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Equipo>> GetEquipos()
        {
            var equipos = _equipoService.getEquipos();
            return Ok(equipos);
        }

        // GET: api/equipos/1        
        [HttpGet]
        [Route("api/equipos/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Equipo> GetEquipoById(int id)
        {           
            var equipo = _equipoService.getEquipoById(id);
            if (equipo == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(equipo);
            }
        }

        // GET: api/equipos/nombre        
        [HttpGet]
        [Route("api/equipos/{nombre}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Equipo> GetEquipoByNombre(string nombre)
        {
            var equipo = _equipoService.getEquipoByNombre(nombre);
            if (equipo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(equipo);
            }
        }

        // POST: api/equipos        
        [HttpPost]
        [Route("api/equipos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Equipo> CreateEquipo(Equipo equipo)
        {
            if (_equipoService.EquipoExists(equipo.Id))            
                return BadRequest("Ya existe el equipo");
            
            if (_equipoService.EquipoNombreExists(equipo.Nombre))          
                return BadRequest("Ya existe el equipo");

            _equipoService.CreateEquipo(equipo);
            return CreatedAtAction("GetEquipoById", new { id = equipo.Id }, equipo);
        }

        // PUT: api/equipos/1
        [HttpPut]
        [Route("api/equipos/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public ActionResult<Equipo> UpdateEquipo(int id, [FromBody] Equipo equipo)
        {
            if (id != equipo.Id)
                return BadRequest();
            
            if (!ModelState.IsValid)
                return BadRequest();

            if (!_equipoService.EquipoExists(id))
                return NotFound("No existe el equipo");

            if (_equipoService.EquipoNombreExists(equipo.Nombre))
                return BadRequest("Ya existe el equipo");

            _equipoService.UpdateEquipo(equipo);
            return CreatedAtAction("GetEquipoById", new { id = equipo.Id }, equipo);
        }

        // DELETE: api/equipo/1
        [HttpDelete]
        [Route("api/equipos/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEquipo(int id)
        {
            var equipo = _equipoService.getEquipoById(id);

            if (equipo == null)
            {
                return NotFound();
            }
            _equipoService.RemoveEquipo(equipo);
            return NoContent();
        }

    }
}

