using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lumo.Api.DTOs
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

    }
}
