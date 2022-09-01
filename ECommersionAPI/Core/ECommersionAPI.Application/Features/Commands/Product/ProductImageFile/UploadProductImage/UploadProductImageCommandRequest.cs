using MediatR;
using Microsoft.AspNetCore.Http;

namespace ECommersionAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandRequest:IRequest<UploadProductImageCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection Files { get; set; }
    }
}
