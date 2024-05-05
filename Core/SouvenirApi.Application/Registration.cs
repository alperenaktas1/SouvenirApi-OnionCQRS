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

namespace SouvenirApi.Application
{
    public static class Registration
    {
        public static void AddAplication(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();

            service.AddTransient<ExceptionMiddleware>();

            service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

            service.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        }
    }
}
