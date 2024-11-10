﻿namespace Domain.MSPizzeria.Entity.Models.v1;

public class OrderDetail
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public string Notes { get; set; }

    // Relationships
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}