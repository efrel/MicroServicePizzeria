namespace Application.MSPizzeria.DTO.ViewModel.v1;

public class OrderDTO
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int AddressId { get; set; }
    public string AddressDetail { get; set; }
    public DateTime OrderDate { get; set; }
    public string OrderStatus { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string Notes { get; set; }
    public List<OrderDetailDTO> Details { get; set; }
}

public class OrderDetailDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public string Notes { get; set; }
}

public class CreateOrderDTO
{
    public int CustomerId { get; set; }
    public int AddressId { get; set; }
    public string PaymentMethod { get; set; }
    public string Notes { get; set; }
    public List<CreateOrderDetailDTO> Details { get; set; }
}

public class CreateOrderDetailDTO
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Notes { get; set; }
}

public class UpdateOrderDTO
{
    public int Id { get; set; }
    public string OrderStatus { get; set; }
    public string PaymentMethod { get; set; }
    public string Notes { get; set; }
    public List<UpdateOrderDetailDTO> Details { get; set; }
}

public class UpdateOrderDetailDTO
{
    public int? Id { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string Notes { get; set; }
}