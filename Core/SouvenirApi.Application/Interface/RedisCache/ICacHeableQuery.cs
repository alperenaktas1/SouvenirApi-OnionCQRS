using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Interface.RedisCache
{
    public interface ICacHeableQuery
    {
        string CacheKey { get; }
        double CacheTime { get; }
    }
}
