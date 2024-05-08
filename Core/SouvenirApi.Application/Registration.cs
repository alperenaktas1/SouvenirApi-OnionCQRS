using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SouvenirApi.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using MediatR;
using SouvenirApi.Application.Beheviors;
using SouvenirApi.Application.Features.Products.Rules;
using Microsoft.AspNetCore.Http;
using SouvenirApi.Application.Features.Auth.Command.Register;
using SouvenirApi.Application.Bases;

namespace SouvenirApi.Application
{
    public static class Registration
    {
        public static void AddAplication(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();

            service.AddTransient<ExceptionMiddleware>();
            //service.AddTransient<ProductRules>();

            service.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));
            
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            
            service.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        }

        private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services,Assembly assembly,Type type)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();

            foreach (var item in types)
            {
                services.AddTransient(item);
            }
            return services;
        }
    }
}
