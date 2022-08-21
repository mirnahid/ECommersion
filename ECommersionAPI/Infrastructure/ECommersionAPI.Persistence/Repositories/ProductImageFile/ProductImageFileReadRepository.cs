using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;

namespace ECommersionAPI.Persistence.Repositories
{
    public class ProductImageFileReadRepository : ReadRepository<ProductImageFile>, IProductImageFileReadRepository
    {
        public ProductImageFileReadRepository(ECommersionAPIDbContext context) : base(context)
        {
        }
    }
}
