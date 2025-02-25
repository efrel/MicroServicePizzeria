﻿namespace Domain.MSPizzeria.Entity.Models.v1;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Relationships
    public virtual ICollection<OrderDetail> OrderDetails { get; set; }
}