
using Authenticatzo.Models;
using Authenticatzo.Models.Enum;

namespace Authenticatzo.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        LoginValidateResponseCode ValidateLoginModel(LoginModel loginModel, UserModel responseModel);
    }
}
