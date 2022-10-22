using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Controllers.Dto;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioService _userService;
        public UsuarioController(ILogger<UsuarioController> logger, UsuarioService userService, IConfiguration configuration)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
        }

        //GET: api/user
        [HttpGet, AllowAnonymous]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            return Ok(_userService.getUsuarios());
        }
        //GET: api/user/1
        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            Usuario user = _userService.findUsuario(id);

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
        public IActionResult DeleteUsuario(int id)
        {

            var user = _userService.findUsuario(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.deleteUsuario(user);

            return NoContent();
        }

        [HttpPost("register"), AllowAnonymous]
        public IActionResult Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            if(_userService.findUsuarioByEmail(request.Email) != null)
            {
                return BadRequest("Email already exists.");
            }
            if (_userService.findUsuarioByUserName(request.Username) != null)
            {
                return BadRequest("Username already exists.");
            }
            _userService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new Usuario {UserName = request.Username, Email = request.Email ,HashedPassword = passwordHash, PasswordSalt = passwordSalt};
            _userService.addUsuario(user);
            return Ok(new { message = "User added" });
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(LoginRequest request)
        {
            var user = _userService.findUsuarioByEmail(request.Email);
            if (user?.Email != request.Email)
            {
                return BadRequest("User not found.");
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
        public async Task<ActionResult<string>> GoogleAuthenticate( string access_token)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));
            return Ok(_userService.CreateToken(await _userService.AuthenticateGoogleUserAsync(access_token, _configuration["Google:Id"]), "Usuario"));
        }
    }


}
