using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Models.Enum
{
    public enum LoginValidateResponseCode
    {
        INVALID_USERNAME_PASSWORD,
        USER_NOT_EXIST,
        USER_LOGIN_SUCCESS,
        PASSWORD_NOT_SET
    }
}
