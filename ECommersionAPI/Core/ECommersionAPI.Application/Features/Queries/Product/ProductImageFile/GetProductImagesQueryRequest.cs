using MediatR;

namespace ECommersionAPI.Application.Features.Queries.Product.ProductImageFile
{
    public class GetProductImagesQueryRequest:IRequest<List<GetProductImagesQueryReponse>>
    {
        public string Id { get; set; }
    }
}
