using Application.MSPizzeria.DTO.ViewModel.v1;
using MediatR;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Order.Update;

public record UpdateOrderCommand(UpdateOrderDTO objParams) : IRequest<Response<OrderDTO>>;