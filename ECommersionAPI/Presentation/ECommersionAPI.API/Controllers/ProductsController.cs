using ECommersionAPI.Application.Abstractions.Storage;
using ECommersionAPI.Application.Features.Commands.CreateProduct;
using ECommersionAPI.Application.Features.Queries.GetAllProduct;
using ECommersionAPI.Application.Repositories;
using ECommersionAPI.Application.RequestParameters;
using ECommersionAPI.Application.ViewModels.Products;
using ECommersionAPI.Domain.Entities;
using ECommersionAPI.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        private readonly IMediator _mediator;

        public ProductsController(IProductWriteRepository productWriteRepository,
                                IProductReadRepository productReadRepository,
                                ECommersionAPIDbContext context,
                                IWebHostEnvironment webHostEnvironment,
                                IStorageService storageService,
                                IProductImageFileWriteRepository productImageFileWriteRepository,
                                IMediator mediator,
                                IConfiguration configuration)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _mediator = mediator;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
           GetAllProductQueryResponse response= await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) => Ok();

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
           CreateProductCommandResponse response= await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
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
