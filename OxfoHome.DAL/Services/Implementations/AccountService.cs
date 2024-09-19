using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public AccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<AccountDTO>> GetAll()
        {
            IEnumerable<AccountDTO> dtoList = _mapper.Map<IEnumerable<AccountDTO>>(await _uow.Accounts.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<AccountDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<Account>, IOrderedQueryable<Account>>? orderBy = null,
        Expression<Func<Account, bool>>? filter = null)
        {
            return await _uow.Accounts.GetPagedAsync<AccountDTO>(pageIndex, pageSize, _mapper, orderBy, filter);
        }

        public async Task<AccountDTO> GetByID(object id)
        {
            return _mapper.Map<AccountDTO>(await _uow.Accounts.GetByIdAsync(id));
        }

        public async Task<bool> Add(AccountDTO dto)
        {
            await _uow.Accounts.AddAsync(_mapper.Map<Account>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Update(AccountDTO dto)
        {
            await _uow.Accounts.UpdateAsync(_mapper.Map<Account>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.Accounts.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }
    }
}
