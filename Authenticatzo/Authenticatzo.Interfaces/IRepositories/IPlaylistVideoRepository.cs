using Authenticatzo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Interfaces.IRepositories
{
    public interface IPlaylistVideoRepository
    {
         List<List<ViewPlaylist>> GetViewPlaylist(GetPlaylistVideoModel getModel);
        List<ViewPlaylist> GetPlaylistByType(int playlistType);
        VideoViewPlaylist GetVideosByPlaylist(GetPlatlistVideo getModel);
    }
}
