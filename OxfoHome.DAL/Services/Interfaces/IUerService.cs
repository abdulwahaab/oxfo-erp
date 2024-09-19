using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IUserService
    {
        Task<IQueryable<UserDTO>> GetAll();
        Task<PagedResult<UserDTO>> GetPagedListAsync(int pageIndex, int pageSize,
        Func<IQueryable<UserDTO>, IOrderedQueryable<UserDTO>>? orderBy = null);
        Task<UserDTO> GetByID(object id);
        Task<bool> Add(UserDTO dto);
        Task<bool> Update(UserDTO dto);
        Task<bool> Delete(int id);

        //methods for roles
        Task<IQueryable<RoleDTO>> GetAllRoles();
    }
}
