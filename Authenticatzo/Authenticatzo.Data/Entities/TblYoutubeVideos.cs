using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authenticatzo.Data.Entities
{
    [Table("tblYoutubeVideos")]
    public partial class TblYoutubeVideos
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("videoId")]
        [StringLength(150)]
        public string VideoId { get; set; }
        [Column("videoName")]
        [StringLength(255)]
        public string VideoName { get; set; }
        [Column("description", TypeName = "text")]
        public string Description { get; set; }
        [Column("imageUrl60")]
        [StringLength(255)]
        public string ImageUrl60 { get; set; }
        [Column("imageUrl90")]
        [StringLength(255)]
        public string ImageUrl90 { get; set; }
        [Column("imageUrl120")]
        [StringLength(255)]
        public string ImageUrl120 { get; set; }
        [Column("groupId")]
        public Guid? GroupId { get; set; }
        [Column("sequenceNumber")]
        public int? SequenceNumber { get; set; }
        [Column("createdDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column("isDeleted")]
        public bool? IsDeleted { get; set; }
    }
}
