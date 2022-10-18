using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HandleErrorController : ControllerBase
    {
        private readonly ILogger<HandleErrorController> _logger;

        public HandleErrorController(ILogger<HandleErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            if (exceptionHandlerFeature.Error is HttpResponseException)
            {
                HttpResponseException error = (HttpResponseException)exceptionHandlerFeature.Error;
                return Problem(
                statusCode: error.StatusCode,
                detail: (string?)error.Value,
                title: exceptionHandlerFeature.Error.Message);
            }
            

            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();
    }
}

