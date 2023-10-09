﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SuperHeroAPI.Services
{
    public class TokenService
    {
        private readonly string issuer;
        private readonly string audience;
        private readonly JwtSecurityTokenHandler handler;
        private readonly SigningCredentials creds;

        public TokenService(IConfiguration config)
        {
            this.issuer = config["JwtSettings:Issuer"]!;
            this.audience = config["JwtSettings:Audience"]!;
            this.handler = new JwtSecurityTokenHandler();

            var secret = config["JwtSettings:Secret"]!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var algorithm = SecurityAlgorithms.HmacSha512;
            this.creds = new SigningCredentials(key, algorithm);
        }

        public string GenerateToken(int userId)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            };

            var now = DateTime.UtcNow;
            var expires = now.AddHours(2);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                now,
                expires,
                creds
            );

            return handler.WriteToken(token);
        }
    }
}
