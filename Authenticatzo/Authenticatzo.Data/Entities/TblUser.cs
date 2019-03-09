using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authenticatzo.Data.Entities
{
    [Table("tblUser")]
    public partial class TblUser
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("firstName")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("lastName")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Column("emailId")]
        [StringLength(150)]
        public string EmailId { get; set; }
        [Column("passwordByte")]
        [StringLength(255)]
        public string PasswordByte { get; set; }
        [Column("passwordSalt")]
        [StringLength(150)]
        public string PasswordSalt { get; set; }
    }
}
