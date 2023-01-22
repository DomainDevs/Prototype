using API.Models;
using Infrastructure.Auth.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers.Identity
{


    public class UsersController : Controller
    {

        public IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public dynamic IniciarSesion([FromBody] Object optData)
        {

            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());           

            string user = data.txtuser.ToString();
            string password = data.txtpassword.ToString();

            Usuario? usuario = Usuario.DB().FirstOrDefault(x => x.StrUsuario == user && x.Password == password);
            if (usuario == null)
            {
                return new
                {
                    success = false,
                    message = "Credenciales incorrectas",
                    result = ""
                };
            }

            var jwt = _configuration.GetSection($"SecuritySettings:{nameof(JwtSettings)}").Get<JwtSettings>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", usuario.IdUsuario),
                new Claim("Usuario", usuario.StrUsuario)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken
                (
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(3),
                    signingCredentials: singIn
                );


            //convertir parametros en clase
            return new
            {
                success = true,
                message = "exito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
