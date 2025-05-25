using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace FinancialApp.Models
{

    public class Account
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required]
        public string AccountName { get; set; } = string.Empty;

        [Required]
        public string AccountType { get; set; } = string.Empty;

        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

        [ValidateNever]
        public User User { get; set; } = null!;
    }

}
