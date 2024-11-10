using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Application.MSPizzeria.DTO.ViewModel.v1;
using Domain.MSPizzeria.Entity.Models.v1;
using Infrastructure.MSPizzeria.Interface;
using Microsoft.Extensions.Options;

namespace Infrastructure.MSPizzeria.Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }
    
    public UserTokenDTO GenerateToken(UserInfoModel userInfo, IList<string> roles)
    {
        var sSecret = _jwtSettings.Secret;
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(sSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var expiration = _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);
        
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: expiration,
            claims: claims,
            signingCredentials: credentials
        );
        
        return new UserTokenDTO()
        {
            Status = "Ok",
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}