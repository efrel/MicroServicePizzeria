using Application.MSPizzeria.DTO.ViewModel.v1;
using MediatR;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Order.Create;

public record CreateOrderCommand(CreateOrderDTO objParams) : IRequest<Response<OrderDTO>>;