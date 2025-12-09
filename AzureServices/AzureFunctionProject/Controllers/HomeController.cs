using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureFunctionProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace AzureFunctionProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly BlobServiceClient _blobServiceClient;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _blobServiceClient = blobServiceClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        // http://localhost:7156/api/OnSalesUploadWriteToQueue
        [HttpPost]
        public async Task<IActionResult> Index(SalesRequest salesRequest, IFormFile file)
        {

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:7156/api/");
            using var content = new StringContent(JsonSerializer.Serialize<SalesRequest>(salesRequest));
            var result = await client.PostAsync("OnSalesUploadWriteToQueue", content);
            string returnValue = await result.Content.ReadAsStringAsync();

            if (file != null) {
                var fileName = file.FileName;
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient("functionsalesrep");
                var bloblClient = containerClient.GetBlobClient(fileName);

                var httpHeaders = new BlobHttpHeaders()
                {
                    ContentType = file.ContentType
                };

                await bloblClient.UploadAsync(file.OpenReadStream(), httpHeaders);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
