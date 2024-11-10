using MediatR;
using AutoMapper;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Order.GetById;

public class GetOrderByIdQueryHandler  : IRequestHandler<GetOrderByIdQuery, Response<OrderDTO>>
{
    private readonly IOrderDomain _orderDomain;
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetOrderByIdQueryHandler> _logger;

    public GetOrderByIdQueryHandler(
        IOrderDomain orderDomain,
        IProductDomain productDomain,
        IMapper mapper,
        IAppLogger<GetOrderByIdQueryHandler> logger
    )
    {
        _orderDomain = orderDomain;
        _productDomain = productDomain;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response<OrderDTO>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var objResponse = new Response<OrderDTO>();
        
        try
        {
            var order = await _orderDomain.GetByIdAsync(request.Id);
            
            if (order == null)
            {
                #region REGISTRO DE LOG DE ERROR
                _logger.LogWarning($"Orden no encontrada: {request.Id}");
                #endregion
                    
                #region SI SUCEDIO UN PROBLEMA
                objResponse.IsSuccess = false;
                objResponse.Message = $"Orden no encontrada: {request.Id}";
                #endregion

                return objResponse;
            }

            var orderDto = _mapper.Map<OrderDTO>(order);
            
            #region EN CASO DE EXITO
            objResponse.IsSuccess = true;
            objResponse.Data = orderDto;
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