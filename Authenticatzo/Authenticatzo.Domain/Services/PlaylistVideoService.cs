using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Domain.Services
{
    public class PlaylistVideoService
    {

        private IPlaylistVideoRepository _playlistVideoRepository;
        public PlaylistVideoService(IPlaylistVideoRepository playlistVideoRepository)
        {
            this._playlistVideoRepository = playlistVideoRepository;
        }

        public List<List<ViewPlaylist>> GetViewPlaylist(GetPlaylistVideoModel getModel)
        {
            return this._playlistVideoRepository.GetViewPlaylist(getModel);
        }

        public List<ViewPlaylist> GetPlaylistByType(int playlistType)
        {
            return this._playlistVideoRepository.GetPlaylistByType(playlistType);
        }

        public VideoViewPlaylist GetVideosByPlaylist(GetPlatlistVideo getModel)
        {
            return this._playlistVideoRepository.GetVideosByPlaylist(getModel);
        }

    }
}
