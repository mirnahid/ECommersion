using ECommersionAPI.Domain.Entities;

namespace ECommersionAPI.Application.Abstractions
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}
