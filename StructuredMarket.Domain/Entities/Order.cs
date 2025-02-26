namespace StructuredMarket.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime DeliveryTime { get; set; } = DateTime.UtcNow;
        public string DeliveryAddress { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
