using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ProcessWorker.Bl.Interfaces;
using ProcessWorker.Web.Models;

namespace ProcessWorker.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly ILogger<AuthorizationController> _logger;

        private readonly IConfiguration _config;

        private readonly IAuthorization _authorization;

        public AuthorizationController(ILogger<AuthorizationController> logger, IAuthorization authorization, IConfiguration config)
        {
            _logger = logger;
            _authorization = authorization;
            _config = config;
        }

        [HttpPost]
        public IActionResult GetToken([FromBody] LoginModel model)
        {
            int? userId = _authorization.Authorize(model.Username, model.Password);
            if (userId.HasValue)
            {
                string token = GenerateJsonWebToken(model.Username, userId.Value);
                Response.Cookies.Append(Constants.TOKEN_COOKIES_KEY, 
                    token, 
                    new CookieOptions { HttpOnly = true, SameSite = SameSiteMode.Strict });
                return this.Ok();
            }

            return BadRequest();
        }
        
        private string GenerateJsonWebToken(string username, int userId)    
        {    
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));    
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);    
    
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],    
                _config["Jwt:Issuer"],    
                new []
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                },    
                expires: DateTime.Now.AddMinutes(120),    
                signingCredentials: credentials);    
    
            return new JwtSecurityTokenHandler().WriteToken(token);    
        } 
    }
}