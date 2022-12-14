using System;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using tupenca_back.Services.Exceptions;
using System.Net;
using AutoMapper;
using System.Collections.Generic;
using tupenca_back.Controllers.Dto;
using Microsoft.AspNetCore.Authorization;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/campeonatos")]
    public class CampeonatoController : ControllerBase
    {
        private readonly ILogger<CampeonatoController> _logger;
        public readonly IMapper _mapper;
        private readonly CampeonatoService _campeonatoService;

        public CampeonatoController(ILogger<CampeonatoController> logger, IMapper mapper, CampeonatoService campeonatoService)
        {
            _logger = logger;
            _mapper = mapper;
            _campeonatoService = campeonatoService;
        }

        //GET: api/campeonatos
        [HttpGet]
        public  ActionResult<IEnumerable<CampeonatoDto>> GetCampeonatos([FromQuery] string? searchString = null, [FromQuery] bool? finalizados = null)
        {
            try
            {
                if (searchString == null)
                {
                    if(finalizados == null)
                    {
                        var campeonatos = _campeonatoService.getCampeonatos();
                        var campeonatosDto = _mapper.Map<List<CampeonatoDto>>(campeonatos);
                        return Ok(campeonatosDto);
                    }
                    if(finalizados == true)
                    {
                        var campeonatos = _campeonatoService.GetCampeonatosFinalizados();
                        var campeonatosDto = _mapper.Map<List<CampeonatoDto>>(campeonatos);
                        return Ok(campeonatosDto);
                    }
                    else
                    {
                        var campeonatos = _campeonatoService.GetCampeonatosNoFinalizados();
                        var campeonatosDto = _mapper.Map<List<CampeonatoDto>>(campeonatos);
                        return Ok(campeonatosDto);
                    }
                }
                else
                {
                    var campeonatos = _campeonatoService.SearchCampeonato(searchString);
                    var campeonatosDto = _mapper.Map<List<CampeonatoDto>>(campeonatos);
                    return Ok(campeonatosDto);
                }

            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            } 
        }

        // GET: api/campeonatos/1
        [HttpGet("{id}")]
        public ActionResult<CampeonatoDto> GetCampeonato(int id)
        {
            try
            {
                var campeonato = _campeonatoService.findCampeonatoById(id);

                if (campeonato == null)
                {
                    return NotFound();
                }
                else
                {
                    var campeonatoDto = _mapper.Map<CampeonatoDto>(campeonato);
                    return Ok(campeonatoDto);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // POST: api/campeonatos
        [HttpPost]
        public IActionResult PostCampeonato(CampeonatoDto campeonatoDto)
        {
            if (campeonatoDto == null)
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Campeonato no debe ser nulo");
            

            if (_campeonatoService.CampeonatoNameExists(campeonatoDto.Name))
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Nombre campeonato repetido");

            try
            {
                var campeonato = _mapper.Map<Campeonato>(campeonatoDto);

                _campeonatoService.AddCampeonato(campeonato);


                return CreatedAtAction("GetCampeonato", new { id = campeonatoDto.Id }, _mapper.Map<CampeonatoDto>(campeonato));
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int) HttpStatusCode.NotFound, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // PUT: api/campeonatos/1
        [HttpPut("{id}")]
        public IActionResult PutCampeonato(int id, CampeonatoDto campeonatoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_campeonatoService.CampeonatoExists(id))
                return NotFound();

            try
            {
                var campeonato = _mapper.Map<Campeonato>(campeonatoDto);

                _campeonatoService.UpdateCampeonato(id, campeonato);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // DELETE: api/campeonatos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteEvento(int id)
        {

            var campeonato = _campeonatoService.findCampeonatoById(id);

            if (campeonato == null)
                return NotFound();

            try
            {
                _campeonatoService.RemoveCampeonato(campeonato);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // Patch: api/campeonatos/1/eventos
        [HttpPatch("{id}/eventos")]
        public IActionResult AddEvento(int id, EventoDto eventoDto)
        {
            try
            {
                var evento = _mapper.Map<Evento>(eventoDto);

                _campeonatoService.addEvento(id, evento);

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


        // PATCH: api/deportes/1/image        
        [HttpPatch("{id}/image")]
        public ActionResult UploadImage(int id, [FromForm] ImagenDto imagenDto)
        {
            try
            {
                _campeonatoService.SaveImagen(id, imagenDto.file);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


    }
}

