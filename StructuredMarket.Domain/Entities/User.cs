using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StructuredMarket.Domain.Entities
{
    [Table("Users")]
    public class User: IEntity
    {
        public User(string firstName, string lastName, string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
        }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
