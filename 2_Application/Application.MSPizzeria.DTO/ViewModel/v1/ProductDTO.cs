namespace Application.MSPizzeria.DTO.ViewModel.v1;

public class ProductDTO
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
}

public class CreateProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
}

public class UpdateProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public string Category { get; set; }
    public string ImageUrl { get; set; }
}

public class GetAllProductDTO
{
    public bool? IsAvailable { get; set; }
    public string Category { get; set; }
}

public class GetProductByIdDTO
{
    public int Id { get; set; }
}