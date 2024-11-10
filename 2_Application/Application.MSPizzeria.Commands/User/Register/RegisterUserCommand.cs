using MediatR;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Commands.User.Register;

public record RegisterUserCommand(RegisterRequestDTO objParams) :
    IRequest<Response<RegisterResponseDTO>>;