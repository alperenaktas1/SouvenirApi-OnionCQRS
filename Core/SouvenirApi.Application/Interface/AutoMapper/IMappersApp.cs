using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Interface.AutoMapper
{
    public interface IMappersApp
    {
        TDestination Map<TDestination,TSource>(TSource source,string? ignore = null);

        IList<TDestination> Map<TDestination, Tsource>(IList<Tsource> source, string? ignore = null);

        TDestination Map<TDestination>(object source, string? ignore = null);
        IList<TDestination> Map<TDestination>(IList<object> source, string? ignore = null);
    }
}
