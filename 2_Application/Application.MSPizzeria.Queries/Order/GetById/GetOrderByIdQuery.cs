using Application.MSPizzeria.DTO.ViewModel.v1;
using MediatR;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Order.GetById;

public record GetOrderByIdQuery(int Id) : IRequest<Response<OrderDTO>>;