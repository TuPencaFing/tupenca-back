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
    public class FuncionarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FuncionarioController> _logger;
        private readonly FuncionarioService _funcionarioService;
        public FuncionarioController(ILogger<FuncionarioController> logger, FuncionarioService funcionarioService, IConfiguration configuration)
        {
            _logger = logger;
            _funcionarioService = funcionarioService;
            _configuration = configuration;
        }

        //GET: api/user
        [HttpGet, AllowAnonymous]
        public ActionResult<IEnumerable<Funcionario>> GetFuncionarios()
        {
            return Ok(_funcionarioService.get());
        }
        //GET: api/user/1
        [HttpGet("{id}")]
        public ActionResult<Funcionario> GetFuncionario(int id)
        {
            Funcionario user = _funcionarioService.find(id);

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
        public IActionResult DeleteFuncionario(int id)
        {

            var user = _funcionarioService.find(id);

            if (user == null)
            {
                return NotFound();
            }

            _funcionarioService.delete(user);

            return NoContent();
        }

        [HttpPost("register"), AllowAnonymous]
        public IActionResult Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            if (_funcionarioService.findByEmail(request.Email) != null)
            {
                return BadRequest("Email already exists.");
            }
            if (_funcionarioService.findByUserName(request.Username) != null)
            {
                return BadRequest("Username already exists.");
            }
            _funcionarioService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new Funcionario { UserName = request.Username, Email = request.Email, HashedPassword = passwordHash, PasswordSalt = passwordSalt };
            _funcionarioService.add(user);
            return Ok(new { message = "User added" });
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(LoginRequest request)
        {
            var user = _funcionarioService.findByEmail(request.Email);
            if (user?.Email != request.Email)
            {
                return BadRequest("User not found.");
            }

            if (!_funcionarioService.VerifyPasswordHash(request.Password, user.HashedPassword, user.PasswordSalt))
            {
                return BadRequest("Wrong password.");
            }

            string token = _funcionarioService.CreateToken(user, "Funcionario");
            UserDto userDto = new UserDto();
            userDto.token = token;
            return Ok(userDto);
        }
    }


}
