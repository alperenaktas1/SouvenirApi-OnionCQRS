using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Context;
using SouvenirApi.Application.Interface.Repositories;
using SouvenirApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class,IEntityBase,new()
    {
        private readonly SouvenirDbContext _souvenirContext;
        public ReadRepository(SouvenirDbContext souvenirContext)
        {
            _souvenirContext = souvenirContext;
        }
        private DbSet<T> Table { get => _souvenirContext.Set<T>(); }


        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            //Tracking yapılan sorguların işlemlerin takip edilmesini sağlar.
            IQueryable<T> queryable = Table;
            if(!enableTracking) queryable = queryable.AsNoTracking();
            if(include is not null) queryable = include(queryable);
            if(predicate is not null) queryable = queryable.Where(predicate);
            if(orderBy is not null) 
               return await orderBy(queryable).ToListAsync();

            return await queryable.ToListAsync();
        }


        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            Table.AsNoTracking();
            if (predicate is not null) Table.Where(predicate);
            return await Table.CountAsync(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking=false)
        {
            if (!enableTracking) Table.AsNoTracking();
            return Table.Where(predicate);
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int currenPage = 1, int pageSize = 3)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);
            if (predicate is not null) queryable = queryable.Where(predicate);
            if (orderBy is not null)
                return await orderBy(queryable).Skip((currenPage -1)* pageSize).Take(pageSize).ToListAsync();

            return await queryable.Skip((currenPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table;
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include is not null) queryable = include(queryable);

            //queryable.Where(predicate);
            return await queryable.FirstOrDefaultAsync(predicate);
        }
    }
}
