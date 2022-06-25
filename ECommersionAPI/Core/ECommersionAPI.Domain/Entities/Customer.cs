using ECommersionAPI.Domain.Entities.Common;

namespace ECommersionAPI.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
