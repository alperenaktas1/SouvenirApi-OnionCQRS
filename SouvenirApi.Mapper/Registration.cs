using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SouvenirApi.Application.Interface.AutoMapper;
using SouvenirApi.Mapper.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection service)
        {
            service.AddSingleton<IMappersApp, AutoMapper.Mapper>();
        }
    }
}
