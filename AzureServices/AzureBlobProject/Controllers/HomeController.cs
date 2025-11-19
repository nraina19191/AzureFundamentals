using System.Diagnostics;
using AzureBlobProject.Models;
using AzureBlobProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureBlobProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContainerService _containerService;

        public HomeController(ILogger<HomeController> logger, IContainerService containerService)
        {
            _logger = logger;
            _containerService = containerService;
        }

        public async Task<IActionResult> Index()
        {
            var containers = await _containerService.GetAllContainer();

            return View(containers);
        }

        public async Task<IActionResult> Create()
        {
            return View(new AzureContainer());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AzureContainer container)
        {
            await _containerService.CreateContainer(container.ContainerName);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string containerName)
        {
            await _containerService.DeleteContainer(containerName);
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
