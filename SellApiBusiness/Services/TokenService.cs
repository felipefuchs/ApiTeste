using System;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SellApiBusiness.Model;

namespace SellApiBusiness.Services
{
    public class TokenService
    {

        public object GerarToken(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));


            var key = Encoding.ASCII.GetBytes(KeyMaster.Secret);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var claims = new Claim[]
            {
                new Claim("usuarioId", usuario.Id.ToString()),
                new Claim("usuarioEmail", usuario.Email)
            };

            var tokenConfig = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(tokenConfig);

            return new { 
                token = tokenString
            };
        }
    }
}
 