using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authenticatzo.Domain.Services;
using Authenticatzo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Authenticatzo.Api.Controllers
{
    [Route("api/dashboard")]
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DashboardService _iDashboardService;
        private readonly ILogger<DashboardController> _iLogger;

        public DashboardController(DashboardService iDashboardService,
              ILogger<DashboardController> iLogger) {
            this._iDashboardService = iDashboardService;
            this._iLogger = iLogger;
        }
        [HttpPost]
        [Route("getdashboardplaylist")]
        public IActionResult GetDashboardPlaylist()
        {
            try
            {
                var playlistModel = Request.Form["getDashboardPlaylistModel"];
                GetDashboardPlaylistModel getDashboardPlaylistModel = JsonConvert.DeserializeObject<GetDashboardPlaylistModel>(playlistModel);
                return Ok(_iDashboardService.GetAllPlayList(getDashboardPlaylistModel));
            }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet]
        [Route("deleteplaylistbyid")]
        public IActionResult deleteplaylistbyid(string playlistId)
        {
            
            try
            {
                if (String.IsNullOrEmpty(playlistId) || String.IsNullOrWhiteSpace(playlistId))
                {
                    ModelState.AddModelError("error", "Playlist id is required.");
                    return BadRequest(ModelState);
                }
                 return Ok(_iDashboardService.deletePlaylistById(new  Guid(playlistId)));
            }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

       
    }
}
