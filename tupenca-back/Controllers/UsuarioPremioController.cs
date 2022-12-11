using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Controllers;
using tupenca_back.Controllers.Dto;
using tupenca_back.DataAccess.Migrations;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Services.Exceptions;

namespace tupenca_back
{
    [ApiController]
    [Authorize]
    [Route("api/usuario-premio")]
    public class UsuarioPremioController : ControllerBase
    {
        private readonly ILogger<DeporteController> _logger;
        private readonly IMapper _mapper;
        private readonly UsuarioPremioService _usuarioPremioService;

        public UsuarioPremioController(ILogger<DeporteController> logger,
                                       UsuarioPremioService usuarioPremioService,
                                       IMapper mapper)
        {
            _logger = logger;
            _usuarioPremioService = usuarioPremioService;
            _mapper = mapper;
        }


        //GET: api/usuario-premio       
        [HttpGet]
        public ActionResult<IEnumerable<UsuarioPremioDto>> GetUsuariosPremio([FromQuery(Name = "idUsuario")] int idUsuario,
                                                                             [FromQuery(Name = "idPenca")] int idPenca)
        {
            try
            {
                var usuariosPremio = _usuarioPremioService.GetUsuariosPremio(idUsuario, idPenca);
                var usuariosPremioDto = _mapper.Map<List<UsuarioPremioDto>>(usuariosPremio);
                return Ok(usuariosPremioDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // GET: api/usuario-premio/1        
        [HttpGet("{id:int}")]
        public ActionResult<UsuarioPremioDto> GetUsuarioPremio(int id)
        {
            try
            {
                var usuarioPremio = _usuarioPremioService.GetUsuarioPremioById(id);
                if (usuarioPremio == null)
                {
                    return NotFound();
                }
                else
                {
                    var usuarioPremioDto = _mapper.Map<UsuarioPremioDto>(usuarioPremio);
                    return Ok(usuarioPremioDto);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // PUT: api/usuario-premio/1
        [HttpPut("{id:int}/facturacion")]
        public IActionResult AddDatosFacturacion(int id, UsuarioPremioDto usuarioPremioDto)
        {
            try
            {
                var usuarioPremio = _usuarioPremioService.AddDatosFacturacion(id, _mapper.Map<UsuarioPremio>(usuarioPremioDto));

                return NoContent();
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


        // PUT: api/usuario-premio/1
        [HttpPut("{id:int}/reclamar")]
        public IActionResult ReclamarPremio(int id)
        {
            try
            {
                var usuarioPremio = _usuarioPremioService.ReclamarPremio(id);

                return NoContent();
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

        // PUT: api/usuario-premio/1
        [HttpPut("{id:int}/pagar")]
        public IActionResult PagarPremio(int id)
        {
            try
            {
                var usuarioPremio = _usuarioPremioService.PagarPremio(id);

                return NoContent();
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

    }
}

