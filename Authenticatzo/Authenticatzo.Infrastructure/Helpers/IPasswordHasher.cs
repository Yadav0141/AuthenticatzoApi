using BlueRidgeUtility_BAL.Models;

namespace Authenticatzo.Infrastructure.Helpers
{
    public interface IPasswordHasher
    {
        string HashPassword(string password, string salt);
        HashedPassword HashPassword(string password);
    }
}