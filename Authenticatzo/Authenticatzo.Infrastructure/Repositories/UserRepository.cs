

using System;
using AutoMapper;
using System.Linq;
using Authenticatzo.Models;
using Authenticatzo.Data.Database;
using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models.Enum;
using Authenticatzo.Infrastructure.Helpers;

namespace Authenticatzo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly IMapper _iMapper;
        public readonly Authenticatzo_DBContext _context;
        private readonly IPasswordHasher _passwordHasher;
        public UserRepository(Authenticatzo_DBContext context, IMapper iMapper, IPasswordHasher passwordHasher) 
        {
            this._context = context;
            this._iMapper = iMapper;
            this._passwordHasher = passwordHasher;
        }

       

        public LoginValidateResponseCode ValidateLoginModel(LoginModel loginModel, UserModel responseModel)
        {
            var user = _context.TblUser.FirstOrDefault(x => x.EmailId == loginModel.username);
            if (user != null)
            {
                if (String.IsNullOrEmpty(user.PasswordByte) || String.IsNullOrWhiteSpace(user.PasswordSalt))
                {
                    return LoginValidateResponseCode.PASSWORD_NOT_SET;
                }
                var hashedPassword = _passwordHasher.HashPassword(loginModel.password, user.PasswordSalt);
                if (String.Equals(user.PasswordByte, hashedPassword, StringComparison.OrdinalIgnoreCase))
                {
                    _iMapper.Map(user, responseModel);
                    return LoginValidateResponseCode.USER_LOGIN_SUCCESS;
                }
                else
                {
                    return LoginValidateResponseCode.INVALID_USERNAME_PASSWORD;
                }
            }
            return LoginValidateResponseCode.USER_NOT_EXIST;
        }
    }
}
