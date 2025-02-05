using Microsoft.Extensions.Configuration.UserSecrets;

namespace API.Dtos
{
    public class UserReadDto
    {
        public string? UserId { get; set; }
        public string? UserEmail { get; set; }
        public string? UserRole { get; set; }

        public UserReadDto(string? id, string? email, string? role)
        {
            UserId = id;
            UserEmail = email;
            UserRole = role;
        }
    }
}