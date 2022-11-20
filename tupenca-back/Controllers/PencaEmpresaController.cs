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

        public PencaEmpresaController(ILogger<PencaEmpresaController> logger,
                               IMapper mapper,
                               PencaService pencaService,
                               FuncionarioService funcionarioService,
                               PlanService planService,
                               CampeonatoService campeonatoService,
                               PuntajeService puntajeService,
                               PrediccionService prediccionService,
                               ResultadoService resultadoService,
                               EquipoService equipoService)
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
        }

        //GET: api/pencas-empresas
        [HttpGet]
        public ActionResult<IEnumerable<PencaEmpresaDto>> GetPencasEmpresa([FromQuery] int id)
        {
            try
            {
                if (id != null && id != 0) // id se instancia en 0 si es vacio
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var pencasEmpresa = _pencaService.GetPencasFromEmpresaByUsuario(id, Convert.ToInt32(userId));
                    var pencasEmpresaDto = _mapper.Map<List<PencaEmpresaDto>>(pencasEmpresa);
                    return Ok(pencasEmpresa);

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
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var penca = _pencaService.findPencaEmpresaById(id);
                if (penca == null)
                {
                    return NotFound();
                }
                PencaInfoDto pencainfo = new PencaInfoDto();
                pencainfo.Id = penca.Id;
                pencainfo.PencaTitle = penca.Title;
                pencainfo.PencaDescription = penca.Description;
                pencainfo.Image = penca.Image;               
                var campeonato = _campeonatoService.findCampeonatoById(penca.Campeonato.Id);
                pencainfo.CampeonatoName = campeonato.Name;
                pencainfo.StartDate = campeonato.StartDate;
                pencainfo.FinishDate = campeonato.FinishDate;
                DeporteDto deportedto = new DeporteDto();
                deportedto.Id = campeonato.Deporte.Id;
                deportedto.Nombre = campeonato.Deporte.Nombre;
                deportedto.Image = campeonato.Deporte.Image;
                pencainfo.Deporte = deportedto;
                List<EventoPrediccionDto> eventos = new List<EventoPrediccionDto>();
                var puntaje = _puntajeService.getPuntajeById(penca.PuntajeId);
                int? puntajeTotal = 0;
                foreach (var evento in penca.Campeonato.Eventos)
                {
                    var prediccion = _prediccionService.GetPrediccionByUsuarioEvento(Convert.ToInt32(userId), evento.Id, penca.Id);
                    var resultado = _resultadoService.getResultadoByEventoId(evento.Id);
                    var equipolocal = _equipoService.getEquipoById(evento.EquipoLocalId);
                    var equipovisitante = _equipoService.getEquipoById(evento.EquipoVisitanteId);

                    EventoPrediccionDto eventoinfo = new EventoPrediccionDto
                    {
                        Id = evento.Id,
                        EquipoLocal = equipolocal,
                        EquipoVisitante = equipovisitante,
                        FechaInicial = evento.FechaInicial,
                        Resultado = resultado,
                        Prediccion = prediccion
                    };
                    eventos.Add(eventoinfo);
                    if (prediccion != null)
                    {
                        if (prediccion.Score != null)
                        {
                            puntajeTotal += prediccion.Score;
                        }
                    }
                }
                pencainfo.Eventos = eventos;
                pencainfo.PuntajeTotal = puntajeTotal;

                return Ok(pencainfo);

            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [HttpGet("{id}/usuarios")]
        public ActionResult<IEnumerable<UsuarioScore>> GetUsuariosPencaCompartida(int id)
        {
            try
            {
                var usuarios = _prediccionService.GetUsuariosByPenca(id);
                return Ok(usuarios);

            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

    }
}

