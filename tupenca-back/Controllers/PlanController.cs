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
    [Route("api/planes")]
    [Authorize]
    public class PlanController : ControllerBase
    {

        private readonly ILogger<PlanController> _logger;
        public readonly IMapper _mapper;
        private readonly PlanService _planService;

        public PlanController(ILogger<PlanController> logger,
                              IMapper mapper,
                              PlanService planService)
        {
            _logger = logger;
            _mapper = mapper;
            _planService = planService;
        }

        //GET: api/planes
        [HttpGet]
        public ActionResult<IEnumerable<PlanDto>> GetPlanes()
        {
            try
            {
                var planes = _planService.GetPlanes();

                var planesDto = _mapper.Map<List<PlanDto>>(planes);

                return Ok(planesDto);
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //GET: api/planes/1
        [HttpGet("{id}")]
        public ActionResult<PlanDto> GetPlan(int id)
        {
            try
            {
                var plan = _planService.FindPlanById(id);

                if (plan == null)
                {
                    return NotFound();
                }
                else
                {
                    var planDto = _mapper.Map<PlanDto>(plan);

                    return Ok(planDto);
                }
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        //POST: api/planes
        [HttpPost]
        public IActionResult PostPlan(PlanDto planDto)
        {
            if (planDto == null)
                throw new HttpResponseException((int)HttpStatusCode.BadRequest, "El Plan no debe ser nulo");

            try
            {
                var plan = _mapper.Map<Plan>(planDto);

                _planService.AddPlan(plan);

                return CreatedAtAction("GetPlan", new { id = plan.Id }, _mapper.Map<PlanDto>(plan));
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


        // PUT: api/planes/1
        [HttpPut("{id}")]
        public IActionResult PutPlan(int id, PlanDto planDto)
        {
            try
            {
                var plan = _mapper.Map<Plan>(planDto);

                _planService.UpdatePlan(id, plan);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        // DELETE: api/planes/1
        [HttpDelete("{id}")]
        public IActionResult DeletePlan(int id)
        {
            try
            {
                _planService.RemovePlan(id);

                return NoContent();
            }
            catch (Exception e)
            {
                throw new HttpResponseException((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }
    }
}

