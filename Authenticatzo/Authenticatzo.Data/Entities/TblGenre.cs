using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authenticatzo.Data.Entities
{
    [Table("tblGenre")]
    public partial class TblGenre
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("genreName")]
        [StringLength(150)]
        public string GenreName { get; set; }
        [Column("genreDescription")]
        [StringLength(255)]
        public string GenreDescription { get; set; }
    }
}
