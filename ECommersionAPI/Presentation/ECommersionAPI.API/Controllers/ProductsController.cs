using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Application.RequestParameters;
using ECommersionAPI.Application.ViewModels.Products;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace ECommersionAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly ECommersionAPIDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, ECommersionAPIDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
            var model = await _productReadRepository.GetByIdAsync(id, false);
            _productWriteRepository.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            Random random = new();

            foreach (IFormFile file in Request.Form.Files)
            {
                string fullPath=Path.Combine(uploadPath, $"{random.Next()}{Path.GetExtension(file.FileName)}");

                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write,FileShare.None,1024*1024,false);

                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();

            }

            return Ok();
        }
    }
}
