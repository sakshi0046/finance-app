using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Models.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; } // Admin/User
    }

}
