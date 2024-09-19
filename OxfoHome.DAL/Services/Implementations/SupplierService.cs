using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public SupplierService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<SupplierDTO>> GetAll()
        {
            IEnumerable<SupplierDTO> dtoList = _mapper.Map<IEnumerable<SupplierDTO>>(await _uow.Suppliers.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<SupplierDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<Supplier>, IOrderedQueryable<Supplier>>? orderBy = null,
        Expression<Func<Supplier, bool>>? filter = null)
        {
            return await _uow.Suppliers.GetPagedAsync<SupplierDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<SupplierDTO> GetByID(object id)
        {
            return _mapper.Map<SupplierDTO>(await _uow.Suppliers.GetByIdAsync(id));
        }

        public async Task<bool> Add(SupplierDTO dto)
        {
            Supplier supplier = await _uow.Suppliers.AddAndGetAsync(_mapper.Map<Supplier>(dto));
            await _uow.SaveAsync();

            //every supplier also can be a payee
            return await AddPayee(supplier);
        }

        public async Task<bool> Update(SupplierDTO dto)
        {
            await _uow.Suppliers.UpdateAsync(_mapper.Map<Supplier>(dto));
            await _uow.SaveAsync();

            //update supplier also can be a payee
            return await UpdatePayee(dto);
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.Suppliers.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }

        //add payee for the supplier
        public async Task<bool> AddPayee(Supplier dto)
        {
            PayingEntity payeeDTO = new PayingEntity()
            {
                EntityId = dto.Id,
                Name = dto.Name,
                Phone = dto.ContactPhone,
                Address = dto.Address,
                Type = "Supplier",
                Status = (int)Status.Active,
                CreatedOn = DateTime.Now
            };
            await _uow.PayingEntities.AddAsync(payeeDTO);
            return await _uow.SaveAsync() > 0;
        }

        //update payee for the supplier
        public async Task<bool> UpdatePayee(SupplierDTO dto)
        {
            var payee = await _uow.PayingEntities.Find(x => x.EntityId == dto.Id && x.Type.ToLower().Equals("supplier"));
            if (payee != null)
            {
                payee.Name = dto.Name;
                payee.Phone = dto.ContactPhone;
                payee.Address = dto.Address;
                await _uow.PayingEntities.UpdateAsync(payee);
                return await _uow.SaveAsync() > 0;
            }
            else
                return false;
        }
    }
}
