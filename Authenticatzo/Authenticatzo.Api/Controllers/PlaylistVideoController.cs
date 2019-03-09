using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authenticatzo.Domain.Services;
using Authenticatzo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Authenticatzo.Api.Controllers
{
    [Route("api/playlistvideo")]
    public class PlaylistVideoController : Controller
    {
        private readonly PlaylistVideoService _iPlaylistVideoService;
        private readonly ILogger<PlaylistVideoController> _iLogger;

        public PlaylistVideoController(PlaylistVideoService iPlaylistVideoService,
              ILogger<PlaylistVideoController> iLogger)
        {
            this._iPlaylistVideoService = iPlaylistVideoService;
            this._iLogger = iLogger;
        }

        [HttpPost]
        [Route("getviewplaylist")]
        public ActionResult GetViewPlaylist() {

            try
            {
                var playlistModel = Request.Form["playlistVideoModel"]; 
                GetPlaylistVideoModel getModel = JsonConvert.DeserializeObject<GetPlaylistVideoModel>(playlistModel);
                if (getModel == null)
                {
                    ModelState.AddModelError("error", "Playlist Video model does not exist.");
                    return BadRequest(ModelState);
                }
               
              
                var response = _iPlaylistVideoService.GetViewPlaylist(getModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }


        [HttpGet]
        [Route("getplaylistbytype")]
        public ActionResult GetPlaylistByType(int playlistType) {
            try
            {
                if (playlistType==0)
                {
                    ModelState.AddModelError("error", "Playlist type is required.");
                    return BadRequest(ModelState);
                }
                return Ok(_iPlaylistVideoService.GetPlaylistByType(playlistType));
            }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpPost]
        [Route("getvideosbyplaylistid")]
        public ActionResult GetVideosByPlaylist()
        {
            try
            {
                var getPlatlistVideo = Request.Form["GetPlatlistVideo"];
                GetPlatlistVideo getModel = JsonConvert.DeserializeObject<GetPlatlistVideo>(getPlatlistVideo);

                if (getModel.playlistId == null || getModel.playlistType==0)
                {
                    ModelState.AddModelError("error", "Playlist id and playlist type is required.");
                    return BadRequest(ModelState);
                }
                return Ok(_iPlaylistVideoService.GetVideosByPlaylist(getModel));
            }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

    }
}
