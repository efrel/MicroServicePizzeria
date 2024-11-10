using Application.MSPizzeria.Commands.Product.Create;
using Application.MSPizzeria.Commands.Product.Update;
using Application.MSPizzeria.DTO.ViewModel.v1;
using Application.MSPizzeria.Queries.Product.GetAll;
using Application.MSPizzeria.Queries.Product.GetById;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Transversal.MSPizzeria.Common;

namespace Service.MSPizzeria.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Get all product
    /// </summary>
    /// <param name="objParams"></param>
    /// <returns></returns>
    [HttpGet(Name = "GetAllProducts")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(List<ProductDTO>), 200)]
    public async Task<IActionResult> GetAll([FromQuery] GetAllProductDTO objParams)
    {
        var query = new GetAllProductsQuery(objParams);
        var response = await _mediator.Send(query);
        
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(ProductDTO), 200)]
    public async Task<IActionResult> GetById(int id)
    {
        var objParams = new GetProductByIdDTO()
        {
            Id = id
        };
        
        var query = new GetProductByIdQuery(objParams);
        var response = await _mediator.Send(query);
            
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// Create new product
    /// </summary>
    /// <param name="objParams"></param>
    /// <returns></returns>
    [HttpPost(Name = "Create")]
    [Authorize(Roles = "Admin")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(ProductDTO), 200)]
    public async Task<IActionResult> Create([FromBody] CreateProductDTO objParams)
    {
        var command = new CreateProductCommand(objParams);
        
        var response = await _mediator.Send(command);
        
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

    /// <summary>
    /// update product
    /// </summary>
    /// <param name="id"></param>
    /// <param name="objParams"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    // [Authorize(Roles = "Admin")]
    [EnableCors("SitiosPermitidos")]
    [ProducesResponseType(400)]
    [ProducesResponseType(typeof(ProductDTO), 200)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDTO objParams)
    {
        var command = new UpdateProductCommand(objParams);
        
        if (id != objParams.Id)
            return BadRequest();

        var response = await _mediator.Send(command);
            
        if(response.IsSuccess)
            return Ok(response);

        return BadRequest(response);
    }

}