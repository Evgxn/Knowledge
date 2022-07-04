using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.ViewModel.Account;

namespace Services
{
    public interface IAccountService
    {
        string GenerateJSONWebToken(ClaimsIdentity identity);
        Task<ClaimsIdentity> GetIdentity(LoginViewModel vm);
    }
    public class AccountService : IAccountService
    {
        private IConfiguration _config;
        private readonly AppDbContext _db;

        public AccountService(IConfiguration config, AppDbContext db)
        {
            _config = config;
            _db = db;
        }

        public string GenerateJSONWebToken(ClaimsIdentity identity)
        {
            // query the database using EF here.

            var now = DateTime.UtcNow;

            //Generate JWT token
            var jwt = new JwtSecurityToken(
                _config["JWT:Issuer"],
                _config["JWT:Audience"],
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(int.Parse(_config["JWT:Lifetime"]))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["JWT:Key"])),
                    SecurityAlgorithms.HmacSha256));

            return
                new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<ClaimsIdentity> GetIdentity(LoginViewModel login)
        {
            var profile = await _db.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(c => c.Email == login.Email && c.Password == login.Password);

            if (profile == null)
                return null;

            var claims = new List<Claim>
            {
                new("Email", profile.Email),
                new("Password", profile.Password),
                new("Role",profile.Role.RoleName)
            };
            ClaimsIdentity claimsIdentity =
                new(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}
