﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application
{
    public static class Registration
    {
        public static void AddAplication(this IServiceCollection service)
        {
            var assembly = Assembly.GetExecutingAssembly();

            service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
        }
    }
}
