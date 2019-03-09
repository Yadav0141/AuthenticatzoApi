using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authenticatzo.Api.ModelValidators;
using Authenticatzo.Domain.Services;
using Authenticatzo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;

namespace Authenticatzo.Api.Controllers
{
    [Route("api/youtube")]
    [Authorize]
    public class YoutubeController : Controller
    {

        private readonly YoutubeService _iYoutubeService;
        private readonly ILogger<UserController> _iLogger;
        private readonly IConfiguration _configuration;
        private string API_KEY;
        private readonly VideoModelValidator _VideoModelValidator;
        public YoutubeController(YoutubeService _iYoutubeService,
                                  ILogger<UserController> _iLogger,
                                  IConfiguration _configuration,
                                  VideoModelValidator _VideoModelValidator)
        {
            this._iLogger = _iLogger;
            this._iYoutubeService = _iYoutubeService;
            this._configuration = _configuration;
            this.API_KEY = _configuration.GetValue<string>("YOUTUBE_API_KEY");
            this._VideoModelValidator = _VideoModelValidator;
        }

        [HttpGet]
        [Route("getvideobyid")]
        public IActionResult getvideobyid(String videoId)
        {
            if (String.IsNullOrEmpty(videoId))
            {
                ModelState.AddModelError("error", "Video Id required.");
                return BadRequest(ModelState);
            }
            var response=  _iYoutubeService.GetVideoById(videoId, API_KEY);
            return Ok(response);
        }

       
        [HttpGet]
        [Route("getvideosbysearch")]
        public IActionResult getvideosbysearch(string searchTerm)
        {
          
            var response =  _iYoutubeService.GetVideosBySearchTerm(searchTerm, API_KEY);
            return Ok(response);
        }


        [HttpGet]
        [Route("getplaylistbyid")]
        public IActionResult getplaylistbyid(string playlistId) {
            if (String.IsNullOrEmpty(playlistId))
            {
                ModelState.AddModelError("error", "Playlist Id required.");
                return BadRequest(ModelState);
            }
          
            var response =  _iYoutubeService.GetVideosByPlaylistId(playlistId, API_KEY);
            return Ok(response);
        }

        [HttpGet]
        [Route("GetGenreSelectList")]
        public IActionResult GetGenreSelectList()
        {
           
            var response = _iYoutubeService.GetGenreSelectList();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetLanguageSelectList")]
        public IActionResult GetLanguageSelectList()
        {

            var response = _iYoutubeService.GetLanguageSelectList();
            return Ok(response);
        }

        [HttpPost]
        [Route("SavePlaylist")]
        public IActionResult SavePlaylist()
        {
            try {
                var playlistModel = Request.Form["playlistModel"]; 
                var playlistVideoModel = JsonConvert.DeserializeObject<PlaylistVideoModel>(playlistModel);
                _VideoModelValidator.ValidatePlaylistModel(playlistVideoModel, ModelState);
                if (ModelState.IsValid)
                {
                    if (playlistVideoModel.playlistVideos != null && playlistVideoModel.playlistVideos.Count() > 0)
                    {
                        foreach (var video in playlistVideoModel.playlistVideos)
                        {
                            _VideoModelValidator.ValidateVideoModel(video, ModelState);
                            if (!ModelState.IsValid)
                            {
                                return BadRequest(ModelState);
                            }
                        }
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
                var response = _iYoutubeService.SavePlaylist(playlistVideoModel);
                     return Ok(response);
               }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpPost]
        [Route("SaveVideo")]
        public IActionResult SaveVideo()
        {
            try
            {
                var playlistModel = Request.Form["videoModel"]; 
                var VideoModel = JsonConvert.DeserializeObject<Video>(playlistModel);
                if (VideoModel == null)
                {
                    ModelState.AddModelError("error","Video model does not exist.");
                    return BadRequest(ModelState);
                }
                _VideoModelValidator.ValidateVideoModel(VideoModel, ModelState);
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                var response = _iYoutubeService.SaveVideo(VideoModel);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _iLogger.LogCritical($"Exception while deleting a user", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet]
        [Route("GetPlaylistVideoModelById")]
        public IActionResult GetPlaylistVideoModelById(Guid? playlistId)
        {
            if (playlistId == null)
            {
                ModelState.AddModelError("error", "Playlist Id is required.");
                return BadRequest(ModelState);
            }
            var response = _iYoutubeService.GetPlaylistVideoModelById(playlistId.Value);
            return Ok(response);
        }

        [HttpGet]
        [Route("DeleteVideoById")]
        public IActionResult DeleteVideoById(string videoId)
        {
            if (String.IsNullOrEmpty(videoId) || String.IsNullOrWhiteSpace(videoId))
            {
                ModelState.AddModelError("error", "Video Id is required.");
                return BadRequest(ModelState);
            }
            var response = _iYoutubeService.deleteVideoById(new Guid(videoId));
            return Ok(response);
            
        }

        [HttpGet]
        [Route("GetParentPlaylistSelectItems")]
        public IActionResult GetParentPlaylistSelectItems(int playlistType,string text)
        {
            if (playlistType==0)
            {
                ModelState.AddModelError("error", "Playlist type is required.");
                return BadRequest(ModelState);
            }
            var response = _iYoutubeService.GetParentPlaylistSelectItems(playlistType, text);
            return Ok(response);

        }

    }
}
