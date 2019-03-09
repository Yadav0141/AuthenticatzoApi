using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Models
{


    public class PlayList
    {
        public Guid? id { get; set; }
        public Guid? parentGroupId { get; set; }
        public string channelId { get; set; }
        public string playlistId { get; set; }
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public int genreId { get; set; }
        public int languageId { get; set; }
        public string groupImageUrl { get; set; }
        public string createdDate { get; set; }
        public int? playlistType { get; set; }
        public SelectListItemStringModel parentGroupSelectListItem {get;set;}
    }

    public class Video
    {
        public Guid? id { get; set; }
        public Guid? groupId { get; set; }
        public string videoName { get; set; }
        public string videoId { get; set; }
        public string imageUrl60 { get; set; }
        public string imageUrl90 { get; set; }
        public string imageUrl120 { get; set; }
        public int sequenceNumber { get; set; }
        public string createdDate { get; set; }
        
    }

        public class PlaylistVideoModel
        {
            public PlayList playlistDetail { get; set; }
            public List<Video> playlistVideos { get; set; }
            public int? totalResults { get; set; }
            
            
           
        }


    public class GetPlatlistVideo {
        public Guid? playlistId { get; set; }
        public int playlistType { get; set; }
    }
    
}
