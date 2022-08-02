using Hackathon_CV_Portal.Data.Pagination;
using System.Linq.Expressions;

namespace Hackathon_CV_Portal.Data.Abstractions
{
    public interface IBaseRepository<T> where T : class
    {
        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> predicate = null);
        Task<IEnumerable<T>> GetAllAsyncWithIP(params Expression<Func<T, object>>[] includeProperties);
        Task<DomainPagedResult<T>> GetAllAsyncByPage(int page, Expression<Func<T, object>>[] includeProperties, Expression<Func<T, bool>>? expression = null, int resultsPerPage = 10);
        Task<T> GetAsync(Expression<Func<T, object>>[] includeProperties = null, Expression<Func<T, bool>> predicate = null);
        Task<T> GetForUpdateAsync(object key);
        Task<int> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(params object[] key);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
