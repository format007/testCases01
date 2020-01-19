using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LinkResolver.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            string CorrelationId = Guid.NewGuid().ToString();

            var feature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = feature?.Error;

            string ErrorMessage = $"Something went wrong. Use this correlation id to investigate problem. CorrelationId: {CorrelationId}";

            logger.LogError(ex, ErrorMessage);

            var problemDetails = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = feature?.Path,
                Title = $"Something went wrong. Use this correlation id to investigate problem. CorrelationId: {CorrelationId}",
                Detail = null
            };

            return StatusCode(problemDetails.Status.Value, problemDetails);
        }
    }
}