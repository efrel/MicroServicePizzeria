namespace Application.MSPizzeria.DTO.ViewModel.v1;

public class RegisterResponseDTO
{
    public string Status { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public ApplicationUserDTO User { get; set; }
}