﻿using Microsoft.AspNet.Identity.EntityFramework;

namespace StructuredMarket.Domain.Entities
{
    public class User
    {
        public User(string firstName, string lastName, string username, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            Phone = phone;
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Role> Roles { get; set; } = new List<Role>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
