using FluentValidation;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;
using Application.MSPizzeria.Validator.ExpRegulares;

namespace Application.MSPizzeria.Validator;

public class UserInfoDTO_Validator : AbstractValidator<UserInfoDTO>
{
    #region DOCUMENTACION
    //https://docs.fluentvalidation.net/en/latest/
    #endregion

    #region CONSTRUCTOR
    public UserInfoDTO_Validator()
    {
        RuleFor(o => o.Email)
            .NotNull().NotEmpty().Must(EsUnCorreo).WithMessage("El correo electronico, no puede ir vacio y debe tener el formato correcto tucorreo@dominio.com");
        RuleFor(o => o.Password)
            .NotNull().NotEmpty().WithMessage("El campo contraseña no puede ir vacio");
    }
    #endregion

    #region VALIDACIONES EXTRAS
    private bool EsUnCorreo(string sExpresion)
    {
        return ValidateDataType.ContieneUnaDireccionDeCorreo(sExpresion);
    }
    #endregion
}