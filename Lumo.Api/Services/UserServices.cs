using Lumo.Api.Data;
using Lumo.Api.DTOs;
using Lumo.Api.Interfaces;
using Lumo.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Lumo.Api.Services
{
    public class UserServices : IUserService
    {
        private readonly LumoDbContext _dbContext;
        public UserServices(LumoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserResponse?> RegisterUserAsync(CreateUserRequest userRequest)
        {
            var existingEMail = await _dbContext.Users.AnyAsync(x => x.
            Email == userRequest.Email);

            if (existingEMail)
            {
                return null;
            }

            var user = new User
            {
                Email = userRequest.Email,
                CreatedAt = DateTime.UtcNow
            };

            var passWordHasher = new PasswordHasher<User>();

            user.PasswordHash = passWordHasher.HashPassword(user, userRequest.Password);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            
            return new UserResponse { Id = user.Id, Email = user.Email, CreatedAt = user.CreatedAt };
        }

        public async Task<LoginResponse?> LoginUserAsync(LoginRequest loginRequest)
        {
            var existingEmail = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginRequest.Email);

            if (existingEmail == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<User>();

            var verificationResult = passwordHasher.VerifyHashedPassword(existingEmail, existingEmail.PasswordHash, loginRequest.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var token = new JsonWebTokenHandler();

            var tokenString = token.CreateToken(new SecurityTokenDescriptor
            {

            });
        }
    }
}
