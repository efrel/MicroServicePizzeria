using FluentValidation;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;

namespace Application.MSPizzeria.Validator;

public class RegisterRequestDTO_Validator : AbstractValidator<RegisterRequestDTO>
{
    public RegisterRequestDTO_Validator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido")
                .MinimumLength(3).WithMessage("El nombre de usuario debe tener al menos 3 caracteres")
                .MaximumLength(50).WithMessage("El nombre de usuario no puede exceder 50 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es requerido")
                .EmailAddress().WithMessage("El formato del email no es válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
                .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula")
                .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula")
                .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número")
                .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("La confirmación de contraseña es requerida")
                .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9][0-9]{7,14}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("El formato del número de teléfono no es válido");
            
            RuleFor(x => x.ConfirmPhoneNumber)
                .NotEmpty().WithMessage("La confirmación del numero de teléfono es requerido")
                .Equal(x => x.PhoneNumber).WithMessage("Los números de teléfono no coinciden");
        }
}