using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Models
{
    public class TransactionCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public required string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User? User { get; set; }
    }
}