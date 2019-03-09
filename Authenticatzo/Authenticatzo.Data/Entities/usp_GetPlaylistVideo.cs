using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Data.Entities
{
   public  class usp_GetPlaylistVideo
    {
        public Guid id { get; set; }
      
        public string playlistId { get; set; }
        public string groupName { get; set; }
       
        public string genreName { get; set; }
        public string imageUrl90 { get; set; }
        public string languageName { get; set; }
        public DateTime? createdDate { get; set; }
    }
}
