using ECommersionAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using P = ECommersionAPI.Domain.Entities;

namespace ECommersionAPI.Application.Features.Queries.Product.ProductImageFile
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryReponse>>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IConfiguration _configuration;

        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository,IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _configuration = configuration;
        }

        public async Task<List<GetProductImagesQueryReponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            P.Product? product = await _productReadRepository.Table.Include(x => x.ProductImageFiles)
                .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));

            return product?.ProductImageFiles.Select(x => new GetProductImagesQueryReponse
            {
                Path = $"{_configuration["BaseStorageUrl"]}/{x.Path}",
                FileName = x.FileName,
                Id = x.Id
            }).ToList();
        }
    }
}
