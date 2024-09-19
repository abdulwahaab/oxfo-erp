using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IDyeingService
    {
        Task<IQueryable<DyeingVendorDTO>> GetAll();
        Task<PagedResult<DyeingVendorDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<DyeingVendor>, IOrderedQueryable<DyeingVendor>>? orderBy = null,
        Expression<Func<DyeingVendor, bool>>? filter = null);
        Task<DyeingVendorDTO> GetByID(object id);
        Task<bool> Add(DyeingVendorDTO dto);
        Task<bool> Update(DyeingVendorDTO dto);
        Task<bool> Delete(int id);

        //method for dyeing prices
        Task<PagedResult<DyeingColorRateDTO>> GetVendorPriceList(int pageIndex, int pageSize, int vendorId);
        Task<DyeingColorRateDTO> GetPriceByID(object id);
        Task<bool> AddPrice(DyeingColorRateDTO dto);
        Task<bool> UpdatePrice(DyeingColorRateDTO dto);
        Task<bool> DeletePrice(int id);
    }
}
