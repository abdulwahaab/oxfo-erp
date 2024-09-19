using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IPayeeService
    {
        Task<IQueryable<PayeeDTO>> GetAll();
        Task<PagedResult<PayeeDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<PayingEntity>, IOrderedQueryable<PayingEntity>>? orderBy = null,
        Expression<Func<PayingEntity, bool>>? filter = null);
        Task<PayeeDTO> GetByID(object id);
        Task<PayeeDTO> Find(Expression<Func<PayingEntity, bool>>? predicate = null);
        Task<bool> Add(PayeeDTO dto);
        Task<bool> Update(PayeeDTO dto);
        Task<bool> Delete(int id);
    }
}
