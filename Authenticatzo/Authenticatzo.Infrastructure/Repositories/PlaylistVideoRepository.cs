using Authenticatzo.Data.Database;
using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Authenticatzo.Infrastructure.Repositories
{
   public class PlaylistVideoRepository: IPlaylistVideoRepository
    {
        string SP_GetAllPlayListVideo = "usp_GetPlaylistVideo @playlistTypeId";
        string SP_GetAllVideosByPlaylistId = "usp_GetVideosByPlaylistId @groupId";
        string SP_GetGroupIdHierarchy = "uspGetGroupIdHierarchy @groupId";
        public readonly IMapper _iMapper;
        public readonly Authenticatzo_DBContext _context;
        public PlaylistVideoRepository(Authenticatzo_DBContext context, IMapper iMapper)
        {
            this._context = context;
            this._iMapper = iMapper;
        }

        public List<List<ViewPlaylist>> GetViewPlaylist(GetPlaylistVideoModel getModel) {
            var playlist = _context.usp_GetPlaylistVideo
                                   .FromSql(SP_GetAllPlayListVideo, new SqlParameter("playlistTypeId", getModel.playlistType))
                                   .Skip(getModel.pageNumber * 12)
                                   .Take(12)
                                   .ToList();
            if (playlist != null && playlist.Count > 0)
            {
                var playlistVideos = _iMapper.Map<List<ViewPlaylist>>(playlist);
                return splitList(playlistVideos);
            }
          
            return null;
        }

        private static List<List<ViewPlaylist>> splitList(List<ViewPlaylist> playlist, int nSize = 4)
        {
            var list = new List<List<ViewPlaylist>>();

            for (int i = 0; i < playlist.Count; i += nSize)
            {
                list.Add(playlist.GetRange(i, Math.Min(nSize, playlist.Count - i)));
            }

            return list;
        }


        public List<ViewPlaylist> GetPlaylistByType(int playlistType) {
            
        var playlist = _context.usp_GetPlaylistVideo
                                  .FromSql(SP_GetAllPlayListVideo, new SqlParameter("playlistTypeId", playlistType))
                                  .OrderByDescending(x=>x.createdDate)
                                  .Take(16)
                                  .ToList();
            if (playlist != null && playlist.Count > 0)
            {
                var playlistVideos = _iMapper.Map<List<ViewPlaylist>>(playlist);
                return playlistVideos;
            }
            return null;
        }

        public VideoViewPlaylist GetVideosByPlaylist(GetPlatlistVideo getModel) {
            VideoViewPlaylist responseModel = new VideoViewPlaylist();
            var allplaylistByType = _context.usp_GetPlaylistVideo
                                 .FromSql(SP_GetAllPlayListVideo,
                                 new SqlParameter("playlistTypeId", getModel.playlistType))?
                                 .ToList();
                                
            if (allplaylistByType != null)
            {
                var playlist = allplaylistByType.Where(x => x.id == getModel.playlistId)?.FirstOrDefault();
                if (playlist != null)
                {
                    responseModel.playlistDetail = _iMapper.Map<ViewPlaylist>(playlist);
                }

                var groupIdHierarchy = _context.uspGetGroupIdHierarchy
                               .FromSql(SP_GetGroupIdHierarchy,
                               new SqlParameter("groupId", getModel.playlistId))?
                               .ToList();
               if (groupIdHierarchy != null)
                {
                    var lstGroupId = groupIdHierarchy.First().groupId.Split(',')
                        .Where(x => !(string.IsNullOrEmpty(x) || string.IsNullOrWhiteSpace(x)))?
                        .Select(x => new Guid(x)).ToList();
                    if (lstGroupId != null)
                    {
                       
                        var similarPlaylist = allplaylistByType.Where(x => lstGroupId.Contains(x.id))?
                            .OrderBy(x => x.createdDate)?
                            .ToList();
                        if (similarPlaylist != null)
                        {
                            responseModel.lstSimilarPlaylist = _iMapper.Map<List<ViewPlaylist>>(similarPlaylist);
                        }

                        var playlist50 = allplaylistByType
                            .Where(x => x.id != getModel.playlistId && lstGroupId.Contains(x.id))
                            .OrderByDescending(x => x.createdDate).Take(50)?.ToList();
                        if (playlist50 != null)
                        {
                            responseModel.lstTop50Playlist = _iMapper.Map<List<ViewPlaylist>>(playlist50);
                        }
                    }

                }
   }

            var allplaylistVideos= _context.usp_GetVideosByPlaylistId
                                 .FromSql(SP_GetAllVideosByPlaylistId,
                                 new SqlParameter("groupId", getModel.playlistId))?
                                 .ToList();
            if (allplaylistVideos != null)
            {
                responseModel.playlistVideos= _iMapper.Map<List<ViewVideo>>(allplaylistVideos);
            }
            return responseModel; 
        }
        
    }
} 
