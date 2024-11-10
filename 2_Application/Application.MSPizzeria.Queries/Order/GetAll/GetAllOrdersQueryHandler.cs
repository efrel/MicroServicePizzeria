using MediatR;
using AutoMapper;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Order.GetAll;

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Response<List<OrderDTO>>>
{
    private readonly IOrderDomain _orderDomain;
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetAllOrdersQueryHandler> _logger;

    public GetAllOrdersQueryHandler(
        IOrderDomain orderDomain,
        IProductDomain productDomain,
        IMapper mapper,
        IAppLogger<GetAllOrdersQueryHandler> logger
    )
    {
        _orderDomain = orderDomain;
        _productDomain = productDomain;
        _mapper = mapper;
        _logger = logger;
    }


    public async Task<Response<List<OrderDTO>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        
        var objResponse = new Response<List<OrderDTO>>();
        
        try
        {
            IEnumerable<Domain.MSPizzeria.Entity.Models.v1.Order> orders;

            if (!string.IsNullOrEmpty(request.Status))
            {
                orders = await _orderDomain.GetByStatusAsync(request.Status);
            }
            else if (request.CustomerId.HasValue)
            {
                orders = await _orderDomain.GetByCustomerIdAsync(request.CustomerId.Value);
            }
            else
            {
                orders = await _orderDomain.GetAllAsync();
            }

            var ordersDto = _mapper.Map<List<OrderDTO>>(orders.ToList());
            
            #region EN CASO DE EXITO
            objResponse.IsSuccess = true;
            objResponse.Data = ordersDto;
            objResponse.Message = "Consulta Exitosa";
            #endregion

            #region REGISTRO DE LOG INFORMATIVO
            _logger.LogInformation("Consulta Exitosa");
            #endregion
        }
        catch (Exception e)
        {
            #region SI SUCEDIO UN PROBLEMA
            objResponse.IsSuccess = false;
            objResponse.Message = "Sucedio algun imprevisto al procesar su solicitud.";
            #endregion

            #region REGISTRO DE LOG DE ERROR
            _logger.LogError("CreateOrderCommandHandler", e);
            #endregion
        }

        return objResponse;
    }
}