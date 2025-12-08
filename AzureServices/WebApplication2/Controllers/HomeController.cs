using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        IConfiguration _config;
        IHttpClientFactory _httpClientFactory;

        public HomeController(IConfiguration config, IHttpClientFactory httpClientFactory)
        {
            this._config = config;
            this._httpClientFactory = httpClientFactory; ;
        }

        [Authorize]
        [Route("employee")]
        [HttpGet]
        public IActionResult GetEmployees()
        {
            return Ok(new
            {
                Id = 1,
                Name = User.Identity?.Name
            });
        }

        [Route("posts")]
        public async Task<IActionResult> GetPosts() {
            var client = this._httpClientFactory.CreateClient();
            var posts = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");

            return Ok(posts.Content.ReadAsStringAsync());
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            // Validates the login creds
            if (loginModel.Email == "nraina@gmail.com")
            {
                // Generate JWT Token
                var token = GenerateToken(loginModel.Email);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        public string GenerateToken(string email)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "admin"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
