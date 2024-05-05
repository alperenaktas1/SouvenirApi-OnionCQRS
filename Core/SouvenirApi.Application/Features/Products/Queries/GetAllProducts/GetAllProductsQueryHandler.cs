using MediatR;
using Microsoft.EntityFrameworkCore;
using SouvenirApi.Application.DTOs;
using SouvenirApi.Application.Interface.AutoMapper;
using SouvenirApi.Application.Interface.UnitOfWorks;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappersApp _mapApp;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork,IMappersApp mapApp)
        {
            _unitOfWork = unitOfWork;
            _mapApp = mapApp;
        }
        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork.GetReadRepository<Product>().GetAllAsync(include:x=>x.Include(b=>b.Brand));

            var brand = _mapApp.Map<BrandDto, Brand>(new Brand());

            var map = _mapApp.Map<GetAllProductsQueryResponse, Product>(products);
            foreach (var item in map)
            {
                item.Price -= (item.Price * item.Discount / 100);
            }
            return map;
        }
    }
}
