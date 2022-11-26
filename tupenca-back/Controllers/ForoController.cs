using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Security.Claims;

namespace tupenca_back.Controllers
{
 
    [Authorize]
    [ApiController]
    [Route("api/foros")]
    public class ForoController : ControllerBase
    {
        private readonly ILogger<ForoController> _logger;
        private readonly ForoService _foroService;

        public ForoController(ILogger<ForoController> logger,
                                 ForoService foroService)
        {
            _logger = logger;
            _foroService = foroService;
        }

        //GET: api/foros/1        
        [HttpGet]
        public ActionResult<IEnumerable<Foro>> GetMessages([FromQuery] int pencaId)
        {
            var messages = _foroService.getMessagesByPenca(pencaId);
            return Ok(messages);
        }

        // POST: api/foros        
        [HttpPost]
        public ActionResult CreateMessage(Foro foro)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (userId != foro.UsuarioId) return BadRequest();
                _foroService.CreateMessage(foro);
                return Ok();
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // DELETE: api/foros/1
        [HttpDelete("{id}")]
        public IActionResult DeleteDeporte(int id)
        {
            var foro = _foroService.getMessageById(id);
            if (foro == null)
            {
                return NotFound();
            }
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));                       
            if (userId != foro.UsuarioId) return BadRequest();

            _foroService.RemoveMessage(foro);

            return NoContent();
        }

    }
}

