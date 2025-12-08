using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using dm = WebApplication1.DomainModels;
using WebApplication1.Models;
using WebApplication1.RepositryPattern;
using Microsoft.Extensions.Options;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HRMSContext context;
        private readonly ProductService _productService;

        public HomeController(ILogger<HomeController> logger, HRMSContext context, ProductService productService, IOptions<dm.ApiSettings> options)
        {
            _logger = logger;
            this.context = context;
            this._productService = productService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid) {
                // Custom logic to validate user creds
                var claims = new List<Claim>();

                if (model.UserName == "nraina" && model.Password == "nraina")
                {
                    // Generate Auth Cookie
                    claims.Add(new Claim(ClaimTypes.Name, model.UserName));
                    claims.Add(new Claim(ClaimTypes.Role, "admin"));
                }
                else {
                    ModelState.AddModelError(string.Empty, "Login failed");
                    return View(model);
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        public IActionResult Forbidden() {
            return View();
        }

        [Authorize]
        public Task AddProduct() {
            string pid = Guid.NewGuid().ToString();
            return _productService.AddProduct(new Product { 
                Name = pid,
                Description = "Test",
                Price = 154M,
                SKU = pid
            });
        }

        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [Authorize(Roles = "customer")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddEmployees([FromBody]dm.Employee emp) {
            try
            {
                var employee = context.Employees.Find(emp.Id);
                employee.Name = emp.Name;
                employee.Version = Guid.NewGuid();

                context.Database.ExecuteSql($"UPDATE dbo.Employees SET Version = '5E3C6AE1-36DF-4331-9426-BD8C386B00C5' WHERE Id = 1");

                context.SaveChanges();

                return Ok(1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        public IActionResult GetEmployee()
        {
            var deptCount = from d in context.Departments
                            join e in context.Employees
                            on d.Id equals e.DepartmentId
                            select new { Emp = e.Name, Dep = d.Name };

            var deptCountlist = from d in context.Departments
                                join e in context.Employees
                                on d.Id equals e.DepartmentId into gj
                                select new { Dept = d.Name, Count = gj.Count() };

            var deptCountlistLeftJoin = from d in context.Departments
                                join e in context.Employees
                                on d.Id equals e.DepartmentId into gj
                                from sub in gj.DefaultIfEmpty()
                                select new { Emp = sub.Name, Dep = d.Name };

            var employee = context.Employees.Include(x => x.Department).Select(x => new 
            {
                Id = x.Id,
                Name = x.Name,
                DepartmentId = x.Department.Id,
                Version = x.Version
            }).First();

            return Ok(employee);
        }
    }
}
