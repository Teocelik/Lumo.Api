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
    public class UserService : IUserService
    {
        private readonly LumoDbContext _dbContext;
        private readonly ITokenService _tokenService;
        public UserService(LumoDbContext dbContext, ITokenService jwtTokenService)
        {
            _dbContext = dbContext;
            _tokenService = jwtTokenService;
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
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == loginRequest.Email);

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginRequest.Password);

            if(passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var response = _tokenService.GenerateToken(user);

            return response;
        }
    }
}