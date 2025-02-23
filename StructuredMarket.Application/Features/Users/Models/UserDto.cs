using System.Text.Json.Serialization;

namespace StructuredMarket.Application.Features.Users.Models
{
    public class UserDto
    {
        public Guid Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Username { get; init; }
        public string? Email { get; init; }
        public string? Phone { get; init; }

        [JsonIgnore]
        public string? Password { get; init; }
    }
}
