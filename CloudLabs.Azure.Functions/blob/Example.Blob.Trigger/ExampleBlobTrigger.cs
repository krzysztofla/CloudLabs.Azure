using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Example.Blob.Trigger
{
    public class ExampleBlobTrigger
    {
        [FunctionName("ExampleBlobTrigger")]
        public async Task Run([BlobTrigger("azure-functions-example/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob} Bytes");
            try
            {
                using (var reader = new StreamReader(myBlob))
                {
                    var text = await reader.ReadToEndAsync();
                    log.LogInformation("End of file processing");
                }
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
            }
        }
    }
}
