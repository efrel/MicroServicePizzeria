namespace Domain.MSPizzeria.Entity.Models.v1;

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime RegisterDate { get; set; }
    public bool IsActive { get; set; }

    // Relationships
    public virtual ICollection<Address> Addresses { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
}