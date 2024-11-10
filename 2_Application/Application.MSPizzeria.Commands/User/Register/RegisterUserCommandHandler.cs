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

namespace Application.MSPizzeria.Commands.User.Register;

public class RegisterUserCommandHandler :
    IRequestHandler<RegisterUserCommand, Response<RegisterResponseDTO>>
{
    #region PROPERTIES
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserDomain _userDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<RegisterUserCommandHandler> _logger;
    private readonly RegisterRequestDTO_Validator _registerRequestValidator;
    //Token
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    #endregion

    #region CONSTRUCTOR

    public RegisterUserCommandHandler(
        SignInManager<ApplicationUser> signInManager
        , IUserDomain userDomain
        , IMapper mapper
        , IAppLogger<RegisterUserCommandHandler> logger
        , RegisterRequestDTO_Validator registerRequestDtoValidator
        , IJwtTokenGenerator jwtTokenGenerator
    )
    {
        _signInManager = signInManager;
        _userDomain = userDomain;
        _mapper = mapper;
        _logger = logger;
        _registerRequestValidator = registerRequestDtoValidator;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    #endregion
    
    public async Task<Response<RegisterResponseDTO>> Handle(
        RegisterUserCommand request
        , CancellationToken cancellationToken
    )
    {
        #region DECLARACIÓN DE VARIABLES
        var userInfo = request.objParams;
        var objResponse = new Response<RegisterResponseDTO>();
        #endregion

        try
        {
            #region VALIDACION DEL DTO
            var objValidacion = await _registerRequestValidator.ValidateAsync(userInfo);

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
            
            if (existingUser != null)
            {
                _logger.LogWarning("El email ya está registrado");
                
                objResponse.IsSuccess = false;
                objResponse.Message = "El email ya está registrado";

                return objResponse;
            }
            #endregion
            
            #region MAPEOS DE DTO A ENTIDADES
            var objAspNetUsers = _mapper.Map<ApplicationUser>(userInfo);
            #endregion
            
            #region REGISTRAR NUEVO USUARIO

            var objUserNew = await _userDomain.CreateAsync(objAspNetUsers, userInfo.Password);
            
            if (!objUserNew)
            {
                _logger.LogWarning("Sucedio un problema al registrar el usuario");
                
                objResponse.IsSuccess = false;
                objResponse.Message = "Sucedio un problema al registrar el usuario";
                
                return objResponse;
            }
            #endregion
            
            #region ASIGNAR ROL POR DEFAULT
            await _userDomain.AddToRoleAsync(objAspNetUsers, "user_App");
            #endregion
            
            #region GENERAR TOKEN
            var lstRoles = await _userDomain.GetRolesAsync(objAspNetUsers);

            // var objUserInfoMap = _mapper.Map<UserInfoModel>(userInfo);
            var objUserInfoMap = new UserInfoModel()
            {
                Email = userInfo.Email,
            };
            // crear JWT Token
            var objUserToken = _jwtTokenGenerator.GenerateToken(objUserInfoMap, lstRoles);
            #endregion
            
            #region MAPEAR REPUESTA

            var objUserAppMap = _mapper.Map<ApplicationUserDTO>(objAspNetUsers);
            objUserAppMap.Roles = lstRoles.ToList();
            #endregion
            
            #region EN CASO QUE SE PUDO LOGUEAR CORRECTAMENTE
            objResponse.IsSuccess = true;
            objResponse.Data = new RegisterResponseDTO()
            {
                Token = objUserToken.Token,
                Status = objUserToken.Status,
                Expiration = objUserToken.Expiration,
                User = objUserAppMap
            };
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
            _logger.LogError("RegisterUserCommandHandler", e);
            #endregion
        }

        return objResponse;
    }
}