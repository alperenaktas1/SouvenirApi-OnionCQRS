using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using SouvenirApi.Application.Interface.Repositories;
using SouvenirApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase,new()
    {
        private readonly SouvenirDbContext _souvenirContext;
        public WriteRepository(SouvenirDbContext souvenirContext)
        {
            _souvenirContext = souvenirContext;
        }
        private DbSet<T> Table { get => _souvenirContext.Set<T>(); }



        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
        }

        public async Task HardDeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.Run(() => Table.Update(entity));
            return entity;
        }
    }
}
