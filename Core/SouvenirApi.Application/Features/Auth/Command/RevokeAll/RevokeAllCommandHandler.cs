using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SouvenirApi.Application.Bases;
using SouvenirApi.Application.Interface.AutoMapper;
using SouvenirApi.Application.Interface.UnitOfWorks;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Auth.Command.RevokeAll
{
    public class RevokeAllCommandHandler : BaseHandler, IRequestHandler<RevokeAllCommandRequest, Unit>
    {
        private readonly UserManager<User> userManager;
        public RevokeAllCommandHandler(IMappersApp mappersApp, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<User> userManager) : base(mappersApp, unitOfWork, httpContextAccessor)
        {
            this.userManager = userManager;
        }

        public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
        {
            var users = await userManager.Users.ToListAsync(cancellationToken);

            foreach (User user in users)
            {
                user.RefreshToken = null;
                await userManager.UpdateAsync(user);
            }

            return Unit.Value;
        }
    }
}
