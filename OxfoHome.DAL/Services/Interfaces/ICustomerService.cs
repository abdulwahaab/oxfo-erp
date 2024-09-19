using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface ICustomerService
    {
        Task<IQueryable<CustomerDTO>> GetAll();
        Task<PagedResult<CustomerDTO>> GetPagedListAsync(int pageIndex, int pageSize, Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = null, Expression<Func<Customer, bool>>? filter = null);
        Task<CustomerDTO> GetByID(object id);
        Task<bool> Add(CustomerDTO dto);
        Task<bool> Update(CustomerDTO dto);
        Task<bool> Delete(int id);
    }
}
