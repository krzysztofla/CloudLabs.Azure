using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Shouldly;

namespace Example.Http.Trigger.Tests
{
    public class HttpTriggerGetMaterialTests
    {
        [Fact]
        public async Task Example_Test_Of_Http_Trigger_Query_Parameter()
        {
            var exampleHttpTrigger = new ExampleHttpTrigger(Substitute.For<ILogger<ExampleHttpTrigger>>());
            var query = new Dictionary<String, StringValues>();
            query.TryAdd("code", "1N3244");

            var context = new DefaultHttpContext();
            var request = context.Request;
            request.Query = new QueryCollection(query);

            var result = await exampleHttpTrigger.GetMaterialAsync(req: request);

            result.ShouldBeOfType<OkObjectResult>();
            result.ShouldNotBeNull();
        }
    }
}