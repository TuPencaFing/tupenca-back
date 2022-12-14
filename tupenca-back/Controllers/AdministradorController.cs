using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Controllers.Dto;
using tupenca_back.Utilities.EmailService;
using tupenca_back.Services.Exceptions;
using System.Net;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/administradores")]
    [Authorize(Roles = "Administrador")]
    public class AdministradorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AdministradorController> _logger;
        private readonly AdministradorService _administradorService;
        public AdministradorController(ILogger<AdministradorController> logger, AdministradorService administradorService, IConfiguration configuration)
        {
            _logger = logger;
            _administradorService = administradorService;
            _configuration = configuration;
        }

        //GET: api/user
        [HttpGet, AllowAnonymous]
        public ActionResult<IEnumerable<Administrador>> GetAdministradors()
        {
            return Ok(_administradorService.get());
        }
        //GET: api/user/1
        [HttpGet("{id}")]
        public ActionResult<Administrador> GetAdministrador(int id)
        {
            Administrador user = _administradorService.find(id);

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        // DELETE: api/campeonatos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteAdministrador(int id)
        {

            var user = _administradorService.find(id);

            if (user == null)
            {
                return NotFound();
            }

            _administradorService.delete(user);

            return NoContent();
        }

        [HttpPost("register"), AllowAnonymous]
        public IActionResult Register(RegisterDTO request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            if (_administradorService.findByEmail(request.email) != null)
            {
                return BadRequest("Email already exists.");
            }
            if (_administradorService.findByUserName(request.username) != null)
            {
                return BadRequest("Username already exists.");
            }
            _administradorService.CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new Administrador { UserName = request.username, Email = request.email, HashedPassword = passwordHash, PasswordSalt = passwordSalt };
            _administradorService.add(user);
            return Ok(new { message = "User added" });
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(LoginDto request)
        {
            var user = _administradorService.findByEmail(request.Email);
            if (user?.Email != request.Email)
            {
                return BadRequest("User not found.");
            }

            if (!_administradorService.VerifyPasswordHash(request.Password, user.HashedPassword, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = _administradorService.CreateToken(user, "Administrador");
            UserDto userDto = new UserDto();
            userDto.token = token;
            return Ok(userDto);
        }


        //GET: api/administrador
        [HttpGet("metricas")]
        [AllowAnonymous]
        public ActionResult<Metrica> GetMetrica()
        {
            try
            {
                var metrica = _administradorService.GetMetrica();

                return Ok(metrica);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }


}
