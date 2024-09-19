using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class SharingService : ISharingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public SharingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<SharingVendorDTO>> GetAll()
        {
            IEnumerable<SharingVendorDTO> dtoList = _mapper.Map<IEnumerable<SharingVendorDTO>>(await _uow.SharingVendors.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<SharingVendorDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<SharingVendor>, IOrderedQueryable<SharingVendor>>? orderBy = null,
        Expression<Func<SharingVendor, bool>>? filter = null)
        {
            return await _uow.SharingVendors.GetPagedAsync<SharingVendorDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<SharingVendorDTO> GetByID(object id)
        {
            return _mapper.Map<SharingVendorDTO>(await _uow.SharingVendors.GetByIdAsync(id));
        }

        public async Task<bool> Add(SharingVendorDTO dto)
        {
            SharingVendor vendor = await _uow.SharingVendors.AddAndGetAsync(_mapper.Map<SharingVendor>(dto));
            await _uow.SaveAsync();
            return await AddPayee(vendor);
        }

        public async Task<bool> Update(SharingVendorDTO dto)
        {
            await _uow.SharingVendors.UpdateAsync(_mapper.Map<SharingVendor>(dto));
            await _uow.SaveAsync();
            return await UpdatePayee(dto);
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.SharingVendors.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }

        //add payee for the sharing vendor
        public async Task<bool> AddPayee(SharingVendor dto)
        {
            PayingEntity payeeDTO = new PayingEntity()
            {
                EntityId = dto.Id,
                Name = dto.Name,
                Type = "Sharing Vendor",
                Status = (int)Status.Active,
                CreatedOn = DateTime.Now
            };
            await _uow.PayingEntities.AddAsync(payeeDTO);
            return await _uow.SaveAsync() > 0;
        }

        //update payee for the sharing vendor
        public async Task<bool> UpdatePayee(SharingVendorDTO dto)
        {
            var payee = await _uow.PayingEntities.Find(x => x.EntityId == dto.Id && x.Type.ToLower().Equals("sharing vendor"));
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
