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

        // GET: api/deportes/1        
        [HttpGet]
        [Route("api/deportes/{id:int}")]
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

        // POST: api/deportes        
        [HttpPost]
        [Route("api/deportes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Deporte> CreateDeporte(DeporteDto deporteDto)
        {
            if (_deporteService.DeporteNombreExists(deporteDto.Nombre))
               return BadRequest("Ya existe el deporte");
            try
            {               
                Deporte deporte = new Deporte();
                deporte.Nombre = deporteDto.Nombre;
                _deporteService.CreateDeporte(deporte);
                return CreatedAtAction("GetDeporteById", new { id = deporte.Id }, deporte);
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // PUT: api/deportes/1
        [HttpPut]
        [Route("api/deportes/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Deporte> UpdateEquipo(int id, [FromBody] DeporteDto deporteDto)
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
            return CreatedAtAction("GetDeporteById", new { id = deporte.Id }, deporte);
        }

        // DELETE: api/deportes/1
        [HttpDelete]
        [Route("api/deportes/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDeporte(int id)
        {
            var deporte = _deporteService.getDeporteById(id);

            if (deporte == null)
            {
                return NotFound();
            }

            if (deporte.ImagenName != null)
            {
                var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", deporte.ImagenName);
                System.IO.File.Delete(path);
            }        

            _deporteService.RemoveDeporte(deporte);

            return NoContent();
        }



        // POST: api/deportes/Image        
        [HttpPost]
        [Route("api/deportes/Image/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UploadDeporteImage(int id, [FromForm] ImagenDto imagenDto)
        {           
            var deporte = _deporteService.getDeporteById(id);
            if (deporte == null)
            {
                return NotFound();
            }
            var filepath = await SaveImagen(imagenDto.file);
            deporte.ImagenName= filepath;
            _deporteService.UpdateDeporte(deporte);
            return CreatedAtAction("GetDeporteById", new { id = deporte.Id }, deporte);
        }


        [HttpGet]
        [Route("api/deportes/Image/{id:int}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var deporte = _deporteService.getDeporteById(id);
            if (deporte == null)
            {
                return NotFound();
            }
            var commonpath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            
            Byte[] b;
            b = await System.IO.File.ReadAllBytesAsync(Path.Combine(commonpath, deporte.ImagenName));
            return File(b, "image/jpeg");
        }


        // DELETE: api/deporte/Image/1
        [HttpDelete]
        [Route("api/deportes/delete/Image/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteDeporteImage(int id)
        {
            var deporte = _deporteService.getDeporteById(id);

            if (deporte == null)
            {
                return NotFound();
            }

            if (deporte.ImagenName != null)
            {
                var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", deporte.ImagenName);
                System.IO.File.Delete(path);
            }
            deporte.ImagenName = null;
            _deporteService.UpdateDeporte(deporte);
            return CreatedAtAction("GetDeporteById", new { id = deporte.Id }, deporte);
        }

        [NonAction]
        public async Task<string> SaveImagen(IFormFile imageFile)
        {
            var commonpath = Path.Combine(_hostEnvironment.ContentRootPath, "Images");
            var imagePath = Path.Combine(commonpath, imageFile.FileName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageFile.FileName;
        }



        // POST: api/deportes/Image        
        [HttpPatch]
        [Route("api/deportes/{id}/image")]
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

