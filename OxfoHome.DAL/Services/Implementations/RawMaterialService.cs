using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class RawMaterialService : IRawMaterialService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public RawMaterialService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<RawMaterialDTO>> GetAll()
        {
            IEnumerable<RawMaterialDTO> dtoList = _mapper.Map<IEnumerable<RawMaterialDTO>>(await _uow.RawMaterials.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<RawMaterialDTO>> GetPagedListAsync(int pageIndex, int pageSize,
            Func<IQueryable<RawMaterial>, IOrderedQueryable<RawMaterial>>? orderBy = null,
            Expression<Func<RawMaterial, bool>>? filter = null)
        {
            return await _uow.RawMaterials.GetPagedAsync<RawMaterialDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<RawMaterialDTO> GetByID(object id)
        {
            return _mapper.Map<RawMaterialDTO>(await _uow.RawMaterials.GetByIdAsync(id));
        }

        public async Task<bool> Add(RawMaterialDTO dto)
        {
            await _uow.RawMaterials.AddAsync(_mapper.Map<RawMaterial>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Update(RawMaterialDTO dto)
        {
            await _uow.RawMaterials.UpdateAsync(_mapper.Map<RawMaterial>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.RawMaterials.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }
    }
}
