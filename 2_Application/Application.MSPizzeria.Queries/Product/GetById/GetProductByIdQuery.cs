using Application.MSPizzeria.DTO.ViewModel.v1;
using MediatR;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Product.GetById;

public record GetProductByIdQuery(GetProductByIdDTO objParams) : IRequest<Response<ProductDTO>>;