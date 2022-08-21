using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;

namespace ECommersionAPI.Persistence.Repositories
{
    public class ProductImageFileWriteRepository : WriteRepository<ProductImageFile>,IProductImageFileWriteRepository
    {
        public ProductImageFileWriteRepository(ECommersionAPIDbContext context) : base(context)
        {
        }
    }
}
