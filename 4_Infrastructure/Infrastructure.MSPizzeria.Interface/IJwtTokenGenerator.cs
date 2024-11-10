using Application.MSPizzeria.DTO.ViewModel;

namespace Infrastructure.MSPizzeria.Interface;

public interface IJwtTokenGenerator
{
    UserTokenDTO GenerateToken(UserInfoDTO userInfo, IList<string> roles);
}