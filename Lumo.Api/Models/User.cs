using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Lumo.Api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Role { get; set; } = "User";

    }
}
