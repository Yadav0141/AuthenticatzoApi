using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Authenticatzo.Models;
using Authenticatzo.Api.ApiServices;
using Newtonsoft.Json;

namespace Authenticatzo.Api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly UserService _iUserService;
        private readonly ILogger<UserController> _iLogger;

        public UserController(UserService _iUserService,
                                  ILogger<UserController> _iLogger)
        {
            this._iLogger = _iLogger;
            this._iUserService = _iUserService;
        }

        [Route("authenticate")]
        [HttpPost]
        public IActionResult Authenticate()
        {
            try
            {
                var loginModel = Request.Form["loginModel"];
            LoginModel getModel = JsonConvert.DeserializeObject<LoginModel>(loginModel);

            if (!TryValidateModel(getModel))
            {
                
                return BadRequest(ModelState);
            }
          var user=  _iUserService.Authenticate(getModel, ModelState);
            if (user==null) {
                return BadRequest(ModelState);
            }
            return Ok(user);
            }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }
     
    }
}
