namespace eduMATE_back.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "Student"; // Default role
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
