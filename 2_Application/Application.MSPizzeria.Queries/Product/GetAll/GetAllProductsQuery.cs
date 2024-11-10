using MediatR;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.Product.GetAll;

public record GetAllProductsQuery(GetAllProductDTO objParams) : IRequest<Response<List<ProductDTO>>>;