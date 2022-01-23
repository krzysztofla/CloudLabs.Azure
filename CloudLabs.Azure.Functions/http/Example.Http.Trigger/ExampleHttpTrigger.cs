using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Example.Http.Trigger
{
    public class ExampleHttpTrigger
    {
        private readonly ILogger<ExampleHttpTrigger> _logger;

        private static Dictionary<string, string> items = new() { { "1N3244", "SNB" }, { "1N3243", "7N1" }, { "1N3233", "PS1" } };

        public ExampleHttpTrigger(ILogger<ExampleHttpTrigger> log)
        {
            _logger = log;
        }

        [FunctionName("Materials")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "code" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiParameter(name: "code", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "Provide item code")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Get item by item code")]
        public async Task<IActionResult> GetMaterialAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Materials/{code}")] HttpRequest req)
        {
            _logger.LogInformation("Processing item search.");

            string code = req.Query["code"];

            if (code is null)
            {
                return new BadRequestObjectResult(code);
            }

            var item = items[code];

            if(item is null)
            {
                return new NotFoundObjectResult("There is no such item.");
            }

            return new OkObjectResult(item);
        }

    }
}

