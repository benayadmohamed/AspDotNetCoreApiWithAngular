using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private MyDbContext _myDbContext;

        public TokenController(IConfiguration config, MyDbContext myDbContext)
        {
            _config = config;
            _myDbContext = myDbContext;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] RequestAuth login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new {token = tokenString});
            }

            return response;
        }

        private string BuildToken(AppUser appUser)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Username),
//                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private AppUser Authenticate(RequestAuth login)
        {
            AppUser appUser = null;
            appUser = _myDbContext.Users.Where(user =>
                user.Username.Equals(login.Username)
                && user.Password.Equals(login.Password)).FirstOr(null);

            Console.WriteLine(appUser);

            return appUser;
        }
    }
}