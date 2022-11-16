using AnimalShelter.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.Win32;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32;
using AnimalShelter.Data.Controllers;
using User = AnimalShelter.Data.Controllers.User;

namespace JWTAuthentication.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
        private readonly DataContext _context;

        public LoginController(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel register)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == register.Username);
            if (user != null)
            {
                return Conflict(new { message = "Istnieje juz uzytkownik o takiej nazwie" });
            }
            _context.Users.Add(register);
            _context.SaveChanges();
            return Ok("User created");
        }

        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            if (login == null)
            {
                return null;
            }
            var user = _context.Users.FirstOrDefault(u => u.Username == login.Username && u.password == login.password);
            if (user == null)
            {
                return null;

            }
            return user;
        }
    }
}