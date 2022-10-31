using System;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/pencas-empresas")]
    public class PencaEmpresaController : ControllerBase
    {
        private readonly ILogger<PencaEmpresaController> _logger;
        public readonly IMapper _mapper;
        private readonly PencaService _pencaService;
        private readonly FuncionarioService _funcionarioService;

        public PencaEmpresaController(ILogger<PencaEmpresaController> logger,
                               IMapper mapper,
                               PencaService pencaService,
                               FuncionarioService funcionarioService)
        {
            _logger = logger;
            _mapper = mapper;
            _pencaService = pencaService;
            _funcionarioService = funcionarioService;
        }

        //GET: api/pencas-empresas
        [HttpGet]
        public ActionResult<IEnumerable<PencaEmpresaDto>> GetPencasEmpresa()
        {
            try
            {
                var pencas = _pencaService.GetPencaEmpresas();

                var pencasDto = _mapper.Map<List<PencaEmpresaDto>>(pencas);

                return Ok(pencasDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
               
        //GET: api/pencas-empresas
        [HttpGet("/miempresa")]
        public ActionResult<IEnumerable<PencaEmpresaDto>> GetPencasEmpresabyEmpresa()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);            
            var funcionario = _funcionarioService.find(Convert.ToInt32(userId));
            if (funcionario == null)
                return NotFound();

            try
            {
                var pencas = _pencaService.GetPencaCompartidasByEmpresa(funcionario.EmpresaId);

                var pencasDto = _mapper.Map<List<PencaEmpresaDto>>(pencas);

                return Ok(pencasDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //GET: api/pencas-empresas/1
        [HttpGet("{id}")]
        public ActionResult<PencaEmpresaDto> GetPencaEmpresa(int id)
        {
            try
            {
                var penca = _pencaService.findPencaCompartidaById(id);

                if (penca == null)
                {
                    return NotFound();
                }
                else
                {
                    var pencaDto = _mapper.Map<PencaEmpresaDto>(penca);

                    return Ok(pencaDto);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //POST: api/pencas-empresas
        [HttpPost]
        public IActionResult PostPencaEmpresa(PencaEmpresaDto pencaEmpresaDto)
        {
            if (pencaEmpresaDto == null)
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "La Penca no debe ser nulo");

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var pencaEmpresa = _mapper.Map<PencaEmpresa>(pencaEmpresaDto);

                _pencaService.AddPencaEmpresa(pencaEmpresa);

                return CreatedAtAction("GetPencaEmpresa", new { id = pencaEmpresa.Id }, _mapper.Map<PencaEmpresaDto>(pencaEmpresa));
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


        // PUT: api/pencas-empresas/1
        [HttpPut("{id}")]
        public IActionResult PutPencaEmpresa(int id, PencaEmpresaDto pencaEmpresaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_pencaService.PencaEmpresaExists(id))
                return NotFound();

            try
            {
                var pencaEmpresa = _mapper.Map<PencaEmpresa>(pencaEmpresaDto);

                _pencaService.UpdatePencaEmpresa(id, pencaEmpresa);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // DELETE: api/pencas-empresas/1
        [HttpDelete("{id}")]
        public IActionResult DeletePencaEmpresa(int id)
        {
            try
            {
                _pencaService.RemovePencaEmpresa(id);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}

