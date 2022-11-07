using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services;
using tupenca_back.Model;
using tupenca_back.Controllers.Dto;
using System.Net;
using tupenca_back.Services.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace tupenca_back.Controllers
{
    [ApiController]
    [Authorize]
    public class PrediccionController : ControllerBase
    {
        private readonly ILogger<PrediccionController> _logger;
        private readonly PrediccionService _prediccionService;
        private readonly ResultadoService _resultadoService;

        public PrediccionController(ILogger<PrediccionController> logger, PrediccionService prediccionService, ResultadoService resultadoService)
        {
            _logger = logger;
            _prediccionService = prediccionService;
            _resultadoService = resultadoService;
        }

        // GET: api/predicciones/1        
        [HttpGet]
        [Route("api/predicciones/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Prediccion> GetPrediccionById(int id)
        {
            var prediccion = _prediccionService.getPrediccionById(id);
            if (prediccion == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prediccion);
            }
        }

        // GET: api/predicciones/evento/1        
        [HttpGet]
        [Route("api/predicciones/evento/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Prediccion> GetPrediccionByEventoPencaId(int eventoid, int pencaid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var prediccion = _prediccionService.getPrediccionByEventoId(eventoid, pencaid, Convert.ToInt32(userId));
            if (prediccion == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prediccion);
            }
        }

       
        // DELETE: api/predicciones/1
        [HttpDelete]
        [Route("api/predicciones/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeletePrediccion(int id)
        {
            var prediccion = _prediccionService.getPrediccionById(id);

            if (prediccion == null)
            {
                return NotFound();
            }

            _prediccionService.RemovePrediccion(prediccion);

            return NoContent();
        }
        

    }
}

