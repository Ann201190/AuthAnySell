using Auth.Business.Entities;
using Auth.Business.Models;
using Auth.Business.Service.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth.Business.Service
{
    public class JWTService: IJWTService
    {
        private readonly IOptions<AuthOptions> _authOptions;
        public JWTService(IOptions<AuthOptions> authOptions) 
        {
            _authOptions = authOptions;
        }

        public string CreateJWT(Account account)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),       
                new Claim("role", account.Role.ToString())
            };


            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }   
    }
}
