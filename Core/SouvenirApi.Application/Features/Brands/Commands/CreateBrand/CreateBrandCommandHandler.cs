using Bogus;
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

namespace SouvenirApi.Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommandHandler : BaseHandler , IRequestHandler<CreateBrandCommandRequest,Unit>
    {
        public CreateBrandCommandHandler(IMappersApp mappersApp, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mappersApp, unitOfWork, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            Faker faker = new ("tr");
            List<Brand> brands = new();

            for (int i = 0; i < 100000; i++)
            {
                brands.Add(new(faker.Commerce.Department(1)));
            }

            await unitOfWork.GetWriteRepository<Brand>().AddRangeAsync(brands);
            await unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
