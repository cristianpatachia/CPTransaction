namespace CPClient.Model
{
    public class TransactionModel
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        public string SenderIdString { get; set; }
                
        public string RecipientIdString { get; set; }

        public string? Details { get; set; }
    }
}
