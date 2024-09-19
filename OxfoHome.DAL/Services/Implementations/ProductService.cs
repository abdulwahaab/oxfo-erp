using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<ProductDTO>> GetAll()
        {
            IEnumerable<ProductDTO> dtoList = _mapper.Map<IEnumerable<ProductDTO>>(await _uow.Products.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<ProductDTO>> GetPagedListAsync(int pageIndex, int pageSize, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, Expression<Func<Product, bool>>? filter = null)
        {
            return await _uow.Products.GetPagedAsync<ProductDTO>(pageIndex, pageSize, _mapper, orderBy);
        }

        public async Task<ProductDTO> GetByID(object id)
        {
            return _mapper.Map<ProductDTO>(await _uow.Products.GetByIdAsync(id));
        }

        public async Task<bool> Add(ProductDTO dto)
        {
            await _uow.Products.AddAsync(_mapper.Map<Product>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Update(ProductDTO dto)
        {
            await _uow.Products.UpdateAsync(_mapper.Map<Product>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.Products.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }
    }
}
