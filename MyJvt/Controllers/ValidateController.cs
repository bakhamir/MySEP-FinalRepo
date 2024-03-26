using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Encodings;
using MyJvt.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyJvt.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ValidateController : ControllerBase
    {
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IConfiguration _configuration;

            public AuthController(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            [HttpPost("login")]
            public IActionResult Login(UserModel model)
            { 
                if (model.login == "exampleUser" && model.password == "examplePassword")
                { 
                    var claims = new[]
                    {
                    new Claim(ClaimTypes.Name,model.login),
                    new Claim(ClaimTypes.Role, "user")  
                };

                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])),
                        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])), SecurityAlgorithms.HmacSha256));

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    });
                }
                else
                { 
                    return Unauthorized();
                }
            }
            [HttpGet("test")]
            [Authorize]
            public IActionResult Test()
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims; 
                    return Ok(claims);
                }

                return BadRequest("Failed to retrieve user claims");
            }
        }

    }
}
