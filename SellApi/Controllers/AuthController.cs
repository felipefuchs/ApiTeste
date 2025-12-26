using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SellApiBusiness.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SellApiBusiness;
using SellApiBusiness.Services;

namespace SellApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Exemplo de usuário fixo para demonstração
        private readonly string demoUsername = "felipe_fuchs_f@yahoo.com.br";
        private readonly string demoPassword = "password";

        [HttpPost]
        public IActionResult Login([FromBody] Usuario request)
        {
            SellApiBusiness.Business.UsuarioBusiness usuarioBusiness = new SellApiBusiness.Business.UsuarioBusiness();
            usuarioBusiness.AdicionarUsuario(request);


            if (request.Email == demoUsername && request.Senha == demoPassword)
            {
                SellApiBusiness.Services.TokenService tokenService = new SellApiBusiness.Services.TokenService();
                var token = tokenService.GerarToken(request);
               // var token = GenerateJwtToken(request.Username);
                return Ok(new { token });
            }
            return BadRequest("Usuario ou senha inválidos.");
        }


    //     [HttpPost("login")]
    //     public IActionResult Login([FromBody] LoginRequest request)
    //     {
    //         if (request.Username == demoUsername && request.Password == demoPassword)
    //         {
    //             var token = GenerateJwtToken(request.Username);
    //             return Ok(new { token });
    //         }
    //         return Unauthorized();
    //     }

    //     private string GenerateJwtToken(string username)
    //     {
    //         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sua-chave-secreta-aqui-1234567890"));
    //         var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //         var claims = new[]
    //         {
    //             new Claim(ClaimTypes.Name, username)
    //         };

    //         var token = new JwtSecurityToken(
    //             issuer: "SellApi",
    //             audience: "SellApi",
    //             claims: claims,
    //             expires: DateTime.Now.AddHours(1),
    //             signingCredentials: creds);

    //         return new JwtSecurityTokenHandler().WriteToken(token);
    //     }
    // }
    }   
}