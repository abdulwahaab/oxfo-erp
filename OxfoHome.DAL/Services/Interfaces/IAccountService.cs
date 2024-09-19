using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IAccountService
    {
        Task<IQueryable<AccountDTO>> GetAll();
        Task<PagedResult<AccountDTO>> GetPagedListAsync(int pageIndex, int pageSize, Func<IQueryable<Account>, IOrderedQueryable<Account>>? orderBy = null, Expression<Func<Account, bool>>? filter = null);
        Task<AccountDTO> GetByID(object id);
        Task<bool> Add(AccountDTO dto);
        Task<bool> Update(AccountDTO dto);
        Task<bool> Delete(int id);
    }
}
