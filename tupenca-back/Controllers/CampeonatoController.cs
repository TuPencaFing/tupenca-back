using System;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/campeonatos")]
    public class CampeonatoController : ControllerBase
    {
        private readonly ILogger<CampeonatoController> _logger;
        private readonly CampeonatoService _campeonatoService;

        public CampeonatoController(ILogger<CampeonatoController> logger, CampeonatoService campeonatoService)
        {
            _logger = logger;
            _campeonatoService = campeonatoService;
        }

        //GET: api/campeonatos
        [HttpGet]
        public  ActionResult<IEnumerable<Campeonato>> GetCampeonatos()
        {
            return Ok(_campeonatoService.getCampeonatos());
        }

        // GET: api/campeonatos/1

        [HttpGet("{id}")]
        public ActionResult<Campeonato> GetCampeonato(int id)
        {
            var campeonato = _campeonatoService.findCampeonato(id);

            if (campeonato == null)
            {
                return NotFound();
            } else
            {
                return campeonato;
            }
        }


        // POST: api/campeonatos
        [HttpPost]
        public ActionResult<Campeonato> PostCampeonato(Campeonato campeonato)
        {
            if (_campeonatoService.CampeonatoNameExists(campeonato.Name))
            {
                throw new HttpResponseException(400, "Nombre campeonato repetido");
            }

            _campeonatoService.AddCampeonato(campeonato);

            return CreatedAtAction("GetCampeonato", new { id = campeonato.Id }, campeonato);
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




    }
}

