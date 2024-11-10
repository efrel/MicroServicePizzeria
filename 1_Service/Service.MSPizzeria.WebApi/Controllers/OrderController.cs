using Application.MSPizzeria.Commands.Order.Create;
using Application.MSPizzeria.Commands.Order.Update;
using Application.MSPizzeria.DTO.ViewModel.v1;
using Application.MSPizzeria.Queries.Order.GetAll;
using Application.MSPizzeria.Queries.Order.GetById;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Service.MSPizzeria.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// get all orders
    /// </summary>
    /// <param name="status"></param>
    /// <param name="customerId"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAll")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(List<OrderDTO>), 200)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string status,
        [FromQuery] int? customerId)
    {
        var query = new GetAllOrdersQuery(status, customerId);
        var response = await _mediator.Send(query);
        
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// get order by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(OrderDTO), 200)]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetOrderByIdQuery(id);
        var response = await _mediator.Send(query);
        
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// create order
    /// </summary>
    /// <param name="objParams"></param>
    /// <returns></returns>
    [HttpPost]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(OrderDTO), 200)]
    public async Task<IActionResult> Create([FromBody] CreateOrderDTO objParams)
    {
        var command = new CreateOrderCommand(objParams);
        
        var response = await _mediator.Send(command);
        
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// update order
    /// </summary>
    /// <param name="id"></param>
    /// <param name="objParams"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(OrderDTO), 200)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateOrderDTO objParams)
    {
        var command = new UpdateOrderCommand(objParams);
        
        if (id != objParams.Id)
            return BadRequest();

        var response = await _mediator.Send(command);
            
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }
}