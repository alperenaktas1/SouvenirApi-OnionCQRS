using MediatR;
using SouvenirApi.Application.Interface.AutoMapper;
using SouvenirApi.Application.Interface.UnitOfWorks;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest,Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMappersApp _mappersApp;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMappersApp mappersApp)
        {
            _unitOfWork = unitOfWork;
            _mappersApp = mappersApp;
        }
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.GetReadRepository<Product>().GetAsync(x=>x.Id==request.Id && !x.IsDeleted);
            var map = _mappersApp.Map<Product, UpdateProductCommandRequest>(request);
            var productCategories = await _unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(x => x.ProductId == product.Id);

            await _unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);

            foreach (var categoryId in request.CategoryIds)
            {
                await _unitOfWork.GetWriteRepository<ProductCategory>()
                    .AddAsync(new() { CategoryId = categoryId, ProductId = product.Id });
            }

            await _unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
