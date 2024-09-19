using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace OxfoHome.DAL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<RawMaterialOrderDTO>> GetAll()
        {
            IEnumerable<RawMaterialOrderDTO> dtoList = _mapper.Map<IEnumerable<RawMaterialOrderDTO>>(await _uow.RawMaterialOrders.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<RawMaterialOrderDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<RawMaterialOrderDTO>, IOrderedQueryable<RawMaterialOrderDTO>>? orderBy = null,
        Status? orderStatus = null, Expression<Func<RawMaterialOrderDTO, bool>>? filter = null)
        {
            var query = _uow.RawMaterialOrders.Table
                        .Join(_uow.Suppliers.Table,
                        order => order.SupplierId,
                        supplier => supplier.Id,
                        (order, supplier) => new RawMaterialOrderDTO
                        {
                            Id = order.Id,
                            SupplierId = supplier.Id,
                            SupplierName = supplier.Name,
                            TotalAmount = order.TotalAmount,
                            CreatedOn = order.CreatedOn,
                            Status = order.Status
                        });

            if (orderStatus != null)
                query = query.Where(x => x.Status == (byte)orderStatus);

            if (orderBy != null)
                query = orderBy(query);

            var totalItems = query.Count();

            var itemList = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<RawMaterialOrderDTO>
            {
                Items = itemList,
                TotalCount = totalItems
            };
        }

        public async Task<RawMaterialOrderDTO> GetByID(object id)
        {
            return await _uow.RawMaterialOrders.Table
                       .Join(_uow.Suppliers.Table,
                       order => order.SupplierId,
                       supplier => supplier.Id,
                       (order, supplier) => new RawMaterialOrderDTO
                       {
                           Id = order.Id,
                           SupplierId = supplier.Id,
                           SupplierName = supplier.Name,
                           ExpectedDelivery = order.ExpectedDelivery,
                           TotalAmount = order.TotalAmount,
                           CreatedBy = order.CreatedBy,
                           CreatedOn = order.CreatedOn,
                           Status = order.Status
                       }).Where(x => x.Id == (int)id).FirstOrDefaultAsync();
        }

        public async Task<bool> CreateOrder(RawMaterialOrderDTO dto, List<RawMaterialOrderDetailDTO> orderItems)
        {
            RawMaterialOrder order = await _uow.RawMaterialOrders.AddAndGetAsync(_mapper.Map<RawMaterialOrder>(dto));
            await _uow.SaveAsync();
            int orderId = order.Id;
            if (orderId > 0)
            {
                foreach (var item in orderItems)
                {
                    item.RawMaterialOrderId = orderId;
                    item.CreatedOn = DateTime.Now;
                    item.Status = (int)Status.Created;
                }

                await _uow.RawMaterialOrderDetails.AddRangeAsync(_mapper.Map<List<RawMaterialOrderDetail>>(orderItems));
            }
            return (await _uow.SaveAsync() > 0 && order.Id > 0);
        }

        public async Task<bool> UpdateOrder(RawMaterialOrderDTO dto, List<RawMaterialOrderDetailDTO> orderItems)
        {
            //update main order record, first fetch the existing entity and map the properties
            RawMaterialOrder orderEntity = await _uow.RawMaterialOrders.GetByIdAsync(dto.Id);
            orderEntity.SupplierId = dto.SupplierId;
            orderEntity.ExpectedDelivery = dto.ExpectedDelivery;
            orderEntity.TotalAmount = dto.TotalAmount;
            //update main order record
            await _uow.RawMaterialOrders.UpdateAsync(_mapper.Map<RawMaterialOrder>(orderEntity));

            //delete the previous entries
            await _uow.RawMaterialOrderDetails.DeleteRangeAsync(x => x.RawMaterialOrderId == dto.Id);

            foreach (var item in orderItems)
            {
                item.RawMaterialOrderId = dto.Id;
                item.CreatedOn = DateTime.Now;
                item.Status = (int)Status.Created;
            }

            //now add new entries
            if (orderItems.Count > 0)
                await _uow.RawMaterialOrderDetails.AddRangeAsync(_mapper.Map<List<RawMaterialOrderDetail>>(orderItems));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            //delete the main order record
            await _uow.RawMaterialOrders.DeleteAsync(id);

            //delete the order items from order details table
            await _uow.RawMaterialOrderDetails.DeleteRangeAsync(x => x.RawMaterialOrderId == id);

            return await _uow.SaveAsync() > 0;
        }

        public async Task<List<RawMaterialOrderDetailDTO>> GetOrderItemsByOrderID(object id)
        {
            var entities = await _uow.RawMaterialOrderDetails.Table.Where(x => x.RawMaterialOrderId == (int)id)
             .Select(x => new RawMaterialOrderDetailDTO
             {
                 Id = x.Id,
                 RawMaterialId = x.RawMaterialId,
                 PriceUnit = x.PriceUnit,
                 PricePerItem = x.PricePerItem,
                 QuantityOrdered = x.QuantityOrdered,
             }).ToListAsync();

            return _mapper.Map<List<RawMaterialOrderDetailDTO>>(entities);
        }

        public async Task<List<RawMaterialOrderDetailDTO>> GetOrderItemDetailByOrderID(object id)
        {
            var entities = await _uow.RawMaterialOrderDetails.Table
            .Join(_uow.RawMaterials.Table,
            order => order.RawMaterialId,
            rawMaterial => rawMaterial.Id,
            (order, rawMaterial) => new RawMaterialOrderDetailDTO
            {
                Id = order.Id,
                RawMaterialOrderId = order.RawMaterialOrderId,
                RawMaterialId = order.RawMaterialId,
                ItemName = rawMaterial.ItemName,
                ItemNumber = rawMaterial.ItemNumber,
                PriceUnit = order.PriceUnit,
                PricePerItem = order.PricePerItem,
                QuantityOrdered = order.QuantityOrdered,
            })
            .Where(x => x.RawMaterialOrderId == (int)id)
            .ToListAsync();

            return _mapper.Map<List<RawMaterialOrderDetailDTO>>(entities);
        }
    }
}