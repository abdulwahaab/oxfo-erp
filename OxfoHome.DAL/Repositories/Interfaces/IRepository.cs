using AutoMapper;
using System.Linq.Expressions;

namespace OxfoHome.DAL
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        // CRUD Operations
        Task<T> GetByIdAsync(object id);
        Task<T?> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void UpdateRange(List<T> entities);
        Task<T> AddAndGetAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(Expression<Func<T, bool>> predicate);

        Task<PagedResult<TResult>> GetPagedAsync<TResult>(int pageIndex, int pageSize, IMapper mapper, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Expression<Func<T, bool>>? filter = null);
    }
}