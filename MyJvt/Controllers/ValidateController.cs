﻿using Microsoft.AspNetCore.Authorization;
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
        IConfiguration config;
        public ValidateController(IConfiguration config)
        {
            this.config = config;
        }
        [AllowAnonymous]
        [HttpPost, Route("GetToken")]
        public ActionResult GetToken(UserModel model)
        {
            try
            {


                /*
                 проверка в БД 
                 */

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[] {
                      new Claim("myRole", "admin"),
                      new Claim("dateBirth", "2000-01-01")
                };

                var token = new JwtSecurityToken(config["Jwt:Issuer"],
                    config["Jwt:Issuer"],
                    claims,
                    //null,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: credentials);

                var sToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new ReturnStatus
                {
                    status = StatusEnum.OK,
                    result = sToken
                });
            }
            catch (Exception err)
            {
                return Ok(new ReturnStatus
                {
                    status = StatusEnum.ERROR,
                    result = "error",
                    error = err.Message
                });
            }
        }

        [HttpGet, Authorize, Route("GetTest/{name}")]
        public ActionResult GetTest(string name)
        {
            return Ok("Hello " + name + " " + User.FindFirst("myRole")?.Value);
        }




    }
}
