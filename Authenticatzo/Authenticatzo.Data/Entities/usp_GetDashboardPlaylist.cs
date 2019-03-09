using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticatzo.Data.Entities
{
   public partial class usp_GetDashboardPlaylist
    {
        public Guid id { get; set; }
        public long seqNo { get; set; }
        public int? playListTypeId { get; set; }
        public string playlistId { get; set; }
        public string playlistName { get; set; }
        public int genreId { get; set; }
        public string genreName { get; set; }
        public int languageId { get; set; }
        public string languageName { get; set; }
        public DateTime? createdDate { get; set; }
    }
}
