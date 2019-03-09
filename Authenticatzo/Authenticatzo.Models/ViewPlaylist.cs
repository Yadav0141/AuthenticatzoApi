using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Models
{
    public class ViewPlaylist
    {
        public string imageUrl90 { get; set; }
        public string playlistId { get; set; }
        public string groupName { get; set; }
        public string genreName { get; set; }
        public string languageName { get; set; }
        public DateTime? createdDate { get; set; }
        public Guid id { get; set; }
    }

    public class GetPlaylistVideoModel {
        public int playlistType { get; set; }

        public int pageNumber { get; set; }
    }

    public class ViewVideo
    {
        public Guid? groupId { get; set; }
        public Guid? id { get; set; }
        public string videoId { get; set; }
        public string videoName { get; set; }
        public string imageUrl60 { get; set; }
        public string imageUrl90 { get; set; }
        public string imageUrl120 { get; set; }

        public int? sequenceNumber { get; set; }
        public DateTime? createdDate { get; set; }
    }

    public class VideoViewPlaylist
    {
        public List<ViewVideo> playlistVideos { get; set; }
        public List<ViewPlaylist> lstTop50Playlist { get; set; }
        public ViewPlaylist playlistDetail{ get; set; }

        public List<ViewPlaylist> lstSimilarPlaylist { get; set; }
    }

  
}
