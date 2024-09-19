using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IPaymentService
    {
        Task<IQueryable<PaymentDTO>> GetAll();
        Task<PagedResult<PaymentDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<PaymentDTO>, IOrderedQueryable<PaymentDTO>>? orderBy = null,
        Expression<Func<PaymentDTO, bool>>? filter = null);
        Task<PaymentDTO?> GetByID(object id);
        Task<bool> Add(PaymentDTO dto);
        Task<bool> Update(PaymentDTO dto);
        Task<bool> Delete(int id);

        //get payment stats
        Task<AccountBalance?> GetAccountData(int id);
    }
}
