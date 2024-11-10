namespace Application.MSPizzeria.DTO.ViewModel.v1;

public class UserTokenDTO
{
    public string Status { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}