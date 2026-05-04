using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        // 🔥 FK
        public int UserId { get; set; }

        [Required]
        public string RecipientName { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string BankName { get; set; }

        [Required]
        public string SwiftCode { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime DateCreated { get; set; } = DateTime.Now;

        // 🔥 NAVIGATION (SAFE)
        public User? User { get; set; }
    }
}