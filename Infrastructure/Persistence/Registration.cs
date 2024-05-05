using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.UnitOfWorks;
using SouvenirApi.Application.Interface.Repositories;
using SouvenirApi.Application.Interface.UnitOfWorks;
using SouvenirApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class Registration
    {
        //IServiceCollection.AddPersistence() bu methoda parametre olarak eklediğimizde ve bunuda this başlığında verdiğimizde bu method,
        //bu servis içerisine eklenmiş olarak ve bu static oalrak çağırıp kullanabiliceğiz.
        public static void AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<SouvenirDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //IdentityDbContext kullanıldığında kullanıması gerek servis
            services.AddIdentityCore<User>(opt => 
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength= 2;
                opt.Password.RequireLowercase= false;
                opt.Password.RequireUppercase= false;
                opt.Password.RequireDigit = false;
                opt.SignIn.RequireConfirmedEmail= false;
            })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<SouvenirDbContext>();

        }
    }
}
