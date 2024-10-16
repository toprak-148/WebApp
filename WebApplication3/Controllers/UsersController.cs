using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; // JsonConvert için
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebApplication3.Models;

public class UsersController : Controller
{
    private readonly IConfiguration _configuration;

    public UsersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        using (var client = new HttpClient())
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Account");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://localhost:7034/api/Users/GetUsers");
            if (!response.IsSuccessStatusCode)
                return Unauthorized();

            var result = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<UserModel>>(result); // Burada JsonConvert kullanılmalı
            return View(users);
        }
    }
}



//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration; // IConfiguration için eklenen namespace
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Threading.Tasks;
//using WebApplication3.Models;

//public class UsersController : Controller
//{
//    private readonly IConfiguration _configuration;

//    public UsersController(IConfiguration configuration)
//    {
//        _configuration = configuration;
//    }

//    [Authorize]
//    public async Task<IActionResult> Index()
//    {
//        using (var client = new HttpClient())
//        {
//            var token = HttpContext.Session.GetString("JWToken");
//            if (string.IsNullOrEmpty(token))
//                return RedirectToAction("Login", "Account");

//            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

//            var apiUrl = _configuration["ApiUrl"]; // ApiUrl burada appsettings.json'dan alınabilir

//            var response = await client.GetAsync($"{apiUrl}/api/Users/GetUsers");
//            if (!response.IsSuccessStatusCode)
//                return Unauthorized();

//            var result = await response.Content.ReadAsStringAsync();

//            try
//            {
//                var users = JsonConvert.DeserializeObject<List<UserModel>>(result);
//                return View(users);
//            }
//            catch (Exception ex)
//            {
//                // Handle JSON deserialization errors
//                ModelState.AddModelError("", "Veriler çözümlenirken bir hata oluştu.");
//                return View();
//            }

//        }
//    }
//}

