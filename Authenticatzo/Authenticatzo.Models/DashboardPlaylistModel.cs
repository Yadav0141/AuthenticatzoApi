using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Models
{
    public class DashboardPlaylist
    {
        public Guid id { get; set; }
        public long seqNo { get; set; }
        public int?  playListTypeId { get; set; }
        public string playlistId { get; set; }
        public string playlistName { get; set; }
        public int genreId { get; set; }
        public string genreName { get; set; }
        public int languageId { get; set; }
        public string languageName { get; set; }
        public DateTime? createdDate { get; set; }
    }

    public class DashboardPlaylistModel {
        public List<DashboardPlaylist> dashboardPlaylist { get; set; }
        public int totalCount { get; set; }
    }

    public class GetDashboardPlaylistModel
    {
        public int playListTypeId { get; set; }
        public int pageNumber { get; set; }
        public int pageCount { get; set; }
       public string filter { get; set; }
       public string sortDirection { get; set; }
}
}
