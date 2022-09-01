﻿using MediatR;

namespace ECommersionAPI.Application.Features.Queries.Product.GetByIdProduct
{
    public class GetByIdProductQueryRequest:IRequest<GetByIdProductQueryResponse>
    {
        public string Id { get; set; }
    }
}
