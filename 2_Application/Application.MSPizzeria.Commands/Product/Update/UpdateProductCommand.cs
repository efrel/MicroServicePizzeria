using MediatR;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.Product.Update;

public record UpdateProductCommand(UpdateProductDTO objParams) :
    IRequest<Response<ProductDTO>>;