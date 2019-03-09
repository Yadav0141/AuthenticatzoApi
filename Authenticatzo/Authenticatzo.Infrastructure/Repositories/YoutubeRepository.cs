using Authenticatzo.Data.Database;
using Authenticatzo.Data.Entities;
using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authenticatzo.Infrastructure.Repositories
{
    public class YoutubeRepository : IYoutubeRepository
    {
        public readonly IMapper _iMapper;
        public readonly Authenticatzo_DBContext _context;
        public YoutubeRepository(Authenticatzo_DBContext context, IMapper iMapper)
        {
            this._context = context;
            this._iMapper = iMapper;
        }


        public List<SelectListItemModel> GetLanguageSelectList() {
            return _context.TblLanguage.Select(x => new SelectListItemModel { text = x.LanguageName, value = x.Id })?.ToList();
        }

        public List<SelectListItemModel> GetGenreSelectList()
        {
            return _context.TblGenre.Select(x => new SelectListItemModel { text = x.GenreName, value = x.Id })?.ToList();
        }

        public Guid? SavePlaylist(PlaylistVideoModel playlistVideoModel) {
            if (playlistVideoModel.playlistDetail != null)
            {
                var tblPlaylist = new TblYoutubeVideoGroups();
                if (playlistVideoModel.playlistDetail.id != null)
                {
                    tblPlaylist = _context.TblYoutubeVideoGroups.Where(group => group.Id == playlistVideoModel.playlistDetail.id).FirstOrDefault();
                    if (tblPlaylist == null)
                    {
                        tblPlaylist = _iMapper.Map<TblYoutubeVideoGroups>(playlistVideoModel.playlistDetail);
                    }
                }
                _iMapper.Map(playlistVideoModel.playlistDetail, tblPlaylist);
                if (playlistVideoModel.playlistDetail.parentGroupSelectListItem != null)
                {
                    if (!(String.IsNullOrEmpty(playlistVideoModel.playlistDetail.parentGroupSelectListItem.value) ||
                       String.IsNullOrWhiteSpace(playlistVideoModel.playlistDetail.parentGroupSelectListItem.value)))
                    {
                        tblPlaylist.ParentGroupId = new Guid(playlistVideoModel.playlistDetail.parentGroupSelectListItem.value);
                    }
                }
                if (tblPlaylist.Id == Guid.Empty)
                {
                    tblPlaylist.CreatedDate = DateTime.Now;
                    tblPlaylist.Id = Guid.NewGuid();
                    _context.TblYoutubeVideoGroups.Add(tblPlaylist);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Entry(tblPlaylist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                }
                playlistVideoModel.playlistDetail.id = tblPlaylist.Id;
            }
            if (playlistVideoModel.playlistVideos != null)
            {
                foreach (var video in playlistVideoModel.playlistVideos)
                {
                    video.groupId = playlistVideoModel.playlistDetail.id;
                    SaveVideo(video);
                }
            }
            return playlistVideoModel.playlistDetail.id;


        }

        public Guid? SaveVideo(Video video) {
            var tblVideo = new TblYoutubeVideos();
            if (tblVideo.Id != null)
            {
                tblVideo = _context.TblYoutubeVideos.Where(v => v.Id == video.id).FirstOrDefault();
                if (tblVideo == null)
                {
                    tblVideo = _iMapper.Map<TblYoutubeVideos>(video);
                }
            }
            _iMapper.Map(video, tblVideo);

            if (tblVideo.Id == Guid.Empty)
            {
                tblVideo.CreatedDate = DateTime.Now;
                tblVideo.Id = Guid.NewGuid();
                _context.TblYoutubeVideos.Add(tblVideo);
                _context.SaveChanges();
            }
            else
            {
                _context.Entry(tblVideo).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

            return tblVideo.Id;


        }

        public PlaylistVideoModel GetPlaylistVideoModelById(Guid playlistId) {
            PlaylistVideoModel playlistVideoModel = new PlaylistVideoModel();
            var playlistDetail = _context.TblYoutubeVideoGroups.FirstOrDefault(playlist => playlist.Id == playlistId);
            if (playlistDetail != null)
            {
                playlistVideoModel.playlistDetail = _iMapper.Map<PlayList>(playlistDetail);
                if (playlistVideoModel.playlistDetail.parentGroupId != null)
                {
                    var groupName = _context.TblYoutubeVideoGroups.FirstOrDefault(y=>y.Id== playlistVideoModel.playlistDetail.parentGroupId)?.GroupName;
                    playlistVideoModel.playlistDetail.parentGroupSelectListItem = new SelectListItemStringModel { text= groupName ,value= playlistVideoModel.playlistDetail.parentGroupId.ToString() };

                }

                var playlistVideos = _context.TblYoutubeVideos.OrderBy(x => x.SequenceNumber).Where(video => video.GroupId == playlistDetail.Id && video.IsDeleted != true)?.ToList();
                if (playlistVideos != null && playlistVideos.Count > 0)
                {
                    playlistVideoModel.playlistVideos = _iMapper.Map<List<Video>>(playlistVideos);
                    playlistVideoModel.totalResults = playlistVideoModel.playlistVideos.Count();
                }
            }
            return playlistVideoModel;
        }


        public int deleteVideoById(Guid videoId) {
            var video = _context.TblYoutubeVideos.FirstOrDefault(x => x.Id == videoId);
            if (video != null)
            {
                video.IsDeleted = true;
                _context.Entry(video).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return _context.SaveChanges();
            }

            return 0;

        }


        public bool isPlaylistAlreadyExist(PlaylistVideoModel playlistVideoModel)
        {
            if (_context.TblYoutubeVideoGroups.Any(x => x.Id != playlistVideoModel.playlistDetail.id && x.PlaylistId == playlistVideoModel.playlistDetail.playlistId))
            {
               
                return true;
            }
            
            return false;
        }
        public bool isVideoAlreadyExist(Video videoModel)
        {
            if (_context.TblYoutubeVideos.Any(x => x.Id != videoModel.id && x.VideoId == videoModel.videoId))
            {
                return true;
            }
            return false;
        }

        public List<SelectListItemStringModel> GetParentPlaylistSelectItems(int playlistType,string text)
        {
            var selectList = _context.TblYoutubeVideoGroups.Where(x => x.IsDeleted != true && x.GroupName.Contains(text, StringComparison.InvariantCultureIgnoreCase))
                             .Select(x => new SelectListItemStringModel { text = x.GroupName, value = x.Id.ToString() })?.ToList();
            return selectList;
        }

    }
}
