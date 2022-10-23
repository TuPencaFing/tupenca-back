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
        public  ActionResult<IEnumerable<Campeonato>> GetCampeonatos()
        {
            var campeonatos = _campeonatoService.getCampeonatos();

            var campeonatosDto = _mapper.Map<List<CampeonatoDto>>(campeonatos);

            return Ok(campeonatosDto);
        }

        // GET: api/campeonatos/1

        [HttpGet("{id}")]
        public ActionResult<Campeonato> GetCampeonato(int id)
        {
            var campeonato = _campeonatoService.findCampeonato(id);

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


        // POST: api/campeonatos
        [HttpPost]
        public ActionResult<Campeonato> PostCampeonato(Campeonato campeonato)
        {
            if (campeonato == null)
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Campeonato no debe ser nulo");
            }

            if (_campeonatoService.CampeonatoNameExists(campeonato.Name))
            {
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "Nombre campeonato repetido");
            }

            try
            {
                _campeonatoService.AddCampeonato(campeonato);

                return CreatedAtAction("GetCampeonato", new { id = campeonato.Id }, campeonato);
            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int) HttpStatusCode.NotFound, e.Message);
            }
           
        }


        // PUT: api/campeonatos/1
        [HttpPut("{id}")]
        public ActionResult<Campeonato> PutCampeonato(int id, Campeonato campeonato)
        {

            if (!ModelState.IsValid)
                return BadRequest();


            if (id != campeonato.Id)
                return BadRequest();

            if (_campeonatoService.CampeonatoExists(id))
                return NotFound();


            _campeonatoService.UpdateCampeonato(id, campeonato);

            return CreatedAtAction("GetCampeonato", new { id = campeonato.Id }, campeonato);
        }


        // DELETE: api/campeonatos/1
        [HttpDelete("{id}")]
        public IActionResult DeleteEvento(int id)
        {

            var campeonato = _campeonatoService.findCampeonato(id);

            if (campeonato == null)
            {
                return NotFound();
            }

            _campeonatoService.RemoveCampeonato(campeonato);

            return NoContent();
        }

        // PUT: api/campeonatos/1/eventos
        [HttpPut("{id}/eventos")]
        public ActionResult<Campeonato> AddEvento(int id, Evento evento)
        {
            try
            {
                return _campeonatoService.addEvento(id, evento);

            }
            catch (NotFoundException e)
            {
                throw new HttpResponseException((int)HttpStatusCode.NotFound, e.Message);
            }

        }



    }
}

