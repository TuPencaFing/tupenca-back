using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;


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

        public LookAndFeelController(ILogger<LookAndFeelController> logger,
                                 LookAndFeelService lookandfeelService,
                                 EmpresaService empresaService)
        {
            _logger = logger;
            _lookandfeelService = lookandfeelService;
            _empresaService = empresaService;
        }

        //GET: api/lookandfeel/1        
        [HttpGet("{EmpresaId}")]
        public ActionResult<LookAndFeel> GetLookAndFeelByEmpresa(int EmpresaId)
        {
            if (_empresaService.getEmpresaById(EmpresaId) == null) return BadRequest();
            var lookandfeel = _lookandfeelService.getLookAndFeelByEmpresaId(EmpresaId);
            if (lookandfeel == null) return NotFound();
            return Ok(lookandfeel);
        }

        // POST: api/lookandfeel        
        [HttpPost]
        public ActionResult CreateLookAndFeel(LookAndFeel lookandfeel)
        {
            try
            {
                if (_empresaService.getEmpresaById(lookandfeel.EmpresaId) == null) return BadRequest();
                _lookandfeelService.CreateLookAndFeel(lookandfeel);
                return Ok();
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }
        }

        // PUT: api/lookandfeel        
        [HttpPut("{EmpresaId}")]
        public ActionResult UpdateLookAndFeel(int EmpresaId, LookAndFeelDto lookandfeel)
        {
            try
            {
                if (_empresaService.getEmpresaById(EmpresaId) == null) return BadRequest();
                var existente = _lookandfeelService.getLookAndFeelByEmpresaId(EmpresaId);
                if (existente == null)
                {
                    LookAndFeel look = new LookAndFeel
                    {
                        EmpresaId = EmpresaId,
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

