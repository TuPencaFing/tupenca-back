using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Controllers.Dto;
using System.Security.Claims;
using System.Net;
using tupenca_back.Services.Exceptions;
using tupenca_back.DataAccess.Repository.IRepository;
using tupenca_back.DataAccess.Repository;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize(Roles = "Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioService _userService;
        private readonly PencaService _pencaService;

        public UsuarioController(ILogger<UsuarioController> logger, UsuarioService userService, PencaService pencaService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
            _pencaService = pencaService;
        }

        //GET: api/user
        [HttpGet, AllowAnonymous]
        public ActionResult<UsuarioCountDto> GetUsuarios()
        {
            UsuarioCountDto usuarios = new UsuarioCountDto();
            usuarios.Usuarios = _userService.get();
            usuarios.CantUsuarios = _userService.GetCantUsuarios();
            return Ok(usuarios);
        }
        //GET: api/user/1
        [HttpGet("{id}"), AllowAnonymous]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            Usuario user = _userService.find(id);

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
        [HttpDelete("{id}"), AllowAnonymous]
        public IActionResult DeleteUsuario(int id)
        {

            var user = _userService.find(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.delete(user);

            return NoContent();
        }

        [HttpPost("register"), AllowAnonymous]
        public IActionResult Register(RegisterDTO request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            if(_userService.findByEmail(request.Email) != null)
            {
                return BadRequest("Email already exists.");
            }
            if (_userService.findByUserName(request.Username) != null)
            {
                return BadRequest("Username already exists.");
            }
            _userService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new Usuario {UserName = request.Username, Email = request.Email ,HashedPassword = passwordHash, PasswordSalt = passwordSalt};
            _userService.add(user);
            return Ok(new { message = "User added" });
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(LoginDto request)
        {
            var user = _userService.findByEmail(request.Email);
            if (user?.Email != request.Email)
            {
                return BadRequest("User not found.");
            }

            if (user?.HashedPassword == null)
            {
                return BadRequest("User already logged in with google");
            }

            if (!_userService.VerifyPasswordHash(request.Password, user.HashedPassword, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = _userService.CreateToken(user, "Usuario");
            UserDto userDto = new UserDto();
            userDto.token = token;
            return Ok(userDto);
        }

        // GoogleAuthenticate
        [AllowAnonymous]
        [HttpPost("googleLogin")]
        public async Task<ActionResult<string>> GoogleAuthenticate(string access_token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));

            string token = _userService.CreateToken(await _userService.AuthenticateGoogleUserAsync(access_token, _configuration["Google:Id"]), "Usuario");
            UserDto userDto = new UserDto();
            userDto.token = token;
            return Ok(userDto);
        }

        [HttpPost("aceptarInvitacion")]
        public IActionResult AceptarInvitacion(string access_token)
        {
            try
            {
                int id = _userService.getPencaIdFromToken(access_token);
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                _pencaService.AddUsuarioToPencaEmpresa(userId, id);
                _userService.RemoveUserToken(access_token);
                return Ok();
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("rechazarInvitacion")]
        public IActionResult RechazarInvitacion(string access_token)
        {
            try
            {
                int id = _userService.getPencaIdFromToken(access_token);
                _userService.RemoveUserToken(access_token);
                return Ok();
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }


}
