﻿namespace StructuredMarket.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Merchant { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
