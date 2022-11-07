using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using AutoMapper;
using tupenca_back.DataAccess.Migrations;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [ApiController]    
    public class EmpresaController : ControllerBase
    {
        private readonly ILogger<EmpresaController> _logger;
        public readonly IMapper _mapper;
        private readonly EmpresaService _empresaService;
        private readonly PlanService _planService;

        public EmpresaController(ILogger<EmpresaController> logger,
                                 IMapper mapper,
                                 EmpresaService empresaService,
                                 PlanService planService)
        {
            _logger = logger;
            _empresaService = empresaService;
            _planService = planService;
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


            var plan = _planService.FindPlanById(empresaDto.PlanId);
            if (plan == null)
                throw new NotFoundException("El Plan no existe");
            empresa.Plan = plan;            

            var emp = _empresaService.getEmpresaByRUT(empresa.RUT);
            if (emp != null)
            {
                return BadRequest("La empresa ingresada ya existe");
            }

            _empresaService.CreateEmpresa(empresa);

            //return CreatedAtAction("GetEmpresaById", new { id = empresa.Id }, _mapper.Map<EmpresaDto>(empresa));
            return CreatedAtAction("GetEmpresaById", new { id = empresa.Id }, empresa);
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

