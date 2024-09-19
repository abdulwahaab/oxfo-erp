using OxfoHome.DAL.DTOs;
using OxfoHome.Data.Entities;
using System.Linq.Expressions;

namespace OxfoHome.DAL.Services
{
    public interface IAuthService
    {
        Task<UserDTO?> AuthenticateUserAsync(string email, string password);
    }
}
