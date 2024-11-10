namespace Domain.MSPizzeria.Entity.Models.v1;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string Notes { get; set; }

    // Relationships
    public virtual Customer Customer { get; set; }
    public virtual Address Address { get; set; }
    public virtual ICollection<OrderDetail> Details { get; set; }
}