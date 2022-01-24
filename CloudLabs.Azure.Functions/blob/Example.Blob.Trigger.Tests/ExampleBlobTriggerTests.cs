using Microsoft.Extensions.Logging;
using NSubstitute;
using Shouldly;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Example.Blob.Trigger.Tests
{
    public class ExampleBlobTriggerTests
    {
        [Fact]
        public async Task Validate_Proper_File_Processing()
        {

            var exampleHttpTrigger = new ExampleBlobTrigger();

            using (var ms = new MemoryStream())
            {
                var sw = new StreamWriter(ms);
                var sr = new StreamReader(ms);
                ms.Position = 0;

                await sw.WriteLineAsync("Azure Function Blob Test");

                var exception = await Record.ExceptionAsync(() => exampleHttpTrigger.Run(ms, "testBlob", Substitute.For<ILogger<ExampleBlobTrigger>>()));
                exception.ShouldBeNull();
            }
        }
    }
}