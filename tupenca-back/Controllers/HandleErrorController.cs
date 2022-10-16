using System;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tupenca_back.Services.Exceptions;

namespace tupenca_back.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HandleErrorController : ControllerBase
    {
       
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            HttpResponseException error = (HttpResponseException) exceptionHandlerFeature.Error;

            return Problem(
                statusCode: error.StatusCode,
                detail: (string?) error.Value,
                title: exceptionHandlerFeature.Error.Message);
        }

        [Route("/error")]
        public IActionResult HandleError() =>
            Problem();
    }
}

