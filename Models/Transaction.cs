using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace FinancialApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        [BindNever]
        //[Required]
        public string Type { get; set; } = string.Empty;


        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public int? CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public Account? Account { get; set; }

        public TransactionCategory? Category { get; set; }
    }

}
