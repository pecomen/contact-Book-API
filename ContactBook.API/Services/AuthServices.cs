﻿using ContactBook.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactBook.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthServices(IConfiguration config, SignInManager<AppUser> signInManager)
        {
            _config = config;
            _signInManager = signInManager;
        }
        public string GenerateJWT(AppUser user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler ();
             var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value);
              var signingCredentials = new SigningCredentials (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            var securityToken= new JwtSecurityToken(
                claims: claims,
               expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_config.GetSection("JWT:LifeSpan").Value)),
               signingCredentials: signingCredentials    );
              var token =  jwtSecurityTokenHandler.WriteToken(securityToken);
            return token;
        }

        public async Task<SignInResult> Login(AppUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password,false,false);
        }
    }
}
