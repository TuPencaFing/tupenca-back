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
using tupenca_back.DataAccess.Repository.IRepository;
using MercadoPago.Resource.User;

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
        public readonly IUsuarioPencaRepository _usuariopenca;
        public readonly EmpresaService _empresaService;
        public readonly PlanService _planService;
        public readonly UsuarioService _usuarioService;

        public FuncionarioController(ILogger<FuncionarioController> logger, FuncionarioService funcionarioService, IConfiguration configuration, IMapper mapper, IEmailSender emailSender, 
                IUsuarioPencaRepository usuariopenca, EmpresaService empresaService, PlanService planService, UsuarioService usuarioService)
        {
            _logger = logger;
            _funcionarioService = funcionarioService;
            _configuration = configuration;
            _emailSender = emailSender;
            _mapper = mapper;
            _usuariopenca = usuariopenca;
            _empresaService = empresaService;
            _planService = planService;
            _usuarioService = usuarioService;
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
            var str = String.Join(",", passwordHash);
            var str2 = String.Join(",", passwordSalt);

            var user = new Funcionario { UserName = request.Username, Email = request.Email, HashedPassword = passwordHash, PasswordSalt = passwordSalt, EmpresaId = request.EmpresaId };
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

        [HttpPost("invitar")]
        public  IActionResult Invite(InviteUserDto request)
        {
            var funcionarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var funcionario = _funcionarioService.find(Convert.ToInt32(funcionarioId));
            var cantusuarios = _usuariopenca.GetCantUsuariosPenca(request.PencaId);
            var empresa = _empresaService.getEmpresaById(funcionario.EmpresaId);
            var plan = _planService.FindPlanById(empresa.PlanId);

            if (cantusuarios < plan.CantUser)
            {
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //var penca =_funcionarioService.findPenca(int.Parse(id),request.PencaId);
                string token = _funcionarioService.createInviteToken(int.Parse(id), request.PencaId);
                var message = new Message(new string[] { request.Email }, "Invitacion a unirse a la penca", "");
                var templateMessage = $"                    <h2>Hola,</h2>\r\n                    <p>Se te a invitado a unirte a la penca de {empresa.Razonsocial}</p>\r\n                                        <p>Para acceder a la penca, haz click en el boton debajo</p>\r\n                    <a href=\"https://tupenca-user-front.azurewebsites.net/invitacion/{token}\">\r\n                        <button>Ir a penca</button>\r\n                    </a>";

                _emailSender.SendEmailWithTemplate(message, templateMessage);
                return Ok();
            }
            else return BadRequest("Has llegado a la cantidad maxima de usuarios");


        }

        [HttpPost("enviarMensajes"), AllowAnonymous]
        public IActionResult enviarMensajes(UserMessageDto userIds)
        {
            var userEmails = new List<string>();

            foreach (var id in userIds.UserIds)
            {
                userEmails.Add(_usuarioService.find(id).Email);
            }
                //var penca =_funcionarioService.findPenca(int.Parse(id),request.PencaId);
                //string token = _funcionarioService.createInviteToken(int.Parse(id), request.PencaId);
                var message = new Message(userEmails.ToArray(), userIds.topic, "");
                _emailSender.SendEmailWithTemplate(message, userIds.message);
                return Ok();


        }

        //GET: api/user
        [HttpGet("empresa/{id}"), AllowAnonymous]
        public ActionResult<IEnumerable<Funcionario>> GetFuncionariosByEmpresa(int id)
        {
            return Ok(_funcionarioService.getFuncionariosByEmpresa(id));
        }


    }


}
