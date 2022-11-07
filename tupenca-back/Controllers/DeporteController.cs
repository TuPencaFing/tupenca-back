using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace tupenca_back.Controllers
{
 
    [Authorize]
    [ApiController]
    [Route("api/deportes")]
    public class DeporteController : ControllerBase
    {
        private readonly ILogger<DeporteController> _logger;
        public readonly IMapper _mapper;
        private readonly DeporteService _deporteService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public DeporteController(ILogger<DeporteController> logger,
                                 IMapper mapper,
                                 DeporteService deporteService,
                                 IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _mapper = mapper;
            _deporteService = deporteService;
            this._hostEnvironment = hostEnvironment;
        }

        //GET: api/deportes        
        [HttpGet]
        public ActionResult<IEnumerable<DeporteDto>> GetDeportes()
        {
            var deportes = _deporteService.getDeportes();
            var deportesDto = _mapper.Map<List<DeporteDto>>(deportes);
            return Ok(deportesDto);
        }

        // GET: api/deportes/1        
        [HttpGet("{id}")]
        public ActionResult<DeporteDto> GetDeporteById(int id)
        {
            var deporte = _deporteService.getDeporteById(id);
            if (deporte == null)
            {                
                return NotFound();
            }
            else
            {
                var deporteDto = _mapper.Map<DeporteDto>(deporte);
                return Ok(deporteDto);
            }
        }

        // GET: api/deportes/nombre        
        [HttpGet("{nombre}")]
        public ActionResult<DeporteDto> GetDeporteByNombre(string nombre)
        {
            var deporte = _deporteService.getDeporteByNombre(nombre);
            if (deporte == null)
            {
                return NotFound();
            }
            else
            {
                var deporteDto = _mapper.Map<DeporteDto>(deporte);
                return Ok(deporteDto);
            }
        }

        // POST: api/deportes        
        [HttpPost]
        public ActionResult<Deporte> CreateDeporte(DeporteDto deporteDto)
        {
            if (_deporteService.DeporteNombreExists(deporteDto.Nombre))
               return BadRequest("Ya existe el deporte");

            try
            {
                var deporte = _mapper.Map<Deporte>(deporteDto);
                _deporteService.CreateDeporte(deporte);

                return CreatedAtAction("GetDeporteById", new { id = deporte.Id }, _mapper.Map<DeporteDto>(deporte));
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // PUT: api/deportes/1
        [HttpPut("{id}")]
        public ActionResult<Deporte> UpdateEquipo(int id, DeporteDto deporteDto)
        {
            var deporte = _deporteService.getDeporteById(id);

            if (deporte == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
                return BadRequest();

            if (_deporteService.DeporteNombreExists(deporteDto.Nombre))
                return BadRequest("Ya existe el deporte");

            deporte.Nombre = deporteDto.Nombre;
            _deporteService.UpdateDeporte(deporte);
            return NoContent();
        }

        // DELETE: api/deportes/1
        [HttpDelete("{id}")]
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


        // PATCH: api/deportes/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _deporteService.SaveImagen(id, imagenDto.file);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}

