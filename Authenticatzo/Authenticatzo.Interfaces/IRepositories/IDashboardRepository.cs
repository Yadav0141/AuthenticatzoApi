using Authenticatzo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Interfaces.IRepositories
{
    public interface IDashboardRepository
    {
        DashboardPlaylistModel GetAllPlayList(GetDashboardPlaylistModel getDashboardPlaylistModel);
        int deletePlaylistById(Guid playlistId);
    }
}
