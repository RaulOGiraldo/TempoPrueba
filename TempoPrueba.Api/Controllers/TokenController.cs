using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TempoPrueba.Core.Entities;
using TempoPrueba.Core.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPasswordService _passwordService;

        public TokenController(IConfiguration configuration, IPasswordService passwordService)
        {
            _configuration = configuration;
            _passwordService = passwordService;
        }

        /// <summary>
        /// Genera el token mediante un usuario y un password
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Authentication(UserLogin login)
        {
            //if it is a valid user
            var validation = await IsValidUser(login);
            if (validation)
            {
                var token = GenerateToken();
                return Ok(new { token });
            }

            return NotFound();
        }

        private async Task<bool> IsValidUser(UserLogin login)
        {
            var password = _passwordService.Hash(_configuration["UserPrueba:Password"].ToString().Trim());
            var user = _configuration["UserPrueba:User"].ToString().Trim();

            var isValid = _passwordService.Check(password, login.Password);
            if (isValid)
            {
                isValid = user == login.User.Trim() ? true : false; 
            }
            return (isValid);
        }

        private string GenerateToken()
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var name = _configuration["UserPrueba:NameUser"];
            var user = _configuration["UserPrueba:User"];
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("User", user)
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(10)
            );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}