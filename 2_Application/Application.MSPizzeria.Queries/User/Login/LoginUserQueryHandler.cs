using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;
using Application.MSPizzeria.Validator;
using Domain.MSPizzeria.Entity.Models.v1;
using Domain.MSPizzeria.Interface;
using Infrastructure.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.User.Login;

public class LoginUserQueryHandler :
    IRequestHandler<LoginUserQuery, Response<UserTokenDTO>>
{
    #region PROPERTIES
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserDomain _userDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<LoginUserQueryHandler> _logger;
    private readonly UserInfoDTO_Validator _userInfoDTOValidator;
    //Token
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    #endregion

    #region CONSTRUCTOR

    public LoginUserQueryHandler(
        SignInManager<ApplicationUser> signInManager
        , IUserDomain userDomain
        , IMapper mapper
        , IAppLogger<LoginUserQueryHandler> logger
        , UserInfoDTO_Validator userInfoDTOValidator
        , IJwtTokenGenerator jwtTokenGenerator
    )
    {
        _signInManager = signInManager;
        _userDomain = userDomain;
        _mapper = mapper;
        _logger = logger;
        _userInfoDTOValidator = userInfoDTOValidator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    #endregion
    
    public async Task<Response<UserTokenDTO>> Handle(
        LoginUserQuery request
        , CancellationToken cancellationToken
    )
    {
        #region DECLARACIÓN DE VARIABLES
        var userInfo = request.UserInfo;
        var objResponse = new Response<UserTokenDTO>();
        #endregion

        try
        {
            #region VALIDACION DEL DTO
            var objValidacion = await _userInfoDTOValidator.ValidateAsync(userInfo);

            if (!objValidacion.IsValid)
            {
                objResponse.IsSuccess = false;
                objResponse.Message = "Alguno de los datos ingresados no son validos.";
                objResponse.Error = objValidacion.Errors;
                return objResponse;
            }
            #endregion
            
            #region VERIFICACION DE USARIOS EXISTE
            var existingUser = await _userDomain.GetByEmailAsync(userInfo.Email);
            
            if (existingUser == null)
            {
                _logger.LogWarning("Credenciales inorrectas.");
                
                objResponse.IsSuccess = false;
                objResponse.Message = "Credenciales inorrectas.";

                return objResponse;
            }
            #endregion
            
            #region VALIDAR CREDENCIALES
            var result = await _signInManager.PasswordSignInAsync(
                userName: existingUser.UserName, 
                password: userInfo.Password, 
                isPersistent: false, 
                lockoutOnFailure: false);
            #endregion
            
            #region VALIDAD SI ES UNA RESPUESTA CORRECTA
            if (!result.Succeeded)
            {
                objResponse.IsSuccess = false;
                objResponse.Message = "Intento de inicio de sesión no válido.";

                return objResponse;
            }
            #endregion
            
            #region OBTIENE DATOS DE USUARIO
            var objUser = await _userDomain.GetByEmailAsync(userInfo.Email);
            var lstRoles = await _userDomain.GetRolesAsync(objUser);
            #endregion

            #region MAPEAR OBJETO

            var objUserInfoDTO = _mapper.Map<UserInfoModel>(userInfo);

            #endregion
            
            #region GENERAR TOKEN
            // crear JWT Token
            var objUserToken = _jwtTokenGenerator.GenerateToken(objUserInfoDTO, lstRoles);
            #endregion
            
            #region EN CASO QUE SE PUDO LOGUEAR CORRECTAMENTE
            objResponse.IsSuccess = true;
            objResponse.Data = objUserToken;
            objResponse.Message = "Login Exitoso";
            #endregion

            #region REGISTRO DE LOG INFORMATIVO
            _logger.LogInformation("Login Exitoso");
            #endregion
            
        }
        catch (Exception e)
        {
            #region SI SUCEDIO UN PROBLEMA
            objResponse.IsSuccess = false;
            objResponse.Message = "Sucedio algo imprevisto al procesar su solicitud.";
            #endregion

            #region REGISTRO DE LOG DE ERROR
            _logger.LogError("LoginUserQueryHandler", e);
            #endregion
        }

        return objResponse;
    }
}