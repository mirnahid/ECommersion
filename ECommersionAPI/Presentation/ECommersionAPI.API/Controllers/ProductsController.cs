using ECommersionAPI.Application.Abstractions.Storage;
using ECommersionAPI.Application.Features.Commands.Product.CreateProduct;
using ECommersionAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage;
using ECommersionAPI.Application.Features.Commands.Product.RemoveProduct;
using ECommersionAPI.Application.Features.Commands.Product.RemoveProductImage;
using ECommersionAPI.Application.Features.Commands.Product.UpdateProduct;
using ECommersionAPI.Application.Features.Queries.Product.GetAllProduct;
using ECommersionAPI.Application.Features.Queries.Product.GetByIdProduct;
using ECommersionAPI.Application.Features.Queries.Product.ProductImageFile;
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

        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
           GetAllProductQueryResponse response= await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse response= await _mediator.Send(getByIdProductQueryRequest);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest request)
        {
           CreateProductCommandResponse response= await _mediator.Send(request);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response= await _mediator.Send(updateProductCommandRequest);

            return Ok();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute]RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response= await _mediator.Send(removeProductCommandRequest);

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommand)
        {
            uploadProductImageCommand.Files = Request.Form.Files;
            await _mediator.Send(uploadProductImageCommand);
            return Ok();
        }

        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]RemoveProductImageCommandRequest removeProductImageCommandRequest,[FromQuery]string imageId)
        {
            removeProductImageCommandRequest.ImageId=imageId;
            RemoveProductImageCommandResponse response= await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
           List<GetProductImagesQueryReponse> reponse= await _mediator.Send(getProductImagesQueryRequest);

            return Ok(reponse);
        }
    }
}
