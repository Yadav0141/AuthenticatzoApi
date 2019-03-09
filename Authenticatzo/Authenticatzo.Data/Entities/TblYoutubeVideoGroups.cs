using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authenticatzo.Data.Entities
{
    [Table("tblYoutubeVideoGroups")]
    public partial class TblYoutubeVideoGroups
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("parentGroupId")]
        public Guid? ParentGroupId { get; set; }
        [Column("channelId")]
        [StringLength(150)]
        public string ChannelId { get; set; }
        [Column("playlistId")]
        [StringLength(150)]
        public string PlaylistId { get; set; }
        [Column("groupName")]
        [StringLength(150)]
        public string GroupName { get; set; }
        [Column("groupDescription", TypeName = "text")]
        public string GroupDescription { get; set; }
        [Column("genreId")]
        public int? GenreId { get; set; }
        [Column("languageId")]
        public int? LanguageId { get; set; }
        [Column("groupImageUrl")]
        [StringLength(255)]
        public string GroupImageUrl { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
        [Column("playListType")]
        public int? PlayListType { get; set; }
    }
}
