using API.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService
    {
        private readonly TokenManagement _tokenManagement;
        private readonly UserService _userService;

        public TokenService(IOptions<TokenManagement> tokenManagement, UserService userService)
        {
            _tokenManagement = tokenManagement.Value;
            _userService = userService;
        }

        public bool IsAuthenticated(UserViewModel user, out string token)
        {
            token = string.Empty;

            if (_userService.ValidateUser(user) is false) return false;

            var claim = new[]
            {
                new Claim("name", user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenManagement.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtToken = new JwtSecurityToken(
                _tokenManagement.Issuer,
                _tokenManagement.Audience,
                claim,
                expires: DateTime.Now.AddMinutes(_tokenManagement.AccessExpiration),
                signingCredentials: credentials
            );
            token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return true;
        }
    }
}
