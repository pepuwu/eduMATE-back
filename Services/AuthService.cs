using eduMATE_back.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eduMATE_back.Services
{
    public class AuthService
    {
        private readonly IConfiguration _config;
        private readonly List<User> _users = new List<User>();

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = _users.Count + 1,
                Email = request.Email,
                Password = hashedPassword,
                Role = request.Source.ToLower() == "web" ? "Teacher" : "Student"
            };

            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<string?> LoginAsync(LoginRequest request)
        {
            var user = _users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            // Generar el JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
