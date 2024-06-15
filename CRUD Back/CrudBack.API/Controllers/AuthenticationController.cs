using CrudBack.Business.Usuarios;
using CrudBack.Models;
using CrudBack.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudBack.API.Controllers
{
    public class AuthenticationController : ControllerBase
    {
        private readonly IUsuariosServices _usuarioServices;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IUsuariosServices usuarioServices, IConfiguration configuration)
        {
            _usuarioServices = usuarioServices;
            _configuration = configuration;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Register([FromBody] Usuario model)
        {
            if (ModelState.IsValid)
            {
                var user = new Usuario { Username = model.Username, Email = model.Email, Password = model.Password };

                try
                {
                    var result = await _usuarioServices.Registrar(user);
                    return Ok(new { Result = "Registro Exitoso", User = result });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { Error = ex.Message });
                }
            }

            return BadRequest("Modelo ingresado inválido");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _usuarioServices.Authenticate(model.Username, model.Password);
                if (user == null)
                {
                    return Unauthorized();
                }

                var token = GenerateJwtToken(user);
                return Ok(new { Token = token });
            }

            return BadRequest("Invalid model state");
        }

        private string GenerateJwtToken(Usuario user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
