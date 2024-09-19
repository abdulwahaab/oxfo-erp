using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IProductService
    {
        Task<IQueryable<ProductDTO>> GetAll();
        Task<PagedResult<ProductDTO>> GetPagedListAsync(int pageIndex, int pageSize, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, Expression<Func<Product, bool>>? filter = null);
        Task<ProductDTO> GetByID(object id);
        Task<bool> Add(ProductDTO dto);
        Task<bool> Update(ProductDTO dto);
        Task<bool> Delete(int id);
    }
}
