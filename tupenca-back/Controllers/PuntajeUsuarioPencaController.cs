using System;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using AutoMapper;

namespace tupenca_back.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/puntaje-usuario-penca")]
    public class PuntajeUsuarioPencaController : ControllerBase
    {

        private readonly ILogger<AdministradorController> _logger;
        public readonly IMapper _mapper;
        private readonly PuntajeUsuarioPencaService _puntajeUsuarioPencaService;

        public PuntajeUsuarioPencaController(ILogger<AdministradorController> logger,
                                             IMapper mapper,
                                             PuntajeUsuarioPencaService puntajeUsuarioPencaService)
        {
            _logger = logger;
            _mapper = mapper;
            _puntajeUsuarioPencaService = puntajeUsuarioPencaService;
        }

        //GET: api/puntaje-usuario-penca/pencas/1
        [HttpGet("pencas/{id}")]
        public ActionResult<IEnumerable<PuntajeUsuarioPencaDto>> GetPuntajeUsuarioPorPenca(int id)
        {
            try
            {
                var puntajes = _puntajeUsuarioPencaService.GetAllByPenca(id);
                var puntajesDto = _mapper.Map<List<PuntajeUsuarioPencaDto>>(puntajes);

                return Ok(puntajesDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        //GET: api/puntaje-usuario-penca/usuarios/1
        [HttpGet("usuarios/{id}")]
        public ActionResult<IEnumerable<PuntajeUsuarioPencaDto>> GetPuntajeUsuarioPorUsuario(int id)
        {
            try
            {
                var puntajes = _puntajeUsuarioPencaService.GetAllByUsuario(id);
                var puntajesDto = _mapper.Map<List<PuntajeUsuarioPencaDto>>(puntajes);

                return Ok(puntajesDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}

