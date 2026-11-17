using Lumo.Api.DTOs;
using Lumo.Api.Models;

namespace Lumo.Api.Interfaces
{
    public interface ITokenService
    {
        public LoginResponse GenerateToken(User user);
    }
}
