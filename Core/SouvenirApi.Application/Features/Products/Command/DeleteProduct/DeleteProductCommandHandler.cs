using MediatR;
using Microsoft.AspNetCore.Http;
using SouvenirApi.Application.Bases;
using SouvenirApi.Application.Interface.UnitOfWorks;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Products.Command.DeleteProduct
{
    public class DeleteProductCommandHandler : BaseHandler, IRequestHandler<DeleteProductCommandRequest,Unit>
    {

        public DeleteProductCommandHandler(Interface.AutoMapper.IMappersApp mappersApp, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor):base(mappersApp, unitOfWork, httpContextAccessor)
        {

        }
        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product =  await unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id && !x.IsDeleted);
            product.IsDeleted = true;

            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
