using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authenticatzo.Data.Entities
{
    [Table("tblLanguage")]
    public partial class TblLanguage
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("languageName")]
        [StringLength(150)]
        public string LanguageName { get; set; }
        [Column("languageDescription")]
        [StringLength(255)]
        public string LanguageDescription { get; set; }
    }
}
