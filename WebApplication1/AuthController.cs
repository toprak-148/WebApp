//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//[Route("api/[controller]")]
//[ApiController]
//public class AuthController : ControllerBase
//{
//    private readonly IUserService _userService;
//    private readonly AppDbContext _context;

//    public AuthController(IUserService userService, AppDbContext context)
//    {
//        _userService = userService;
//        _context = context;
//    }

//    [HttpPost("login")]
//    public IActionResult Login([FromBody] LoginModel model)
//    {
//        var token = _userService.Authenticate(model.Username, model.Password);

//        if (token == null)
//            return Unauthorized(new { message = "Kullanıcı adı veya şifre yanlış" });

//        return Ok(new { token });
//    }

//    [Authorize]
//    [HttpGet("GetUsers")]
//    public async Task<ActionResult<IEnumerable<object>>> GetUsers()
//    {
//        var users = await _context.Users
//            .Select(u => new { u.Id, u.Adi, u.Soyadi, u.KullaniciAdi })
//            .ToListAsync();

//        return Ok(users);
//    }
//}

//public class LoginModel
//{
//    public string Username { get; set; }
//    public string Password { get; set; }
//}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly AppDbContext _context;

    public AuthController(IUserService userService, AppDbContext context)
    {
        _userService = userService;
        _context = context;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        var token = _userService.Authenticate(model.Username, model.Password);

        if (token == null)
            return Unauthorized(new { message = "Kullanıcı adı veya şifre yanlış" });

        return Ok(new { token });
    }

    [Authorize]
    [HttpGet("GetUsers")]

    public async Task<ActionResult<IEnumerable<object>>> GetUsers()
    {
        var users = await _context.Users
            .Select(u => new { u.Id, u.Adi, u.Soyadi, u.KullaniciAdi })
            .ToListAsync();

        return Ok(users);
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}
