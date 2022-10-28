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
    [Route("api/premios")]
    [Authorize]
    public class PremioController : ControllerBase
    {

        private readonly ILogger<PremioController> _logger;
        public readonly IMapper _mapper;
        private readonly PremioService _premioService;

        public PremioController(ILogger<PremioController> logger,
                                IMapper mapper,
                                PremioService premioService)
        {
            _logger = logger;
            _mapper = mapper;
            _premioService = premioService;
        }


        //GET: api/premios
        [HttpGet]
        public ActionResult<IEnumerable<PremioDto>> GetPremios()
        {
            try
            {
                var premios = _premioService.GetPremios();

                var premiosDto = _mapper.Map<List<PremioDto>>(premios);

                return Ok(premiosDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //GET: api/premios/1
        [HttpGet("{id}")]
        public ActionResult<PremioDto> GetPremio(int id)
        {
            try
            {
                var premio = _premioService.FindPremioById(id);

                if (premio == null)
                {
                    return NotFound();
                }
                else
                {
                    var premioDto = _mapper.Map<PremioDto>(premio);

                    return Ok(premioDto);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //POST: api/premios
        [HttpPost]
        public IActionResult PostPremio(PremioDto premioDto)
        {
            if (premioDto == null)
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "El Premio no debe ser nulo");

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var premio = _mapper.Map<Premio>(premioDto);

                _premioService.AddPremio(premio);

                return CreatedAtAction("GetPremio", new { id = premio.Id }, _mapper.Map<PremioDto>(premio));
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



        // PUT: api/premios/1
        [HttpPut("{id}")]
        public IActionResult PutPremio(int id, PremioDto premioDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var premio = _mapper.Map<Premio>(premioDto);

                _premioService.UpdatePremio(id, premio);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // DELETE: api/premios/1
        [HttpDelete("{id}")]
        public IActionResult DeletePremio(int id)
        {
            try
            {
                _premioService.RemovePremio(id);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}

