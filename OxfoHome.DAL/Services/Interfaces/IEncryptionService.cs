
namespace OxfoHome.DAL.Services
{
    public interface IEncryptionService
    {
        //URL encryption
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
