using MediatR;
using Microsoft.AspNetCore.Http;
using SouvenirApi.Application.Bases;
using SouvenirApi.Application.Interface.AutoMapper;
using SouvenirApi.Application.Interface.UnitOfWorks;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Brands.Queries.GetAllBrands
{
    public class GetAllBrandsQueryHandler : BaseHandler, IRequestHandler<GetAllBrandsQueryRequest, IList<GetAllBrandsQueryResponse>>
    {
        public GetAllBrandsQueryHandler(IMappersApp mappersApp, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mappersApp, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<IList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await unitOfWork.GetReadRepository<Brand>().GetAllAsync();
            return mappersApp.Map<GetAllBrandsQueryResponse,Brand>(brands);
        }
    }
}
