namespace PaymentSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public decimal Amount { get; set; }
    }
}