using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;

namespace ECommersionAPI.Persistence.Repositories
{
    public class ProductWriterepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriterepository(ECommersionAPIDbContext context) : base(context)
        {
        }
    }
}
