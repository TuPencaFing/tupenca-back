using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace tupenca_back.Controllers
{
 
    [Authorize]
    [ApiController]
    [Route("api/lookandfeel")]
    public class LookAndFeelController : ControllerBase
    {
        private readonly ILogger<LookAndFeelController> _logger;
        private readonly LookAndFeelService _lookandfeelService;
        private readonly EmpresaService _empresaService;
        private readonly FuncionarioService _funcionarioService;

        public LookAndFeelController(ILogger<LookAndFeelController> logger,
                                 LookAndFeelService lookandfeelService,
                                 EmpresaService empresaService,
                                 FuncionarioService funcionarioService)
        {
            _logger = logger;
            _lookandfeelService = lookandfeelService;
            _empresaService = empresaService;
            _funcionarioService = funcionarioService;
        }

        //GET: api/lookandfeel/1        
        [HttpGet("{tenantCode}")]
        public ActionResult<LookAndFeel> GetLookAndFeelByEmpresa(string tenantCode)
        {
            var empresa = _empresaService.getEmpresaByTenantCode(tenantCode);
            if (empresa == null) return BadRequest();
            var lookandfeel = _lookandfeelService.getLookAndFeelByEmpresaId(empresa.Id);
            if (lookandfeel == null) return NotFound();
            return Ok(lookandfeel);
        }

        // POST: api/lookandfeel        
        [HttpPost]
        public ActionResult CreateLookAndFeel(LookAndFeelDto lookandfeel)
        {
            try
            {
                var funcionarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var funcionario = _funcionarioService.find(Convert.ToInt32(funcionarioId));
                if (funcionario == null) return BadRequest();
                if (_empresaService.getEmpresaById(funcionario.EmpresaId) == null) return BadRequest();
                LookAndFeel look = new LookAndFeel
                {
                    EmpresaId = funcionario.EmpresaId,
                    Navbar = lookandfeel.Navbar,
                    Generalbackground = lookandfeel.Generalbackground,
                    Generaltext = lookandfeel.Generaltext,
                    Textnavbar = lookandfeel.Textnavbar
                };
                _lookandfeelService.CreateLookAndFeel(look);
                return Ok();
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // PUT: api/lookandfeel        
        [HttpPut]
        public ActionResult UpdateLookAndFeel(LookAndFeelDto lookandfeel)
        {
            try
            {
                var funcionarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var funcionario = _funcionarioService.find(Convert.ToInt32(funcionarioId));
                if (funcionario == null) return BadRequest();
                if (_empresaService.getEmpresaById(funcionario.EmpresaId) == null) return BadRequest();
                var existente = _lookandfeelService.getLookAndFeelByEmpresaId(funcionario.EmpresaId);
                if (existente == null)
                {
                    LookAndFeel look = new LookAndFeel
                    {
                        EmpresaId = funcionario.EmpresaId,
                        Navbar = lookandfeel.Navbar,
                        Generalbackground = lookandfeel.Generalbackground,
                        Generaltext = lookandfeel.Generaltext,
                        Textnavbar = lookandfeel.Textnavbar
                    };

                   _lookandfeelService.CreateLookAndFeel(look);


                }
                else
                {
                    existente.Generaltext = lookandfeel.Generaltext;
                    existente.Generalbackground = lookandfeel.Generalbackground;
                    existente.Textnavbar = lookandfeel.Textnavbar;
                    existente.Navbar = lookandfeel.Navbar;

                    _lookandfeelService.UpdateLookAndFeel(existente);
                }               

                return Ok();
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // DELETE: api/lookandfeel/1
        [HttpDelete("{EmpresaId}")]
        public IActionResult DeleteLookAndFeel(int EmpresaId)
        {
            if (_empresaService.getEmpresaById(EmpresaId) == null) return BadRequest();
            var lookandfeel = _lookandfeelService.getLookAndFeelByEmpresaId(EmpresaId);
            if (lookandfeel == null) return NotFound();

            _lookandfeelService.RemoveLookAndFeel(lookandfeel);

            return NoContent();
        }

    }
}

