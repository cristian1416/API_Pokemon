using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pokemon.Domain.Entities;
using Pokemon.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Persistence.Repository
{
    public class Authorize : IAuthorize
    {
        private IConfiguration _config;
        public Authorize(IConfiguration Configuration)
        {
            _config = Configuration;
        }

        public async Task<string> TokenJWT()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);
            return await Task.Run(() => new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task<bool> ValidateUser(AuthLogin authLogin)
        {
            if (authLogin.usuario == "Pokemon" && authLogin.password == "Pokemon2024*")
            {
                
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
