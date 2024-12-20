namespace WebApp.Models.BankManagementModels
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }

        public DateTime Date {  get; set; }

        public decimal Amount { get; set; }

        public  string? Type { get; set; }

        public string? Description { get; set; }


        public Transaction(decimal amount, string type, string description)
        {
            TransactionId = Guid.NewGuid();
            Date = DateTime.UtcNow;
            Amount = amount;
            Type = type;
            Description = description;

        }


    }
}
