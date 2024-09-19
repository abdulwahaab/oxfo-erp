
namespace OxfoHome.DAL.Services
{
    public interface IHashService
    {
        //password hashing
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
