using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public interface IUserService
{
    string Authenticate(string username, string password);
}

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public UserService(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public string Authenticate(string username, string password)
    {
        // Kullan c y  veritaban ndan al
        var user = _context.Users.SingleOrDefault(x => x.KullaniciAdi == username && x.Sifre == password);
        if (user == null) return null;

        // JWT ayarlar n  konfig rasyondan al
        var jwtSettings = _config.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];

        if (string.IsNullOrEmpty(secretKey))
        {
            throw new InvalidOperationException("JWT SecretKey boş veya boş olamaz.");
        }

        var key = Encoding.ASCII.GetBytes(secretKey);

        // TokenDescriptor olu tur
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Token olu tur
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
