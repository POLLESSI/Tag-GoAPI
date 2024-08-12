using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag_Go.BLL.Interfaces;
using Tag_Go.DAL.Entities;

namespace Tag_Go.BLL.Services
{
    public class UserService : IUserService
    {
    #nullable disable
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<NUser> _passwordHasher;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IPasswordHasher<NUser> passwordHasher, IConfiguration configuration)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            //var user = await _context.NUsers.SingleOrDefaultAsync(x => x.Pseudo == dto.Pseudo);
            //if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Pwd) == PasswordVerificationResult.Failed)
            //{ 
            //    throw new UnauthorizedAccessException("Invalid nickname or password");
            //}
            //var token = GenerateJwtToken(user);
            //return token;
            return null;
        }

        public async Task<NUser> RegisterAsync(UserRegisterDto dto)
        {
            var user = new NUser
            {
                Pseudo = dto.pseudo,
                Email = dto.Email

            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Pwd);
            //_context.NUsers.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
