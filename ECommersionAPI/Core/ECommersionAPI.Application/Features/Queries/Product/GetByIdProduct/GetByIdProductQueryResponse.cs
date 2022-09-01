using ECommersionAPI.Domain.Entities;
using PI = ECommersionAPI.Domain.Entities;

namespace ECommersionAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryResponse
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<PI.ProductImageFile> ProductImageFiles { get; set; }
    }
}
