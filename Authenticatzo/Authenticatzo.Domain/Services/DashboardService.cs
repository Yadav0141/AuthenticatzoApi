using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Domain.Services
{
    public class DashboardService
    {
        private IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            this._dashboardRepository = dashboardRepository;
        }

        public DashboardPlaylistModel GetAllPlayList(GetDashboardPlaylistModel getDashboardPlaylistModel)
        {
            return _dashboardRepository.GetAllPlayList(getDashboardPlaylistModel);
        }

        public int deletePlaylistById(Guid playlistId)
        {
            return _dashboardRepository.deletePlaylistById(playlistId);
        }

    }
}
