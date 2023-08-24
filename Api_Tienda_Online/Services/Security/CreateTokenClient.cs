using Api_Tienda_Online.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api_Tienda_Online.Services.Security
{
    public class CreateTokenClient:IToken
    {
        private IConfiguration _config;

        public CreateTokenClient(IConfiguration config)
        {
            _config = config;
        }

        public Task<string> CreateToken(int id)
        {
            var jwtConfig = _config.GetSection("Jwt").Get<JwtConfig>();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,jwtConfig.Subject),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                new Claim("id", id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key));

            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwtConfig.Issuer,
                    jwtConfig.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: credenciales
                ) ;

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
