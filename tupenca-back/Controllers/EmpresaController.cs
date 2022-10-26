using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using AutoMapper;

namespace tupenca_back.Controllers
{
    [ApiController]    
    public class EmpresaController : ControllerBase
    {
        private readonly ILogger<EmpresaController> _logger;
        public readonly IMapper _mapper;
        private readonly EmpresaService _empresaService;

        public EmpresaController(ILogger<EmpresaController> logger,
                                 IMapper mapper,
                                 EmpresaService empresaService)
        {
            _logger = logger;
            _empresaService = empresaService;
        }

        //GET: api/empresas        
        [HttpGet]
        [Route("api/empresas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Empresa>> GetEmpresas()
        {
            var empresas = _empresaService.getEmpresas();
            return Ok(empresas);
        }

        //GET: api/empresas/nuevas        
        [HttpGet]
        [Route("api/empresas/nuevas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<int> GetEmpresasNuevas()
        {
            int empresas = _empresaService.GetCantEmpresasNuevas();
            return Ok(empresas);
        }

        // GET: api/empresas/1        
        [HttpGet]
        [Route("api/empresas/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Empresa> GetEmpresaById(int id)
        {
            var empresa = _empresaService.getEmpresaById(id);

            if (empresa == null)
            {
                return NotFound();
            } 
            else
            {
                return Ok(empresa);
            }
        }


        // POST: api/empresas       
        [HttpPost]
        [Route("api/empresas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Empresa> CreateEmpresa(EmpresaDto empresaDto)
        {
            Empresa empresa = new Empresa();
            empresa.RUT = empresaDto.RUT;
            empresa.Razonsocial = empresaDto.Razonsocial;
            empresa.FechaCreacion = DateTime.Now;

            var emp = _empresaService.getEmpresaByRUT(empresa.RUT);
            if (emp != null)
            {
                return BadRequest("La empresa ingresada ya existe");
            }

            _empresaService.CreateEmpresa(empresa);

            return CreatedAtAction("GetEmpresaById", new { id = empresa.Id }, _mapper.Map<EmpresaDto>(empresa));
        }


        // DELETE: api/empresas/1
        [HttpDelete]
        [Route("api/empresas/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteEmpresas(int id)
        {
            var empresa = _empresaService.getEmpresaById(id);

            if (empresa == null)
            {
                return NotFound();
            }
            _empresaService.RemoveEmpresa(empresa);
            return NoContent();
        }

    }
}

