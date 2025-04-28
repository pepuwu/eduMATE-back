using eduMATE_back.Models;

namespace eduMATE_back.Services
{
    public class AuthService
    {
        // Simulación de base de datos en memoria
        private readonly List<User> _users = new List<User>();
        public async Task<User> RegisterAsync(RegisterRequest request)
        {
            string role = request.Source.ToLower() == "web" ? "Teacher" : "Student";

            var user = new User
            {
                Id = _users.Count + 1,
                Email = request.Email,
                Password = request.Password,
                Role = role
            };

            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<User?> LoginAsync(LoginRequest request)
        {
            var user = _users.FirstOrDefault(u => 
                u.Email == request.Email && 
                u.Password == request.Password);

            return await Task.FromResult(user);
        }
    }
}
