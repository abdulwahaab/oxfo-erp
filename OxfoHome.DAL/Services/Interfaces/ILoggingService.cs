using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface ILoggingService
    {
        Task<PagedResult<ErrorLogDTO>> GetPagedListAsync(int pageIndex, int pageSize, Func<IQueryable<ErrorLog>, IOrderedQueryable<ErrorLog>>? orderBy = null, Expression<Func<ErrorLog, bool>>? filter = null);
        Task<ErrorLogDTO> GetByID(object id);
        Task<bool> Add(ErrorLogDTO dto);
    }
}
