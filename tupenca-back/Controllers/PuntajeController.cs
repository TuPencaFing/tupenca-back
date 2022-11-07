using System;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Controllers.Dto;
using tupenca_back.Model;
using tupenca_back.Services;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Route("api/puntajes")]
    [Authorize]
    public class PuntajeController : ControllerBase
    {

        private readonly ILogger<PremioController> _logger;
        public readonly IMapper _mapper;
        private readonly PuntajeService _puntajeService;

        public PuntajeController(ILogger<PremioController> logger,
                                IMapper mapper,
                                PuntajeService puntajeService)
        {
            _logger = logger;
            _mapper = mapper;
            _puntajeService = puntajeService;
        }


        //GET: api/puntajes
        [HttpGet]
        public ActionResult<IEnumerable<Puntaje>> GetPuntajes()
        {
            try
            {
                var puntajes = _puntajeService.getPuntajes();

                //var puntajesDto = _mapper.Map<List<PuntajeDto>>(puntajes);

                return Ok(puntajes);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //GET: api/premios/1
        [HttpGet("{id}")]
        public ActionResult<Puntaje> GetPuntaje(int id)
        {
            try
            {
                var puntaje = _puntajeService.getPuntajeById(id);

                if (puntaje == null)
                {
                    return NotFound();
                }
                else
                {
                    //var puntajeDto = _mapper.Map<PuntajeDto>(puntaje);

                    return Ok(puntaje);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //POST: api/puntaje
        [HttpPost]
        public IActionResult PostPuntaje(Puntaje puntaje)
        {
            if (puntaje == null)
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "El puntaje no debe ser nulo");

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                //var puntaje = _mapper.Map<Puntaje>(puntajeDto);

                _puntajeService.CreatePuntaje(puntaje);

                //return CreatedAtAction("GetPuntaje", new { id = puntaje.Id }, _mapper.Map<PuntajeDto>(puntaje));
                return CreatedAtAction("GetPuntaje", new { id = puntaje.Id }, puntaje);
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


        // DELETE: api/puntaje/1
        [HttpDelete("{id}")]
        public IActionResult DeletPuntaje(int id)
        {
            try
            {
                var puntaje = _puntajeService.getPuntajeById(id);

                if (puntaje == null)
                {
                    return NotFound();
                }
                
                _puntajeService.RemovePuntaje(puntaje);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}

