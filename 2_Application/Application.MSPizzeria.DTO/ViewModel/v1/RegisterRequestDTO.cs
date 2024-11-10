namespace Application.MSPizzeria.DTO.ViewModel.v1;

public class RegisterRequestDTO
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string PhoneNumber { get; set; }
    public string ConfirmPhoneNumber { get; set; }
}