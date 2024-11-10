using MediatR;
using AutoMapper;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Entity.Models.v1;
using Domain.MSPizzeria.Interface;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Order.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Response<OrderDTO>>
{
    private readonly IOrderDomain _orderDomain;
    private readonly IProductDomain _productDomain;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(
        IOrderDomain orderDomain,
        IProductDomain productDomain,
        IMapper mapper,
        IAppLogger<UpdateOrderCommandHandler> logger
    )
    {
        _orderDomain = orderDomain;
        _productDomain = productDomain;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Response<OrderDTO>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var objParams = request.objParams;
        var objResponse = new Response<OrderDTO>();
        
        try
        {
            #region VALIDA SI LA ORDEN EXISTE
            var existingOrder = await _orderDomain.GetByIdAsync(objParams.Id);
            
            if (existingOrder == null)
            {
                #region REGISTRO DE LOG DE ERROR
                _logger.LogWarning($"Orden no encontrada: {objParams.Id}");
                #endregion
                    
                #region SI SUCEDIO UN PROBLEMA
                objResponse.IsSuccess = false;
                objResponse.Message = $"Orden no encontrada: {objParams.Id}";
                #endregion

                return objResponse;
            }
            #endregion
            
            #region ENCABEZADO
            existingOrder.OrderStatus = objParams.OrderStatus;
            existingOrder.PaymentMethod = objParams.PaymentMethod;
            existingOrder.Notes = objParams.Notes;
            existingOrder.Details.Clear();
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

                existingOrder.Details.Add(new OrderDetail
                {
                    Id = detail.Id ?? 0,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    UnitPrice = product.Price,
                    Notes = detail.Notes,
                    Subtotal = product.Price * detail.Quantity
                });
            }

            #region UPDATE ORDEN
            var updatedOrder = await _orderDomain.UpdateAsync(existingOrder);
            var orderDto = _mapper.Map<OrderDTO>(updatedOrder);
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