using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class DyeingService : IDyeingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public DyeingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<DyeingVendorDTO>> GetAll()
        {
            IEnumerable<DyeingVendorDTO> dtoList = _mapper.Map<IEnumerable<DyeingVendorDTO>>(await _uow.DyeingVendors.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<DyeingVendorDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<DyeingVendor>, IOrderedQueryable<DyeingVendor>>? orderBy = null,
        Expression<Func<DyeingVendor, bool>>? filter = null)
        {
            return await _uow.DyeingVendors.GetPagedAsync<DyeingVendorDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<DyeingVendorDTO> GetByID(object id)
        {
            return _mapper.Map<DyeingVendorDTO>(await _uow.DyeingVendors.GetByIdAsync(id));
        }

        public async Task<bool> Add(DyeingVendorDTO dto)
        {
            DyeingVendor vendor = await _uow.DyeingVendors.AddAndGetAsync(_mapper.Map<DyeingVendor>(dto));
            await _uow.SaveAsync();
            return await AddPayee(vendor);
        }

        public async Task<bool> Update(DyeingVendorDTO dto)
        {
            await _uow.DyeingVendors.UpdateAsync(_mapper.Map<DyeingVendor>(dto));
            await _uow.SaveAsync();
            return await UpdatePayee(dto);
        }

        public async Task<bool> Delete(int id)
        {
            //delete the vendor from DyeingVendors table
            await _uow.DyeingVendors.DeleteAsync(id);

            //delete DyeingColorRates from the table for this specific vendor.
            await _uow.DyeingColorRates.DeleteRangeAsync(x => x.DyeingVendorId == id);

            return await _uow.SaveAsync() > 0;
        }

        //methods for vendor color rates
        public async Task<PagedResult<DyeingColorRateDTO>> GetVendorPriceList(int pageIndex, int pageSize, int vendorId)
        {
            return await _uow.DyeingColorRates.GetPagedAsync<DyeingColorRateDTO>(pageIndex, pageSize, _mapper, filter: x => x.DyeingVendorId == vendorId);
        }

        public async Task<DyeingColorRateDTO> GetPriceByID(object id)
        {
            return _mapper.Map<DyeingColorRateDTO>(await _uow.DyeingColorRates.GetByIdAsync(id));
        }

        public async Task<bool> AddPrice(DyeingColorRateDTO dto)
        {
            await _uow.DyeingColorRates.AddAsync(_mapper.Map<DyeingColorRate>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> UpdatePrice(DyeingColorRateDTO dto)
        {
            await _uow.DyeingColorRates.UpdateAsync(_mapper.Map<DyeingColorRate>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> DeletePrice(int id)
        {
            await _uow.DyeingColorRates.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }

        //add payee for the dyeing vendor
        public async Task<bool> AddPayee(DyeingVendor dto)
        {
            PayingEntity payeeDTO = new PayingEntity()
            {
                EntityId = dto.Id,
                Name = dto.Name,
                Type = "Dyeing Vendor",
                Status = (int)Status.Active,
                CreatedOn = DateTime.Now
            };
            await _uow.PayingEntities.AddAsync(payeeDTO);
            return await _uow.SaveAsync() > 0;
        }

        //update payee for the supplier
        public async Task<bool> UpdatePayee(DyeingVendorDTO dto)
        {
            var payee = await _uow.PayingEntities.Find(x => x.EntityId == dto.Id && x.Type.ToLower().Equals("dyeing vendor"));
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
