using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital.API.Model;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _dbcontext;
        public AccountController(IConfiguration configuration, ApplicationDbContext dbcontext)
        {
            _configuration = configuration;
            _dbcontext = dbcontext;
        }
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginModel login)
        {
            var currentUser = _dbcontext.Users.FirstOrDefault(x => x.Username == login.Username);
            if (currentUser == null)
            {
                return NotFound("InValid Username or Password");
            }
            var token = GenerateToken(currentUser);
            if (token == null)
            {
                return NotFound("Invalid credentials");
            }
            return Ok(token);
        }
        [NonAction]
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512); var myClaims = new List<Claim>
                {
                         new Claim(ClaimTypes.Name,user.Username),
                         new Claim(ClaimTypes.Email,user.EmailId),
                         new Claim(ClaimTypes.GivenName,user.FullName),
                         new Claim(ClaimTypes.Role,user.Role ),
                 }; 
                 var token = new JwtSecurityToken(issuer: _configuration["JWT:issuer"],
                 audience: _configuration["JWT:audience"],
                 claims: myClaims,
                 expires: DateTime.Now.AddDays(1),
                 signingCredentials: credentials); return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("GetName"), Authorize]
        public IActionResult GetName()
        {
            var FullName = User.FindFirstValue(ClaimTypes.GivenName);
            return Ok(FullName);
        }
    }
}