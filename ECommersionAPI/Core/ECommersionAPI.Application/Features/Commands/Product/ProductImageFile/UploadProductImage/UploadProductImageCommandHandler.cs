using ECommersionAPI.Application.Abstractions.Storage;
using ECommersionAPI.Application.Repositories;
using MediatR;
using P = ECommersionAPI.Domain.Entities;

namespace ECommersionAPI.Application.Features.Commands.Product.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IStorageService _storageService;

        public UploadProductImageCommandHandler(IProductReadRepository productReadRepository,IProductImageFileWriteRepository productImageFileWriteRepository,IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }

        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {

            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images",request.Files);

            P.Product product = await _productReadRepository.GetByIdAsync(request.Id);

            await _productImageFileWriteRepository.AddRangeAsync(result.Select(x => new P.ProductImageFile
            {
                FileName = x.fileName,
                Path = x.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<P.Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
