namespace WebApp.Models
{
    public class Product
    {

        public Guid Id { get; set; } = Guid.NewGuid();
        public required string ProductName { get; set; }

        public decimal Price { get; set; }


    }
}
