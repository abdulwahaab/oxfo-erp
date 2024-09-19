using OxfoHome.DAL.DTOs;

namespace OxfoHome.DAL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHashService _hashService;
        private readonly IUnitOfWork _uow;

        public AuthService(IUnitOfWork unitOfWork, IHashService hashService)
        {
            _uow = unitOfWork;
            _hashService = hashService;
        }

        public async Task<UserDTO?> AuthenticateUserAsync(string email, string password)
        {
            // Authentication successful
            var user = _uow.Users.Table
                   .Join(_uow.Roles.Table,
                   user => user.RoleId,
                   role => role.Id,
                   (user, role) => new UserDTO
                   {
                       Id = user.Id,
                       RoleId = user.Id,
                       Role = role.Name,
                       Name = user.Name,
                       Email = user.Email,
                       Password = user.Password,
                       Status = user.Status
                   })
                   .Where(x => x.Email == email)
                   .FirstOrDefault();
            if (user == null || !_hashService.VerifyPassword(password, user.Password))
            {
                // Invalid credentials
                return null;
            }
            else
                return user;
        }
    }
}