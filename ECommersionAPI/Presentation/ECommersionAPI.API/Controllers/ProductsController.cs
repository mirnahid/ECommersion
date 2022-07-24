using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Application.RequestParameters;
using ECommersionAPI.Application.ViewModels.Products;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace ECommersionAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ECommersionAPIDbContext _context;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ECommersionAPIDbContext context)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Pagination pagination)
        {
            var totalCount = _productReadRepository.GetAll().Count();
            var products = _productReadRepository.GetAll(false)
                                                .Skip(pagination.Page * pagination.Size)
                                                .Take(pagination.Size)
                                                .Select(p => new
                                                {
                                                    p.Id,
                                                    p.Name,
                                                    p.Stock,
                                                    p.Price,
                                                    p.UpdatedDate
                                                });

            return Ok(new { totalCount, products });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok();

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Products model)
        {
            await _productWriteRepository.AddAsync(new Product { Name = model.Name, Price = model.Price, Stock = model.Stock });
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_Update_Product model)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok();
        }
    }
}
