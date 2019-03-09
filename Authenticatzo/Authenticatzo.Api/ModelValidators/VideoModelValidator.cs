
using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authenticatzo.Api.ModelValidators
{
    public class VideoModelValidator
    {

        public IYoutubeRepository _youtubeRepository;
        public VideoModelValidator(IYoutubeRepository youtubeRepository) {
            this._youtubeRepository = youtubeRepository;
        }
        public void ValidatePlaylistModel(PlaylistVideoModel model,ModelStateDictionary ModelState)
        {
            if (model.playlistDetail == null)
            {
                ModelState.AddModelError("error", "Playlist model does not exist.");
                return;
            }
            if (_youtubeRepository.isPlaylistAlreadyExist(model))
            {
                ModelState.AddModelError("error",$"Playlist with id {model.playlistDetail.playlistId} already exist.");
            }
        }

        public void ValidateVideoModel(Video model, ModelStateDictionary ModelState)
        {

            if (_youtubeRepository.isVideoAlreadyExist(model))
            {
                ModelState.AddModelError("error", $"Video with id {model.videoId} for sequence {model.sequenceNumber}  already exist.");
            }
        }
    }
}
