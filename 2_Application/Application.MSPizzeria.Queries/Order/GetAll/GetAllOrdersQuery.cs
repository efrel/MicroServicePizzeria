using Application.MSPizzeria.DTO.ViewModel.v1;
using MediatR;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Order.GetAll;

public record GetAllOrdersQuery(string Status, int? CustomerId) : IRequest<Response<List<OrderDTO>>>;