﻿using System;
using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Services.Exceptions;
using MercadoPago.Client.Common;
using MercadoPago.Client.Payment;
using MercadoPago.Config;
using MercadoPago.Resource.Payment;

namespace tupenca_back.Controllers
{

    [ApiController]
    [Route("api/pencas-compartidas")]
    [Authorize]
    public class PencaCompartidaController : ControllerBase
    {

        private readonly ILogger<PencaCompartidaController> _logger;
        public readonly IMapper _mapper;
        private readonly PencaService _pencaService;
        private readonly PrediccionService _prediccionService;
        private readonly CampeonatoService _campeonatoService;
        private readonly EquipoService _equipoService;
        private readonly ResultadoService _resultadoService;
        private readonly PuntajeService _puntajeService;
        private readonly EventoService _eventoService;
        private readonly PuntajeUsuarioPencaService _puntajeUsuarioPencaService;

        public PencaCompartidaController(ILogger<PencaCompartidaController> logger,
                               IMapper mapper,
                               PencaService pencaService,
                               PrediccionService prediccionService,
                               CampeonatoService campeonatoService,
                               EquipoService equipoService,
                               ResultadoService resultadoService,
                               PuntajeService puntajeService,
                               EventoService eventoService,
                               PuntajeUsuarioPencaService puntajeUsuarioPencaService)
        {
            _logger = logger;
            _mapper = mapper;
            _pencaService = pencaService;
            _prediccionService = prediccionService;
            _campeonatoService = campeonatoService;
            _equipoService = equipoService;
            _resultadoService = resultadoService;
            _puntajeService = puntajeService;
            _eventoService = eventoService;
            _puntajeUsuarioPencaService = puntajeUsuarioPencaService;
        }

        //GET: api/pencas-compartidas
        [HttpGet]
        public ActionResult<IEnumerable<PencaCompartidaDto>> GetPencasCompartida()
        {
            try
            {
                var pencas = _pencaService.GetPencaCompartidas();

                var pencasDto = _mapper.Map<List<PencaCompartidaDto>>(pencas);

                return Ok(pencasDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //GET: api/pencas-compartidas/1
        [HttpGet("{id}")]
        public ActionResult<PencaCompartidaDto> GetPencaCompartida(int id)
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
                    var pencaDto = _mapper.Map<PencaCompartidaDto>(penca);

                    return Ok(pencaDto);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //POST: api/pencas-compartidas
        [HttpPost]
        public IActionResult PostPencaCompartida(PencaCompartidaDto pencaCompartidaDto)
        {
            if (pencaCompartidaDto == null)
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "La Penca no debe ser nulo");

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var pencaCompartida = _mapper.Map<PencaCompartida>(pencaCompartidaDto);

                _pencaService.AddPencaCompartida(pencaCompartida);

                return CreatedAtAction("GetPencaCompartida", new { id = pencaCompartida.Id }, _mapper.Map<PencaCompartidaDto>(pencaCompartida));
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


        // PUT: api/pencas-compartidas/1
        [HttpPut("{id}")]
        public IActionResult PutPencaCompartida(int id, PencaCompartidaDto pencaCompartidaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_pencaService.PencaCompartidaExists(id))
                return NotFound();

            try
            {
                var pencaCompartida = _mapper.Map<PencaCompartida>(pencaCompartidaDto);

                _pencaService.UpdatePencaCompartida(id, pencaCompartida);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // DELETE: api/pencas-compartidas/1
        [HttpDelete("{id}")]
        public IActionResult DeletePencaCompartida(int id)
        {
            try
            {
                _pencaService.RemovePencaCompartida(id);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //Pencas de cada usuario

        [HttpGet("me")]
        public ActionResult<IEnumerable<PencaCompartida>> GetPencasCompartidasByUsuario([FromQuery] bool joined = true)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (joined == true)
            {
                try
                {
                    var pencas = _pencaService.GetPencasCompartidasByUsuario(Convert.ToInt32(userId));
                    return Ok(pencas);
                }
                catch (Exception e)
                {
                    throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
                }
            }
            else
            {
                try
                {
                    var pencas = _pencaService.GetPencasCompartidasNoJoinedByUsuario(Convert.ToInt32(userId));
                    return Ok(pencas);
                }
                catch (Exception e)
                {
                    throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
                }
            }

        }

        [HttpPost("{id}/add")]
        public IActionResult AddUsuarioToPencaCompartida(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _pencaService.AddUsuarioToPencaCompartida(Convert.ToInt32(userId), id);
                return Ok();

            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // PATCH: api/deportes/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _pencaService.SaveImagen(id, imagenDto.file, true);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET: api/pencas-compartidas/1/evento/1/estadisticas     
        [HttpGet("{id}/evento/{idEvento}/estadisticas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EstadisticaEventoDto> GetEstadisticas(int id, int idEvento)
        {
            var penca = _pencaService.findPencaCompartidaById(id);
            if (penca == null) return NotFound();
            EstadisticaEventoDto eventodto = new EstadisticaEventoDto();
            var porcentajeLocal = _prediccionService.GetPorcentajeLocal(id, idEvento);
            eventodto.PorcentajeLocal= porcentajeLocal;
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

        [HttpGet("{id}/info")]
        public ActionResult<PencaInfoDto> GetInfoPenca(int id)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var penca = _pencaService.findPencaCompartidaById(id);                
                if (penca == null)
                {
                    return NotFound();
                }
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
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }       
    }
}

