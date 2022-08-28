﻿using ECommersionAPI.Application.Abstractions.Storage;
using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Application.RequestParameters;
using ECommersionAPI.Application.Services;
using ECommersionAPI.Application.ViewModels.Products;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IStorageService _storageService;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductWriteRepository productWriteRepository,
                                IProductReadRepository productReadRepository,
                                ECommersionAPIDbContext context,
                                IWebHostEnvironment webHostEnvironment,
                                IStorageService storageService,
                                IProductImageFileWriteRepository productImageFileWriteRepository,
                                IConfiguration configuration)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _configuration = configuration;
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
        public async Task<IActionResult> Upload(string id)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(x => new ProductImageFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
            var product = await _productReadRepository.Table.Include(x => x.ProductImageFiles)
                  .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(x => new
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{x.Path}",
                x.FileName,
                x.Id
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(string id, string imageId)
        {
            var product = await _productReadRepository.Table.Include(x => x.ProductImageFiles)
                 .FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            var productImageFile = product.ProductImageFiles.FirstOrDefault(x => x.Id == Guid.Parse(imageId));
            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();

            return Ok();

        }
    }
}
