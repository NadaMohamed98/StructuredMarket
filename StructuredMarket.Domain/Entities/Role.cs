using Microsoft.AspNet.Identity.EntityFramework;

namespace StructuredMarket.Domain.Entities
{
    public class Role
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
