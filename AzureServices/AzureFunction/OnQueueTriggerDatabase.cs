using System;
using System.Text.Json;
using Azure.Storage.Queues.Models;
using AzureFunction.Data;
using AzureFunction.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunction;

public class OnQueueTriggerDatabase
{
    private readonly ILogger<OnQueueTriggerDatabase> _logger;
    private readonly SalesDbContext _context;

    public OnQueueTriggerDatabase(ILogger<OnQueueTriggerDatabase> logger, SalesDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [Function(nameof(OnQueueTriggerDatabase))]
    public void Run([QueueTrigger("SalesRequestInBound", Connection = "")] QueueMessage message)
    {
        SalesRequest salesRequest = JsonSerializer.Deserialize<SalesRequest>(message.Body);

        if (salesRequest != null) {
            salesRequest.Status = "";
            _context.SalesRequests.Add(salesRequest);
            _context.SaveChanges();
          _logger.LogInformation("Saved", message.MessageText);
        }
    }
}