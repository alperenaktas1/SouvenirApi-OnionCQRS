using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Infrastructure.RedisCache
{
    public class RedisCacheSettings
    {
        //Redise bağlanmamızı sağlaycak con. string için
        public string ConnectionString { get; set; }
        //Redis db'sinin adı için
        public string InstanceName { get; set; }
    }
}
