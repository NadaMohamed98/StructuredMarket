namespace StructuredMarket.Application.Features.Orders.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalCost { get; set; }
        public List<OrderDetailsModel> OrderDetails { get; set; }
        public Guid UserId { get; set; }
        public DateTime DeliveryTime { get; set; }
    }
}
