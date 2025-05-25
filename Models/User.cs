using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        [Required]
        public string? Role { get; set; } // Admin/User
        public DateTime CreatedAt { get; set; }
    }


}
