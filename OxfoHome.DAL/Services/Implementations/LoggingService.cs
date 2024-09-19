using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public LoggingService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }
            
        public async Task<IQueryable<ErrorLogDTO>> GetAll()
        {
            IEnumerable<ErrorLogDTO> dtoList = _mapper.Map<IEnumerable<ErrorLogDTO>>(await _uow.ErrorLogs.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<ErrorLogDTO>> GetPagedListAsync(int pageIndex, int pageSize, Func<IQueryable<ErrorLog>, IOrderedQueryable<ErrorLog>>? orderBy = null, Expression<Func<ErrorLog, bool>>? filter = null)
        {
            return await _uow.ErrorLogs.GetPagedAsync<ErrorLogDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<ErrorLogDTO> GetByID(object id)
        {
            return _mapper.Map<ErrorLogDTO>(await _uow.ErrorLogs.GetByIdAsync(id));
        }

        public async Task<bool> Add(ErrorLogDTO dto)
        {
            await _uow.ErrorLogs.AddAsync(_mapper.Map<ErrorLog>(dto));
            return await _uow.SaveAsync() > 0;
        }
    }
}
