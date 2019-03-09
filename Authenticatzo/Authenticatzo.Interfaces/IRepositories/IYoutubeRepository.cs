using Authenticatzo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Interfaces.IRepositories
{
  public interface IYoutubeRepository
    {
        List<SelectListItemModel> GetLanguageSelectList();
        List<SelectListItemModel> GetGenreSelectList();
        Guid? SavePlaylist(PlaylistVideoModel playlistVideoModel);
        Guid? SaveVideo(Video video);
        PlaylistVideoModel GetPlaylistVideoModelById(Guid playlistId);
        int deleteVideoById(Guid videoId);
        bool isPlaylistAlreadyExist(PlaylistVideoModel playlistVideoModel);
        bool isVideoAlreadyExist(Video playlistVideoModel);
        List<SelectListItemStringModel> GetParentPlaylistSelectItems(int playlistType, string text);

    }
}
