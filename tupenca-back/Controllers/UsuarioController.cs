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
using tupenca_back.Utilities.EmailService;
using System.Text;
using System;
using AutoMapper;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsuarioController> _logger;
        private readonly UsuarioService _userService;
        private readonly PencaService _pencaService;
        private readonly IEmailSender _emailSender;
        public readonly IMapper _mapper;
        public readonly PuntajeUsuarioPencaService _puntajeUsuarioPencaService;
        public readonly ImagesService _imagesService;

        public UsuarioController(ILogger<UsuarioController> logger, UsuarioService userService, PencaService pencaService, IConfiguration configuration, IEmailSender emailSender, IMapper imapper, PuntajeUsuarioPencaService puntajeUsuarioPencaService, ImagesService imagesService)
        {
            _logger = logger;
            _userService = userService;
            _configuration = configuration;
            _pencaService = pencaService;
            _emailSender = emailSender;
            _mapper = imapper;
            _puntajeUsuarioPencaService = puntajeUsuarioPencaService;
            _imagesService = imagesService;
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
        public ActionResult<UsuarioDto> GetUsuario(int id)
        {
            Usuario user = _userService.getUsuario(id);
            //EmpresaDto empresasDto = new EmpresaDto() { Id = user.Empresas};
            var userDto = _mapper.Map<UsuarioDto>(user);

            //UsuarioDto userDto = new UsuarioDto() { Email = user.Email, UserName = user.UserName, Image = user.Image, Empresas = null};

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(userDto);
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
        public IActionResult Register([FromForm] RegisterDTO request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            if(_userService.findByEmail(request.email) != null)
            {
                return BadRequest("Email already exists.");
            }
            if (_userService.findByUserName(request.username) != null)
            {
                return BadRequest("Username already exists.");
            }
            string? imagen = null;
            if (request.image != null)
            {
               imagen = _imagesService.uploadImage("user_" + request.username + ".png", request.image.file.OpenReadStream());
            }           
            _userService.CreatePasswordHash(request.password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new Usuario {UserName = request.username, Email = request.email ,HashedPassword = passwordHash, PasswordSalt = passwordSalt, Image = imagen};
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
        public async Task<ActionResult<string>> GoogleAuthenticate(TokenDto tokenDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));

            string token = _userService.CreateToken(await _userService.AuthenticateGoogleUserAsync(tokenDto.accessToken, _configuration["Google:Id"]), "Usuario");
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

        [HttpPost("forgotPassword"), AllowAnonymous]
        public ActionResult ForgotPassword(EmailDto Email)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.findByEmail(Email.Email);
                if (user == null)
                {
                    return BadRequest("User doesnt exist.");
                }else 
                {
                    string token = _userService.createResetToken(user.Id);
                    string To = Email.Email, UserID, Password, SMTPPort, Host;

                        //Create URL with above token
                        var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = Email.Email }, "http") + "'>Reset Password</a>";
                        //HTML Template for Send email
                        string subject = "Your changed password";
                    //string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                    //string htmlSend = $"<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional //EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html\r\n  xmlns=\"http://www.w3.org/1999/xhtml\"\r\n  xmlns:o=\"urn:schemas-microsoft-com:office:office\"\r\n  xmlns:v=\"urn:schemas-microsoft-com:vml\"\r\n  lang=\"en\"\r\n>\r\n  <head>\r\n    <link\r\n      rel=\"stylesheet\"\r\n      type=\"text/css\"\r\n      hs-webfonts=\"true\"\r\n      href=\"https://fonts.googleapis.com/css?family=Lato|Lato:i,b,bi\"\r\n    />\r\n    <title>Email template</title>\r\n    <meta property=\"og:title\" content=\"Email template\" />\r\n\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />\r\n\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\" />\r\n\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n\r\n    <style type=\"text/css\">\r\n      a {{\r\n        text-decoration: underline;\r\n        color: inherit;\r\n        font-weight: bold;\r\n        color: #253342;\r\n      }}\r\n\r\n      h1 {{\r\n        font-size: 56px;\r\n      }}\r\n\r\n      h2 {{\r\n        font-size: 28px;\r\n        font-weight: 900;\r\n      }}\r\n\r\n      p {{\r\n        font-weight: 100;\r\n      }}\r\n\r\n      td {{\r\n        vertical-align: top;\r\n      }}\r\n\r\n      #email {{\r\n        margin: auto;\r\n        width: 600px;\r\n        background-color: white;\r\n      }}\r\n\r\n      button {{\r\n        font: inherit;\r\n        background-color: #ff7a59;\r\n        border: none;\r\n        padding: 10px;\r\n        text-transform: uppercase;\r\n        letter-spacing: 2px;\r\n        font-weight: 900;\r\n        color: white;\r\n        border-radius: 5px;\r\n        box-shadow: 3px 3px #d94c53;\r\n      }}\r\n\r\n      .subtle-link {{\r\n        font-size: 9px;\r\n        text-transform: uppercase;\r\n        letter-spacing: 1px;\r\n        color: #cbd6e2;\r\n      }}\r\n    </style>\r\n  </head>\r\n\r\n  <body\r\n    bgcolor=\"#F5F8FA\"\r\n    style=\"\r\n      width: 100%;\r\n      margin: auto 0;\r\n      padding: 0;\r\n      font-family: Lato, sans-serif;\r\n      font-size: 18px;\r\n      color: #33475b;\r\n      word-break: break-word;\r\n    \"\r\n  >\r\n    <! View in Browser Link -->\r\n\r\n    <div id=\"email\">\r\n      <table align=\"right\" role=\"presentation\">\r\n        <tr>\r\n          <td>\r\n            <a class=\"subtle-link\" href=\"#\">View in Browser</a>\r\n          </td>\r\n        </tr>\r\n\r\n        <tr></tr>\r\n      </table>\r\n\r\n      <! Banner -->\r\n      <table role=\"presentation\" width=\"100%\">\r\n        <tr>\r\n          <td bgcolor=\"#00A4BD\" align=\"center\" style=\"color: white\">\r\n            <img\r\n              alt=\"Flower\"\r\n              src=\"https://tupenca.blob.core.windows.net/images/logo1.png\"\r\n              width=\"200px\"\r\n              align=\"middle\"\r\n            />\r\n\r\n            <!-- <h1>Tu Penca</h1> -->\r\n          </td>\r\n        </tr>\r\n      </table>\r\n\r\n      <! First Row -->\r\n\r\n      <table\r\n        role=\"presentation\"\r\n        border=\"0\"\r\n        cellpadding=\"0\"\r\n        cellspacing=\"10px\"\r\n        style=\"padding: 30px 30px 30px 60px\"\r\n      >\r\n        <tr>\r\n          <td>\r\n            <h2>Hi {user.UserName},</h2>\r\n            <p>Forgot your password?</p>\r\n            <p>We recived a request to reset the password for your account</p>\r\n            <p>To reset your password, click on the button below</p>\r\n            <a href=\"https://localhost:3000/resetPassword?requestToken={token}\">\r\n              <button>Reset Password</button>\r\n            </a>\r\n          </td>\r\n        </tr>\r\n      </table>\r\n\r\n      <! Second Row with Two Columns-->\r\n\r\n      <!-- <table\r\n        role=\"presentation\"\r\n        border=\"0\"\r\n        cellpadding=\"0\"\r\n        cellspacing=\"10px\"\r\n        width=\"100%\"\r\n        style=\"padding: 30px 30px 30px 60px\"\r\n      >\r\n        <tr>\r\n          <td>\r\n            <img\r\n              alt=\"Blog\"\r\n              src=\"https://www.hubspot.com/hubfs/assets/hubspot.com/style-guide/brand-guidelines/guidelines_sample-illustration-3.svg\"\r\n              width=\"200px\"\r\n              align=\"middle\"\r\n            />\r\n\r\n            <h2>Vivamus ac elit eget</h2>\r\n            <p>\r\n              Vivamus ac elit eget dolor placerat tristique et vulputate nibh.\r\n              Sed in elementum nisl, quis mollis enim. Etiam gravida dui vel est\r\n              euismod, at aliquam ipsum euismod.\r\n            </p>\r\n          </td>\r\n\r\n          <td>\r\n            <img\r\n              alt=\"Shopping\"\r\n              src=\"https://tupenca.blob.core.windows.net/images/logo1.png\"\r\n              width=\"200px\"\r\n              align=\"middle\"\r\n            />\r\n            <h2>Suspendisse tincidunt iaculis</h2>\r\n            <p>\r\n              Suspendisse tincidunt iaculis fringilla. Orci varius natoque\r\n              penatibus et magnis dis parturient montes, nascetur ridiculus mus.\r\n              Cras laoreet elit purus, quis pulvinar ipsum pulvinar et.\r\n            </p>\r\n          </td>\r\n        </tr>\r\n\r\n        <tr>\r\n          <td><button>Button 2</button></td>\r\n          <td><button>Button 3</button></td>\r\n        </tr>\r\n      </table> -->\r\n\r\n      <! Banner Row -->\r\n      <!-- <table\r\n        role=\"presentation\"\r\n        bgcolor=\"#EAF0F6\"\r\n        width=\"100%\"\r\n        style=\"margin-top: 50px\"\r\n      >\r\n        <tr>\r\n          <td align=\"center\" style=\"padding: 30px 30px\">\r\n            <h2>Nullam porta arcu</h2>\r\n            <p>\r\n              Nam vel lobortis lorem. Nunc facilisis mauris at elit pulvinar,\r\n              malesuada condimentum erat vestibulum. Pellentesque eros tellus,\r\n              finibus eget erat at, tempus rutrum justo.\r\n            </p>\r\n            <a href=\"#\"> Ask us a question</a>\r\n          </td>\r\n        </tr>\r\n      </table> -->\r\n\r\n      <! Unsubscribe Footer -->\r\n\r\n      <!-- <table role=\"presentation\" bgcolor=\"#F5F8FA\" width=\"100%\">\r\n        <tr>\r\n          <td align=\"left\" style=\"padding: 30px 30px\">\r\n            <p style=\"color: #99acc2\">Made with &hearts; at HubSpot HQ</p>\r\n            <a class=\"subtle-link\" href=\"#\"> Unsubscribe </a>\r\n          </td>\r\n        </tr>\r\n      </table> -->\r\n    </div>\r\n  </body>\r\n</html>\r\n";  
                    //Get and set the AppSettings using configuration manager.
                    Message message = new Message(new string[] { Email.Email }, subject, "");
                    var templateMessage = $"                    <h2>Hola {user.UserName},</h2>\r\n                    <p>Olvidaste tu contraseña?</p>\r\n                    <p>Recibimos una solicitud para restablecer la contraseña de tu cuenta</p>\r\n                    <p>Para restablecer la contraseña, haz click en el boton debajo</p>\r\n                    <a href=\"https://tupenca-user-front.azurewebsites.net/restablecer-password?token={token}\">\r\n                        <button>Reiniciar Contraseña</button>\r\n                    </a>";

                    _emailSender.SendEmailWithTemplate(message, templateMessage);
                    //Call send email methods.
                    //EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);

                }
            }
            return Ok();
        }
        [HttpPost("resetPassword"), AllowAnonymous]
        public IActionResult ResetPassword(ResetPasswordDto resetDto)
        {
            try
            {
                int id = _userService.getPersonaIdFromToken(resetDto.token);
                var user = _userService.getUsuario(id);
                _userService.CreatePasswordHash(resetDto.password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordSalt = passwordSalt;
                user.HashedPassword = passwordHash;
                _userService.UpdateUsuario(user);
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

        [HttpPost("habilitarUsuario")]
        public IActionResult HabilitarUsuarioPencaEmpresa(int pencaId, int userId)
        {
            try
            {
                //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var penca = _pencaService.findPencaEmpresaById(pencaId);
                if(penca == null)
                {
                    throw new HttpResponseException((int)HttpStatusCode.NotFound, "Penca Empresa not found");
                }
                else
                {
                    _pencaService.HabilitarUsuario(pencaId, userId);
                    PuntajeUsuarioPenca puntajeUsuarioPenca = new PuntajeUsuarioPenca { PencaId = pencaId, UsuarioId = userId };
                    _puntajeUsuarioPencaService.Create(puntajeUsuarioPenca);
                    return Ok();
                }


            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost("rechazarUsuario")]
        public IActionResult RechazarUsuarioPencaEmpresa(int pencaId, int userId)
        {
            try
            {
                //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var penca = _pencaService.findPencaEmpresaById(pencaId);
                if (penca == null)
                {
                    throw new HttpResponseException((int)HttpStatusCode.NotFound, "Penca Empresa not found");
                }
                else
                {
                    _pencaService.RechazarUsuario(pencaId, userId);
                    return Ok();
                }


            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }


}
