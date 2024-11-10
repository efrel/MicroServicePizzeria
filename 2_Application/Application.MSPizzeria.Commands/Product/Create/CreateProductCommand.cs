using MediatR;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Product.Create;

public record CreateProductCommand(CreateProductDTO objParams) :
    IRequest<Response<ProductDTO>>;