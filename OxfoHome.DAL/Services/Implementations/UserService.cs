using AutoMapper;
using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace OxfoHome.DAL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uow = unitOfWork;
        }

        public async Task<IQueryable<UserDTO>> GetAll()
        {
            IEnumerable<UserDTO> dtoList = _mapper.Map<IEnumerable<UserDTO>>(await _uow.Users.GetAllAsync());
            return dtoList.AsQueryable();
        }

        public async Task<PagedResult<UserDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<UserDTO>, IOrderedQueryable<UserDTO>>? orderBy)
        {
            var query = _uow.Users.Table
                        .Join(_uow.Roles.Table,
                        user => user.RoleId,
                        role => role.Id,
                        (user, role) => new UserDTO
                        {
                            Id = user.Id,
                            Name = user.Name,
                            RoleId = user.Id,
                            Role = role.Name,
                            Email = user.Email,
                            Phone = user.Phone,
                            LastLogin = user.LastLogin
                        });

            query = orderBy(query);

            var totalItems = query.Count();

            var itemList = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<UserDTO>
            {
                Items = itemList,
                TotalCount = totalItems
            };
        }

        public async Task<UserDTO> GetByID(object id)
        {
            return _mapper.Map<UserDTO>(await _uow.Users.GetByIdAsync(id));
        }

        public async Task<bool> Add(UserDTO dto)
        {
            await _uow.Users.AddAsync(_mapper.Map<User>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Update(UserDTO dto)
        {
            await _uow.Users.UpdateAsync(_mapper.Map<User>(dto));
            return await _uow.SaveAsync() > 0;
        }

        public async Task<bool> Delete(int id)
        {
            await _uow.Users.DeleteAsync(id);
            return await _uow.SaveAsync() > 0;
        }

        //method for roles
        public async Task<IQueryable<RoleDTO>> GetAllRoles()
        {
            IEnumerable<RoleDTO> dtoList = _mapper.Map<IEnumerable<RoleDTO>>(await _uow.Roles.GetAllAsync());
            return dtoList.AsQueryable();
        }
    }
}
