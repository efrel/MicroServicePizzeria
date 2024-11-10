using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Entity.Models.v1;

namespace Infrastructure.MSPizzeria.Interface;

public interface IJwtTokenGenerator
{
    UserTokenDTO GenerateToken(UserInfoModel userInfo, IList<string> roles);
}