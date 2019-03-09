using Authenticatzo.Api.Helpers;
using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models;
using Authenticatzo.Models.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authenticatzo.Api.ApiServices
{
    public class UserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;
        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            this._appSettings = appSettings.Value;
            this._userRepository = userRepository;
        }

        public UserModel Authenticate(LoginModel loginModel,ModelStateDictionary ModelState)
        {
            var userModel = new UserModel();
           var response= _userRepository.ValidateLoginModel(loginModel, userModel);
            switch (response)
            {
                case LoginValidateResponseCode.INVALID_USERNAME_PASSWORD:
                    ModelState.AddModelError("error", "Invalid username or password.");
                    break;
                case LoginValidateResponseCode.USER_NOT_EXIST:
                    ModelState.AddModelError("error", "User does not exist.");
                    break;
                case LoginValidateResponseCode.USER_LOGIN_SUCCESS:
                    // authentication successful so generate jwt token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                             new Claim(ClaimTypes.Name, userModel.Id.ToString()),
                             new Claim(ClaimTypes.Email,userModel.EmailId)
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    userModel.token = tokenHandler.WriteToken(token);
                    return userModel;
                case LoginValidateResponseCode.PASSWORD_NOT_SET:
                    ModelState.AddModelError("error", "Password not set.Please set password first.");
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
