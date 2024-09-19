using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface ISupplierService
    {
        Task<IQueryable<SupplierDTO>> GetAll();
        Task<PagedResult<SupplierDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>? orderBy = null,
        Expression<Func<Supplier, bool>>? filter = null);
        Task<SupplierDTO> GetByID(object id);
        Task<bool> Add(SupplierDTO dto);
        Task<bool> Update(SupplierDTO dto);
        Task<bool> Delete(int id);
    }
}
