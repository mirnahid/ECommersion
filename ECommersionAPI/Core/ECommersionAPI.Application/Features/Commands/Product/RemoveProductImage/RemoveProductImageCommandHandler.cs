using ECommersionAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = ECommersionAPI.Domain.Entities;

namespace ECommersionAPI.Application.Features.Commands.Product.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(x => x.ProductImageFiles).FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));
            P.ProductImageFile productImageFile=product.ProductImageFiles.FirstOrDefault(x=>x.Id==Guid.Parse(request.ImageId));

            if (productImageFile!=null)
            {
                product?.ProductImageFiles.Remove(productImageFile);
            }
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
