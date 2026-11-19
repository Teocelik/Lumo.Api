using Lumo.Api.DTOs;

namespace Lumo.Api.Interfaces
{
    public interface IUserService
    {
        public Task<UserResponse> RegisterUserAsync(CreateUserRequest userRequest);

        public Task<LoginResponse?> LoginUserAsync(LoginRequest loginRequest);
    }
}
