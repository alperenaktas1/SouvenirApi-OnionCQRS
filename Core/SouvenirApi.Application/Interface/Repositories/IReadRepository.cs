using Microsoft.EntityFrameworkCore.Query;
using SouvenirApi.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SouvenirApi.Application.Interface.Repositories
{
    // T parametresi alacak, class olacak , IEntityBase'den türeyecek ve newlenebilir olacak
    
    //Include mantığı çoka çok ilişkili tablolarda sorgu yapmak için ekledik.
    //GetAllAsync(x => x.include(a=>a.Brand).thenInclude) şeklinde iç içe sorgularla ilerleyebiliyoruz.
    
    //OrderBy en yeni olanlar gibi sıralama yapmak için kullanabiliriz.
    public interface IReadRepository<T> where T : class,IEntityBase, new()
    {
        //aradaki soru işaretleri boş geçebilmek için
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>,IIncludableQueryable<T,object>>? include = null,
            Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy= null,
            bool enableTracking = false);

        //Buradaki currentPage ve pageSize bizegelen ilk 3 veriyi getiriyor şeklinde ayarlıyor.
        Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false,int currenPage=1,int pageSize=3);

        //enableTracking sadece tekli veriler için gereklidir.
        Task<T> GetAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = false);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking= false);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
