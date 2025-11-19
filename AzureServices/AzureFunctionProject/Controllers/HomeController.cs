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

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        // http://localhost:7156/api/OnSalesUploadWriteToQueue
        [HttpPost]
        public async Task<IActionResult> Index(SalesRequest salesRequest)
        {

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:7156/api/");
            using var content = new StringContent(JsonSerializer.Serialize<SalesRequest>(salesRequest));
            var result = await client.PostAsync("OnSalesUploadWriteToQueue", content);
            string returnValue = await result.Content.ReadAsStringAsync();

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
