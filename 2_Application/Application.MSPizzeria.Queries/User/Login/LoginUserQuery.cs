using MediatR;

// MIS REFERENCIAS
using Application.MSPizzeria.DTO.ViewModel.v1;
using Transversal.MSPizzeria.Common;

namespace Application.MSPizzeria.Queries.User.Login;

public record LoginUserQuery(UserInfoDTO UserInfo) :
    IRequest<Response<UserTokenDTO>>;