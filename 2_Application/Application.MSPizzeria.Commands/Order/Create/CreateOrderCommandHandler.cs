using MediatR;
using AutoMapper;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Entity.Models.v1;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Order.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<OrderDTO>>
{
    private readonly IOrderDomain _orderDomain;
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(
        IOrderDomain orderDomain,
        IProductDomain productDomain,
        IMapper mapper,
        IAppLogger<CreateOrderCommandHandler> logger
    )
    {
        _orderDomain = orderDomain;
        _productDomain = productDomain;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Response<OrderDTO>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var objParams = request.objParams;
        var objResponse = new Response<OrderDTO>();
        
        try
        {
            #region MAPEAR DTO
            var objOrderMap = _mapper.Map<Domain.MSPizzeria.Entity.Models.v1.Order>(objParams);
            #endregion

            foreach (var detail in objParams.Details)
            {
                var product = await _productDomain.GetByIdAsync(detail.ProductId);
                
                if (product == null)
                {
                    #region REGISTRO DE LOG DE ERROR
                    _logger.LogWarning($"El producto con el ID {detail.ProductId} no existe");
                    #endregion
                    
                    #region SI SUCEDIO UN PROBLEMA
                    objResponse.IsSuccess = false;
                    objResponse.Message = $"El producto con el ID {detail.ProductId} no existe";
                    #endregion

                    return objResponse;
                }
            }

            #region CREAR ORDEN
            var createdOrder = await _orderDomain.CreateAsync(objOrderMap);
            var orderDto = _mapper.Map<OrderDTO>(createdOrder);
            #endregion

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