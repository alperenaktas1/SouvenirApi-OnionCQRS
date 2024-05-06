using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SouvenirApi.Application.Interface.Tokens;
using SouvenirApi.Infrastructure.Tokens;

namespace SouvenirApi.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<TokenSettings>(configuration.GetSection("JWT"));
            service.AddTransient<ITokenService, TokenService>();

            service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                    ValidateLifetime = false,
                    ValidIssuer = configuration["JWT:Secret"],
                    ValidAudience = configuration["JWT:Secret"],
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
