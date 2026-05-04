using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        // 🔥 FOREIGN KEY
        [Required]
        public int UserId { get; set; }

        // 🔥 PAYMENT DETAILS

        [Required]
        [StringLength(100)]
        public string RecipientName { get; set; }

        [Required]
        [RegularExpression(@"^\d{10,16}$", ErrorMessage = "Invalid Account Number")]
        public string AccountNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string BankName { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{6}[A-Z0-9]{2,5}$", ErrorMessage = "Invalid SWIFT Code")]
        public string SwiftCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(1, 1000000000)]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; }

        // 🔥 SYSTEM FIELDS

        public string Status { get; set; } = "Pending"; // Pending → Verified → SentToSWIFT

        public DateTime DateCreated { get; set; } = DateTime.Now;

        // 🔥 NAVIGATION
        public User? User { get; set; }
    }
}