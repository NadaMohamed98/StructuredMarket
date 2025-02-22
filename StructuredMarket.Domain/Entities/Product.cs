namespace StructuredMarket.Domain.Entities
{
    public class Product: IEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
