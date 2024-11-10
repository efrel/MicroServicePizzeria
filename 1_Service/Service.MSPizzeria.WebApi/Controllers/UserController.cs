using Application.MSPizzeria.Commands.User.Register;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;
using Application.MSPizzeria.Queries.User.Login;

namespace Service.MSPizzeria.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
// [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UserController : ControllerBase
{
    #region PROPIEDADES
    //mediatR
    private readonly ISender _mediator;
    #endregion

    #region CONSTRUCTOR DE CONTROLADOR
    public UserController(ISender mediator)
    {
        _mediator = mediator;
    }
    #endregion

    #region ENDPOINTS
    
    /// <summary>
    /// Login
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    [HttpPost(Name = "Login")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(UserTokenDTO), 200)]
    public async Task<IActionResult> Login([FromBody] UserInfoDTO userInfo)
    {
        #region USANDO MEDIATR
        var query = new LoginUserQuery(userInfo);
        var response = await _mediator.Send(query);
            
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
        #endregion
    }
    
    /// <summary>
    /// Registrar nuevo usuario
    /// </summary>
    /// <param name="registerRequest"></param>
    /// <returns></returns>
    [HttpPost(Name = "Register")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(RegisterResponseDTO), 200)]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
    {
        #region USANDO MEDIATR
        var command = new RegisterUserCommand(registerRequest);
        var response = await _mediator.Send(command);
            
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
        #endregion
    }

    #endregion
}