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
    [Route("api/equipos")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Equipo>?> GetEquipos([FromQuery] string? searchString = null)
        {
            if (searchString == null)
            {
                var equipos = _equipoService.getEquipos();
                return Ok(equipos);
            }
            else
            {
                var equipos = _equipoService.SearchEquipos(searchString);
                return Ok(equipos);
            }

        }


        // GET: api/equipos/1        
        [HttpGet("{id:int}")]
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
        [HttpGet("{nombre}")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Equipo> CreateEquipo(EquipoDto equipoDto)
        {           
            if (_equipoService.EquipoNombreExists(equipoDto.Nombre))          
                //return BadRequest("Ya existe el equipo");
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Ya existe el equipo");

            Equipo equipo = new Equipo();
            equipo.Nombre = equipoDto.Nombre;
            _equipoService.CreateEquipo(equipo);
            return CreatedAtAction("GetEquipoById", new { id = equipo.Id }, equipo);
        }


        // PUT: api/equipos/1
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]        
        public ActionResult<Equipo> UpdateEquipo(int id, [FromBody] EquipoDto equipoDto)
        {
            var equipo = _equipoService.getEquipoById(id);
            if (equipo == null)
            {
                return NotFound("No existe el equipo");
            }
            if (_equipoService.EquipoNombreExists(equipoDto.Nombre))
                return BadRequest("Ya existe el equipo");

            equipo.Nombre = equipoDto.Nombre;
            _equipoService.UpdateEquipo(equipo);
            return Ok(equipo);                                    
        }

        // DELETE: api/equipo/1
        [HttpDelete("{id}")]
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


        // PATCH: api/equipos/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _equipoService.SaveImagen(id, imagenDto.file);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}

