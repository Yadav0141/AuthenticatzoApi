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
    public class DashboardRepository: IDashboardRepository
    {
        string SP_GetAllPlayList = "usp_GetDashboardPlaylist @playlistTypeId";
        public readonly IMapper _iMapper;
        public readonly Authenticatzo_DBContext _context;
      public  DashboardRepository(Authenticatzo_DBContext context, IMapper iMapper) {
            this._context = context;
            this._iMapper = iMapper;
        }
        public DashboardPlaylistModel GetAllPlayList(GetDashboardPlaylistModel getDashboardPlaylistModel)
        {
            var totalCount = _context.TblYoutubeVideoGroups
                                     .Count(x => x.PlayListType == getDashboardPlaylistModel.playListTypeId && x.IsDeleted != true);
            var playlist=   _context.usp_GetDashboardPlaylist
                                    .FromSql(SP_GetAllPlayList, new SqlParameter("playlistTypeId", getDashboardPlaylistModel.playListTypeId))
                                    .Skip(getDashboardPlaylistModel.pageNumber  * getDashboardPlaylistModel.pageCount)
                                    .Take(getDashboardPlaylistModel.pageCount)
                                    .ToList();
            var dashboardPlaylistModel = new DashboardPlaylistModel();
            dashboardPlaylistModel.totalCount = totalCount;
            dashboardPlaylistModel.dashboardPlaylist = _iMapper.Map<List<DashboardPlaylist>>(playlist);
            dashboardPlaylistModel.dashboardPlaylist.ForEach(playlistModel =>
            {
                if (playlistModel.playlistName.Length > 40)
                {
                    playlistModel.playlistName = playlistModel.playlistName.Substring(0,40) + "...";
                }
            });
            return dashboardPlaylistModel;


        }
        public int deletePlaylistById(Guid playlistId) {
            var playlist=  _context.TblYoutubeVideoGroups.FirstOrDefault(x => x.Id == playlistId);
            if (playlist != null)
            {
                playlist.IsDeleted = true;
                _context.Entry(playlist).State = EntityState.Modified;
               return  _context.SaveChanges();
            }
            return 0;

        }
    }
}
