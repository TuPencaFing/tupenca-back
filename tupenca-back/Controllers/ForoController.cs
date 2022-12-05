using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Security.Claims;
using MercadoPago.Resource.User;
using System.Runtime.CompilerServices;

namespace tupenca_back.Controllers
{
 
    [Authorize]
    [ApiController]
    [Route("api/foros")]
    public class ForoController : ControllerBase
    {
        private readonly ILogger<ForoController> _logger;
        private readonly ForoService _foroService;
        private readonly UsuarioService _usuarioService;
        public readonly IMapper _mapper;

        public ForoController(ILogger<ForoController> logger,
                                 ForoService foroService,
                                 UsuarioService usuarioService,
                                 IMapper mapper)
        {
            _logger = logger;
            _foroService = foroService;
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        //GET: api/foros/1        
        [HttpGet]
        public ActionResult<IEnumerable<ForoUsers>> GetMessages([FromQuery] int pencaId)
        {
            var messages = _foroService.getMessagesByPenca(pencaId);
            return Ok(messages);
        }

        // POST: api/foros        
        [HttpPost]
        public ActionResult CreateMessage(ForoDto forodto)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = _usuarioService.find(userId);
                if (user == null) return BadRequest();
                Foro foro = new Foro();
                foro.PencaId = forodto.PencaId;
                foro.UsuarioId = userId;
                foro.Message = forodto.Message;
                foro.Creacion = DateTime.UtcNow;
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

