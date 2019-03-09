using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Data.Entities
{
  public  class usp_GetVideosByPlaylistId
    {
        public Guid? id { get; set; }
        public Guid? groupId { get; set; }
        public string videoName { get; set; }
        public string videoId { get; set; }
        public string imageUrl60 { get; set; }
        public string imageUrl90 { get; set; }
        public string imageUrl120 { get; set; }
        public int sequenceNumber { get; set; }
        public DateTime? createdDate { get; set; }
    }
}
