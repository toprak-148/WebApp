using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using WebApplication3.Models;
using System.Net.Http;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7034/api/Auth/login", content);

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış");
                    return View(model);
                }

                var result = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<dynamic>(result);
                var token = (string)jsonResult.token;

                HttpContext.Session.SetString("JWToken", token);
                return RedirectToAction("Index", "Users");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

