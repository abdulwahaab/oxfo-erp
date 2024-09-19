using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<CustomerDTO>> GetAll()
        {
            IEnumerable<CustomerDTO> dtoList = _mapper.Map<IEnumerable<CustomerDTO>>(await _uow.Customers.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<CustomerDTO>> GetPagedListAsync(int pageIndex, int pageSize, Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? orderBy = null, Expression<Func<Customer, bool>>? filter = null)
        {
            return await _uow.Customers.GetPagedAsync<CustomerDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<CustomerDTO> GetByID(object id)
        {
            return _mapper.Map<CustomerDTO>(await _uow.Customers.GetByIdAsync(id));
        }

        public async Task<bool> Add(CustomerDTO dto)
        {
            Customer customer = await _uow.Customers.AddAndGetAsync(_mapper.Map<Customer>(dto));
            await _uow.SaveAsync();
            return await AddPayee(customer);
        }

        public async Task<bool> Update(CustomerDTO dto)
        {
            await _uow.Customers.UpdateAsync(_mapper.Map<Customer>(dto));
            await _uow.SaveAsync();
            return await UpdatePayee(dto);
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.Customers.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }

        //add payee for the customer
        public async Task<bool> AddPayee(Customer dto)
        {
            PayingEntity payeeDTO = new PayingEntity()
            {
                EntityId = dto.Id,
                Name = dto.Name,
                Type = "Customer",
                Status = (int)Status.Active,
                CreatedOn = DateTime.Now
            };
            await _uow.PayingEntities.AddAsync(payeeDTO);
            return await _uow.SaveAsync() > 0;
        }

        //update payee for the customer
        public async Task<bool> UpdatePayee(CustomerDTO dto)
        {
            var payee = await _uow.PayingEntities.Find(x => x.EntityId == dto.Id && x.Type.ToLower().Equals("customer"));
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
