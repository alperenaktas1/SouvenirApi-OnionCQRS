using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SouvenirApi.Application.Bases;
using SouvenirApi.Application.Features.Auth.Rules;
using SouvenirApi.Application.Interface.AutoMapper;
using SouvenirApi.Application.Interface.UnitOfWorks;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Features.Auth.Command.Revoke
{
    public class RevokeCommandHandler : BaseHandler, IRequestHandler<RevokeCommandRequest,Unit>
    {
        private readonly UserManager<User> userManager;
        private readonly AuthRules authRules;
        public RevokeCommandHandler(IMappersApp mappersApp,UserManager<User> userManager,AuthRules authRules ,IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mappersApp, unitOfWork, httpContextAccessor)
        {
            this.userManager = userManager;
            this.authRules = authRules;
        }

        

        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            User user = await userManager.FindByEmailAsync(request.Email);
            await authRules.EmailAddressShouldBeValid(user);

            user.RefreshToken = null;
            await userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}
