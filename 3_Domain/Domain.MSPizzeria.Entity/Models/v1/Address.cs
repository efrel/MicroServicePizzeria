namespace Domain.MSPizzeria.Entity.Models.v1;

public class Address
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Street { get; set; }
    public string ExteriorNumber { get; set; }
    public string InteriorNumber { get; set; }
    public string Neighborhood { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Reference { get; set; }
    public bool IsMain { get; set; }

    // Relationships
    public virtual Customer Customer { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}