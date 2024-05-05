using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SouvenirApi.Infrastructure.Tokens;

namespace SouvenirApi.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<TokenSettings>(configuration.GetSection("JWT"));
        }
    }
}
