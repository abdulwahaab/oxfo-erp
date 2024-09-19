using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface ISharingService
    {
        Task<IQueryable<SharingVendorDTO>> GetAll();
        Task<PagedResult<SharingVendorDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<SharingVendor>, IOrderedQueryable<SharingVendor>>? orderBy = null,
        Expression<Func<SharingVendor, bool>>? filter = null);
        Task<SharingVendorDTO> GetByID(object id);
        Task<bool> Add(SharingVendorDTO dto);
        Task<bool> Update(SharingVendorDTO dto);
        Task<bool> Delete(int id);
    }
}
