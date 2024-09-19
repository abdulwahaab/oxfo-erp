using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IRawMaterialService
    {
        Task<IQueryable<RawMaterialDTO>> GetAll();

        Task<PagedResult<RawMaterialDTO>> GetPagedListAsync(int pageIndex, int pageSize,
            Func<IQueryable<RawMaterial>, IOrderedQueryable<RawMaterial>>? orderBy = null,
            Expression<Func<RawMaterial, bool>>? filter = null);

        Task<RawMaterialDTO> GetByID(object id);
        Task<bool> Add(RawMaterialDTO dto);
        Task<bool> Update(RawMaterialDTO dto);
        Task<bool> Delete(int id);
    }
}
