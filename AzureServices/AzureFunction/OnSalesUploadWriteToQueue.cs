using AzureFunction.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AzureFunction
{
    public class OnSalesUploadWriteToQueue
    {
        private readonly ILogger<OnSalesUploadWriteToQueue> _logger;

        public OnSalesUploadWriteToQueue(ILogger<OnSalesUploadWriteToQueue> logger)
        {
            _logger = logger;
        }

        [Function("OnSalesUploadWriteToQueue")]
        [QueueOutput("SalesRequestInBound", Connection = "AzureWebJobsStorage")]
        public async Task<SalesRequest> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string requestData = await new StreamReader(req.Body).ReadToEndAsync();
            SalesRequest? srquest = JsonSerializer.Deserialize<SalesRequest>(requestData);
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return srquest ?? new SalesRequest();
        }
    }
}
