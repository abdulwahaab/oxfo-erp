using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class PayeeService : IPayeeService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public PayeeService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<PayeeDTO>> GetAll()
        {
            IEnumerable<PayeeDTO> dtoList = _mapper.Map<IEnumerable<PayeeDTO>>(await _uow.PayingEntities.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<PayeeDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<PayingEntity>, IOrderedQueryable<PayingEntity>>? orderBy = null,
        Expression<Func<PayingEntity, bool>>? filter = null)
        {
            return await _uow.PayingEntities.GetPagedAsync<PayeeDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<PayeeDTO> GetByID(object id)
        {
            return _mapper.Map<PayeeDTO>(await _uow.PayingEntities.GetByIdAsync(id));
        }

        public async Task<PayeeDTO> Find(Expression<Func<PayingEntity, bool>>? predicate = null)
        {
            return _mapper.Map<PayeeDTO>(await _uow.PayingEntities.Find(predicate));
        }

        public async Task<bool> Add(PayeeDTO dto)
        {
            await _uow.PayingEntities.AddAsync(_mapper.Map<PayingEntity>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Update(PayeeDTO dto)
        {
            await _uow.PayingEntities.UpdateAsync(_mapper.Map<PayingEntity>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.PayingEntities.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }
    }
}