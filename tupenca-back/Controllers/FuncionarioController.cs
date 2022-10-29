using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Controllers.Dto;
using tupenca_back.Utilities.EmailService;
using System.Security.Claims;
using AutoMapper;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    [Authorize(Roles ="Funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FuncionarioController> _logger;
        private readonly FuncionarioService _funcionarioService;
        private readonly IEmailSender _emailSender;
        public readonly IMapper _mapper;

        public FuncionarioController(ILogger<FuncionarioController> logger, FuncionarioService funcionarioService, IConfiguration configuration, IMapper mapper, IEmailSender emailSender)
        {
            _logger = logger;
            _funcionarioService = funcionarioService;
            _configuration = configuration;
            _emailSender = emailSender;
            _mapper = mapper;
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
        public IActionResult Register(FuncionarioDTO request)
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
            var Empresa = _mapper.Map<Empresa>(request.Empresa);
            var user = new Funcionario { UserName = request.Username, Email = request.Email, HashedPassword = passwordHash, PasswordSalt = passwordSalt, EmpresaId = Empresa.Id };
            _funcionarioService.add(user);
            return Ok(new { message = "User added" });
        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login(LoginDto request)
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

        [HttpPost("invitar"),AllowAnonymous]
        public  IActionResult Invite(InviteUserDto request)
        {
            //var message = new Message(new string[] { "mati98bor@gmail.com" }, "Test email", "This is the content from our email.");
            //_emailSender.SendEmail(message);
            //var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var penca =_funcionarioService.findPenca(int.Parse(id),request.PencaId);
            var penca =_funcionarioService.findPenca(14,0);

            return Ok();
        }
    }


}
