using FluentValidation;

using Application.MSPizzeria.DTO.ViewModel.v1;

namespace Application.MSPizzeria.Validator;

public class UpdateProductDTO_Validator : AbstractValidator<UpdateProductDTO>
{
    public UpdateProductDTO_Validator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id invalido");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es requerido")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("La categoria es requerido")
            .MaximumLength(50).WithMessage("La categoria no puede exceder 50 caracteres");
    }
}