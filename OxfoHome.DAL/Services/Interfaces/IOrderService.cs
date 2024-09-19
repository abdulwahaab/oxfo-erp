using OxfoHome.DAL.DTOs;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IOrderService
    {
        Task<IQueryable<RawMaterialOrderDTO>> GetAll();
        Task<PagedResult<RawMaterialOrderDTO>> GetPagedListAsync(int pageIndex, int pageSize,
                Func<IQueryable<RawMaterialOrderDTO>, IOrderedQueryable<RawMaterialOrderDTO>>? orderBy = null,
                Status? orderStatus = null, Expression<Func<RawMaterialOrderDTO, bool>>? filter = null);
        Task<RawMaterialOrderDTO> GetByID(object id);
        Task<bool> CreateOrder(RawMaterialOrderDTO dto, List<RawMaterialOrderDetailDTO> orderItems);
        Task<bool> UpdateOrder(RawMaterialOrderDTO dto, List<RawMaterialOrderDetailDTO> orderItems);
        Task<bool> Delete(int id);

        //order details methods
        Task<List<RawMaterialOrderDetailDTO>> GetOrderItemsByOrderID(object id);

        Task<List<RawMaterialOrderDetailDTO>> GetOrderItemDetailByOrderID(object id);
    }
}
