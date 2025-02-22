using System.Security;

namespace StructuredMarket.Domain.Entities
{
    public class Role : IEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
