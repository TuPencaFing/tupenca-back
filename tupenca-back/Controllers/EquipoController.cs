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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<IEnumerable<Equipo>> GetEquipos()
        {
            var equipos = _equipoService.getEquipos();
            if (equipos == null)
            {
                return NoContent();
            }
            return Ok(equipos);
        }

        // GET: api/equipo/1        
        [HttpGet]
        [Route("api/equipo/{id}")]
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

        // GET: api/equipo/nombre        
        [HttpGet]
        [Route("api/equipo/{nombre}")]
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

        // POST: api/equipo        
        [HttpPost]
        [Route("api/equipo/create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Equipo> CreateEquipo(Equipo equipo)
        {
            if (_equipoService.EquipoExists(equipo.Id))            
                return BadRequest();
            
            if (!_equipoService.EquipoNombreExists(equipo.Nombre))          
                return BadRequest("No puede tener los mismos equipos enfrentados");

            _equipoService.CreateEquipo(equipo);
            return CreatedAtAction("GetEquipoById", new { id = equipo.Id }, equipo);
        }

        // PUT: api/equipo/1
        [HttpPut]
        [Route("api/equipo/{id}")]
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
                return NotFound();

            if (!_equipoService.EquipoNombreExists(equipo.Nombre))
                return BadRequest("No puede tener los mismos equipos enfrentados");

            _equipoService.UpdateEquipo(equipo);
            return CreatedAtAction("GetEquipoById", new { id = equipo.Id }, equipo);
        }

        // DELETE: api/equipo/1
        [HttpDelete]
        [Route("api/equipo/delete/{id}")]
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

