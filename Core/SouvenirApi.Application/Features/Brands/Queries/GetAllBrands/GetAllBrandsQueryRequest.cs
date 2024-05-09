using MediatR;
using SouvenirApi.Application.Interface.RedisCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Brands.Queries.GetAllBrands
{
    public class GetAllBrandsQueryRequest : IRequest<IList<GetAllBrandsQueryResponse>>, ICacHeableQuery
    {
        public string CacheKey => "GetAllBrands";

        public double CacheTime => 5;
    }
}
