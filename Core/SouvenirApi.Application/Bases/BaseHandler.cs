using Microsoft.AspNetCore.Http;
using SouvenirApi.Application.Interface.AutoMapper;
using SouvenirApi.Application.Interface.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Bases
{
    public class BaseHandler
    {
        public readonly IMappersApp mappersApp;
        public readonly IUnitOfWork unitOfWork;
        public readonly IHttpContextAccessor httpContextAccessor;
        public readonly string userId;

        public BaseHandler(IMappersApp mappersApp,IUnitOfWork unitOfWork,IHttpContextAccessor httpContextAccessor)
        {
            this.mappersApp = mappersApp;
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
