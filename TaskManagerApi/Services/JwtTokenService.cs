using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Entities;
using TaskManager.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.DTOs;

namespace TaskManager.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly SymmetricSecurityKey _key;

        public JwtTokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokenkey"]));
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub , user.Email),
                new Claim(JwtRegisteredClaimNames.Name , user.Email),
                new Claim("userId" , user.Id.ToString()),
                new Claim("Manager", user.IsManager ? "true" : "false")
            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

