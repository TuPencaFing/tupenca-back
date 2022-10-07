using System;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;

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
      
    }
}

