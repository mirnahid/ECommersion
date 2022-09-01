using MediatR;

namespace ECommersionAPI.Application.Features.Commands.Product.RemoveProductImage
{
    public class RemoveProductImageCommandRequest:IRequest<RemoveProductImageCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
