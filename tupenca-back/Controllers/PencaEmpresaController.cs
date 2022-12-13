using System;
using System.Net;
using System.Numerics;
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
        private readonly PlanService _planService;
        private readonly CampeonatoService _campeonatoService;
        private readonly PuntajeService _puntajeService;
        private readonly PrediccionService _prediccionService;
        private readonly ResultadoService _resultadoService;
        private readonly EquipoService _equipoService;
        private readonly PuntajeUsuarioPencaService _puntajeUsuarioPencaService;
        private readonly EventoService _eventoService;
        private readonly UsuarioService _usuarioService;
        private readonly EmpresaService _empresaService;

        public PencaEmpresaController(ILogger<PencaEmpresaController> logger,
                               IMapper mapper,
                               PencaService pencaService,
                               FuncionarioService funcionarioService,
                               PlanService planService,
                               CampeonatoService campeonatoService,
                               PuntajeService puntajeService,
                               PrediccionService prediccionService,
                               ResultadoService resultadoService,
                               EquipoService equipoService,
                               PuntajeUsuarioPencaService puntajeUsuarioPencaService,
                               EventoService eventoService,
                               UsuarioService usuarioService,
                               EmpresaService empresaService)
        {
            _logger = logger;
            _mapper = mapper;
            _pencaService = pencaService;
            _funcionarioService = funcionarioService;
            _planService = planService;
            _campeonatoService = campeonatoService;
            _puntajeService = puntajeService;
            _prediccionService = prediccionService;
            _resultadoService = resultadoService;
            _equipoService = equipoService;
            _puntajeUsuarioPencaService = puntajeUsuarioPencaService;
            _eventoService = eventoService;
            _usuarioService = usuarioService;
            _empresaService = empresaService;
        }

        //GET: api/pencas-empresas
        [HttpGet]
        public ActionResult<IEnumerable<PencaEmpresaDto>> GetPencasEmpresa([FromQuery] string? TenantCode = null)
        {
            try
            {
                if (TenantCode != null)
                {
                    var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                    var user = _usuarioService.find(userId);
                    if (user == null)
                    {
                        return BadRequest();
                    }
                    var empresa = _empresaService.getEmpresaByTenantCode(TenantCode);
                    if (empresa == null)
                    {
                        return BadRequest();
                    }
                    
                    if (_pencaService.chekAuthUserEmpresa(TenantCode, userId))
                    {
                        var pencasEmpresa = _pencaService.GetPencasFromEmpresaByUsuario(TenantCode, userId);
                        Console.WriteLine("ODIO TSI");
                        var pencasEmpresaDto = _mapper.Map<List<PencaEmpresaDto>>(pencasEmpresa);
                        return Ok(pencasEmpresaDto);
                    }
                    else
                    {
                        return Unauthorized();
                    }

                }

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
        [HttpGet("miempresa")]
        public ActionResult<IEnumerable<PencaEmpresaDto>> GetPencasEmpresabyEmpresa()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);            
            var funcionario = _funcionarioService.find(Convert.ToInt32(userId));
            if (funcionario == null)
                return NotFound();

            try
            {
                var pencas = _pencaService.GetPencaEmpresasByEmpresa(funcionario.EmpresaId);

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
                var penca = _pencaService.findPencaEmpresaById(id);

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
                var plan = _planService.FindPlanById(pencaEmpresaDto.Empresa.PlanId);
                if (plan == null)
                    throw new NotFoundException("El Plan no existe");
                
                var cantpencas = _pencaService.GetCantPencaEmpresas(pencaEmpresaDto.Empresa.Id);
                if (cantpencas < plan.CantPencas)
                {
                    var pencaEmpresa = _mapper.Map<PencaEmpresa>(pencaEmpresaDto);

                    _pencaService.AddPencaEmpresa(pencaEmpresa);

                    return CreatedAtAction("GetPencaEmpresa", new { id = pencaEmpresa.Id }, _mapper.Map<PencaEmpresaDto>(pencaEmpresa));
                }
                else
                {
                    throw new NotFoundException("Has llegado al limite de Pencas");
                }           

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

        // PATCH: api/pencas-empresas/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _pencaService.SaveImagen(id, imagenDto.file, false);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet("{id}/info")]
        public ActionResult<PencaInfoDto> GetInfoPenca(int id)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var penca = _pencaService.findPencaEmpresaById(id);
                if (penca == null)
                {
                    return NotFound();
                }
                
                
                if (_pencaService.chekAuthUserEmpresa(penca.Empresa.TenantCode, userId))
                {
                    PencaInfoDto pencaInfo = new PencaInfoDto();
                    pencaInfo.Id = penca.Id;
                    pencaInfo.PencaTitle = penca.Title;
                    pencaInfo.PencaDescription = penca.Description;
                    pencaInfo.Image = penca.Image;
                    pencaInfo.CampeonatoName = penca.Campeonato.Name;
                    pencaInfo.DeporteName = _campeonatoService.findCampeonatoById(penca.Campeonato.Id).Deporte.Nombre;
                    var eventos = _pencaService.GetInfoEventosByPencaUsuario(id, userId);
                    pencaInfo.Eventos = eventos;
                    var score = _puntajeUsuarioPencaService.GetTotalByPencaAndUsuario(id, userId);
                    if (score == null)
                    {
                        pencaInfo.PuntajeTotal = 0;
                    }
                    else
                    {
                        pencaInfo.PuntajeTotal = score;

                    }
                    return Ok(pencaInfo);
                }
                else
                {
                    return Unauthorized();
                }
                

            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

  
        [HttpGet("{id}/eventos/{idEvento}/estadisticas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EstadisticaEventoDto> GetEstadisticas(int id, int idEvento)
        {
            var penca = _pencaService.findPencaEmpresaById(id);
            if (penca == null) return NotFound();
            EstadisticaEventoDto eventodto = new EstadisticaEventoDto();
            var porcentajeLocal = _prediccionService.GetPorcentajeLocal(id, idEvento);
            eventodto.PorcentajeLocal = porcentajeLocal;
            var evento = _eventoService.getEventoById(idEvento);
            if (evento.IsEmpateValid)
            {
                if (porcentajeLocal == null)
                {
                    eventodto.PorcentajeVisitante = null;
                    eventodto.PorcentajeEmpate = null;
                }
                else
                {
                    var porcentajeEmpate = _prediccionService.GetPorcentajeEmpate(id, idEvento);
                    eventodto.PorcentajeEmpate = porcentajeEmpate;
                    eventodto.PorcentajeVisitante = (100 - (porcentajeLocal + porcentajeEmpate));
                }
            }
            else
            {
                eventodto.PorcentajeEmpate = null;
                if (porcentajeLocal == null)
                {
                    eventodto.PorcentajeVisitante = null;
                }
                else
                {
                    eventodto.PorcentajeVisitante = (100 - porcentajeLocal);
                }
            }
            return Ok(eventodto);
        }

   
        [HttpGet("{id}/usuarios")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]        
        public ActionResult<UsuariosPencaEmpresaDto> GetUsuariosPencaEmpresa(int id)
        {

        }

    }
}

